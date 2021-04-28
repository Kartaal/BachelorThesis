using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

public class Task : MonoBehaviour
{
    //Constant variables for bounded randomness.
    private const int wrkMin = 1;
    private const int wrkMax = 11;
    private const int relMin = 1;
    private const int relMax = 10;
    private const int dedMax = 11;

    [SerializeField]
    private const float scaleForDimensions = 100f;
    [SerializeField]
    private const float scaleHeight = 25f;
    private const float positionHeight = 15f;
    private const float positionHeightFactor = 3f;

    [SerializeField]
    private int id;
    [SerializeField]
    private int releaseT, deadlineT;
    [SerializeField]
    private double workT;
    [SerializeField]
    private double taskIntensity = 0.0f;
    private bool complete;

    private double start;
    private double runDuration;

    //fields needed to make the task visual
    private float width;
    private float height;
    private RectTransform rt;

    private bool scheduled = false;

    // Start is called before the first frame update
    void Start()
    {
        rt = (RectTransform) gameObject.transform;
        SetDimensionsOfTask();
        CalcIntensity();
        //DEBUG();
    }

    private void DEBUG()
    {
        Debug.Log($"Task {id} has run Start()");
        Debug.Log("width = " + rt.rect.width);
        Debug.Log("height = " + rt.rect.height);
    }

    /* Method for setting the dimensions of the visual task. Right now it only sets them when it starts, 
     * but could be useful to use it in update, to change size dynamically.
     *
     * As of now the intervals it stretches over (deadlineT - releaseT) is multipled with 100, to scale it up to a size that makes sense.
     * The scale which it should be upscaled with can be changed, if we find a better scale.
     */
    public void SetDimensionsOfTask()
    {
        rt = (RectTransform) gameObject.transform;
        width = (deadlineT - releaseT) * scaleForDimensions;
        height = (float) taskIntensity * scaleHeight;
        rt.sizeDelta = new Vector2(width, height);
    }

    // Same as SetDimensionsOfTask() but sets width according to duration instead of deadline
    public void SetScheduledDimensionsOfTask()
    {
        //Debug.Log($"Task {id} running scheduled dimensions");
        rt = (RectTransform) gameObject.transform;
        width = (float) runDuration * scaleForDimensions;
        height = (float) taskIntensity * scaleHeight;
        rt.sizeDelta = new Vector2(width, height);
    }

    public void SetPosition()
    {
        rt = (RectTransform) gameObject.transform;
    
        var startX = releaseT * scaleForDimensions;

        float startY = 0f;

        // If not scheduled, make partial overlap thing
        if(!scheduled)
        {
            startY = (id  * positionHeight) * positionHeightFactor;
        }

        rt.localPosition = new Vector2(startX, startY);
    }

    // Same as SetPosition but positions based on parameter and doesn't check for being scheduled
    public void SetScheduledPosition(double start)
    {
        rt = (RectTransform) gameObject.transform;
    
        var startX = (float) start * scaleForDimensions;

        float startY = 0f;

        rt.localPosition = new Vector2(startX, startY);
    }

    public void CalcIntensity()
    {
        double duration = deadlineT - releaseT;
        taskIntensity = workT / duration;
    }


    /*Getters*/
    public int GetId() { return id; }
    public double GetWork() { return workT; }
    public int GetRelease() { return releaseT; }
    public int GetDeadline() { return deadlineT; }
    public double GetIntensity() { return taskIntensity; }
    public bool GetComplete() { return complete; }
    public float GetWidth() { return width; }
    public float GetHeight() { return height; }
    public bool GetScheduled() { return scheduled; }
    public double GetStart() { return start; }
    public double GetDuration() { return runDuration; }

    /*Setters*/
    public void SetId(int newId) { id = newId; }
    public void SetComplete(bool newComplete) { complete = newComplete; }
    public void SetDeadline(int newDeadline) { deadlineT = newDeadline; }
    public void SetWork(double newWorkload) { workT = newWorkload; }
    public void SetRelease(int newRelease) { releaseT = newRelease; }
    public void SetIntensity(double newIntensity) { taskIntensity = newIntensity; }
    public void SetScheduled(bool newSche) { scheduled = newSche; }
    public void SetStart(double newStart) { start = newStart; }
    public void SetDuration(double newDuration) { runDuration = newDuration; }

    /* 
     * Takes the remaining amount of time that can be used to work (between 0 and 1)
     * If amount of work done in remaining time is less than work load, 
     *        update work load and set remaining time to 0
     *        Increment release time to indicate work has been done but not completed 
     *                (ensures task gets added to tasks to be scheduled)
     * If amount of work done in remaining time is more than work load,
     *        update remaining time and set work load to 0 and complete to true
     * Always return remaining time after committing work, 
     *      this is needed for working on other tasks
    */
    public double CommitWork(double timeRemaining, int currentTime)
    {
        // Can't complete this task
        if (timeRemaining * taskIntensity < workT)
        {
            workT -= timeRemaining * taskIntensity;
            releaseT = currentTime;
            timeRemaining = 0f;
        }
        else // More work can be done after this task
        {
            timeRemaining -= workT / taskIntensity;
            workT = 0f;
            complete = true;
        }
        return timeRemaining;
    }

    public override string ToString()
    {
        return "ID: " + id + " | " +
               "WORKLOAD: " + workT + " | " +
               "RELEASE: " + releaseT + " | " +
               "DEADLINE: " + deadlineT + " | " +
               "INTENSITY: " + taskIntensity;
    }

}


/*
 a job denoted by I
has a release time R:I when it arrives
a work requirement W:I
and a Deadline D:I 
if I runs in S constant time
it will be completed by
W:I / S
     */
