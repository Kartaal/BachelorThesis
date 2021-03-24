using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();

    [SerializeField]
    private GameObject taskPrefab;
    private int iterationYDS = 1;
    private int stepYDS = 1;

    /* Getters */
    public int GetIterationYDS() { return iterationYDS; }
    public int GetStepYDS() { return stepYDS; }
    
    /* Setters */
    public void SetIterationYDS(int newIter)
    {
        iterationYDS = newIter;
    }

    public void SetStepYDS(int newStep)
    {
        stepYDS = newStep;
    }

    // Ensure that when becoming active, iteration and step are both 1 and generate tasks
    private void Awake() 
    {
        iterationYDS = 1;
        stepYDS = 1;
        //GenerateLockedYDSTasks();
    }

    public void GenerateLockedYDSTasks()
    {
        // Make lists of task releases, deadlines and works
        List<int> taskReleases = new List<int>();
        taskReleases.Add(1);
        taskReleases.Add(4);
        taskReleases.Add(4);
        taskReleases.Add(6);
        taskReleases.Add(3);
        taskReleases.Add(4);
        taskReleases.Add(7);
        taskReleases.Add(6);

        List<int> taskDeadlines = new List<int>();
        taskDeadlines.Add(3);
        taskDeadlines.Add(7);
        taskDeadlines.Add(6);
        taskDeadlines.Add(9);
        taskDeadlines.Add(5);
        taskDeadlines.Add(7);
        taskDeadlines.Add(10);
        taskDeadlines.Add(11);

        List<double> taskWork = new List<double>();
        taskWork.Add(5.0);
        taskWork.Add(6.0);
        taskWork.Add(3.0);
        taskWork.Add(10.0);
        taskWork.Add(5.0);
        taskWork.Add(7.0);
        taskWork.Add(9.0);
        taskWork.Add(2.0);

        Transform canvasTransform = gameObject.transform.parent.gameObject.transform;
        Transform taskContainerTransform = canvasTransform.Find("TaskContainer").transform;
        foreach (Transform trans in taskContainerTransform)
        {
            trans.gameObject.SetActive(false);
        }

        // Generate tasks and assign field values
        for (int i = 0 ; i < 8 ; i++)
        {
            GameObject taskGO = Instantiate(taskPrefab, this.transform.position, this.transform.rotation);
            Task task = taskGO.GetComponent<Task>();
            task.SetId(i);
            task.SetRelease(taskReleases[i]);
            task.SetDeadline(taskDeadlines[i]);
            task.SetWork(taskWork[i]);

            tasks.Add(task);

            taskGO.name = $"Task ({i})";

            taskGO.transform.SetParent(taskContainerTransform);
        }

        /*                      work - release - deadline
            sampleSchedule.Add(new Task(5, 1, 3));
            sampleSchedule.Add(new Task(6, 4, 7));
            sampleSchedule.Add(new Task(3, 4, 6));
            sampleSchedule.Add(new Task(10, 6, 9));
            sampleSchedule.Add(new Task(5, 3, 5));
            sampleSchedule.Add(new Task(7, 4, 7));
            sampleSchedule.Add(new Task(9, 7, 10));
            sampleSchedule.Add(new Task(2, 6, 11));


        */
    }


    
}
