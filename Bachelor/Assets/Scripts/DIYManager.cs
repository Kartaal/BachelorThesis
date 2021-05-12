using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DIYManager : MonoBehaviour
{
    [SerializeField]
    private AlgoManager algoManager;
    [SerializeField]
    private TextMeshProUGUI explanationText;
    [SerializeField]
    private TextMeshProUGUI overviewText;

    private GraphStateHandler gsh;

    private int currentStep = 1;
    private int currentIteration = 1;

    private int prevStep = 0;
    private int prevIteration = 0;

    private List<Task> allTaskFromUserInput;
    private Slider maxIntervalUserLEFT;
    private Slider maxIntervalUserRIGHT;
    private int maxIntervalUserInputSTART;
    private int maxIntervalUserInputEND;


    private GraphState graphStateToCompareTo;
    private List<TaskData> allTaskDataFromGraphState;
    private IntervalData maxIntervalFromGraphState;


   
    IEnumerator Start()
    {
        gsh = algoManager.GetComponent<GraphStateHandler>();
        allTaskFromUserInput = transform.parent.Find("GraphContainer").Find("OutputContainer").GetComponentsInChildren<Task>().ToList<Task>();
        overviewText.text = $"Iteration {currentIteration} | Step {currentStep}";

        maxIntervalUserLEFT = transform.parent.Find("LeftBar").GetComponent<Slider>();
        maxIntervalUserRIGHT = transform.parent.Find("RightBar").GetComponent<Slider>();

        yield return new WaitForSeconds(1.01f);
        maxIntervalUserLEFT.value = 0;
        maxIntervalUserRIGHT.value = 0;
    }

    public void CheckUserAnswer()
    {
        SetupDataToCompare();

        var countOfCorrectTasks = 0;

        if (CompareTasks(ref countOfCorrectTasks) && CompareMaxIntensityInterval())
        {
            explanationText.text = "You edited both the maximum intensity interval and tasks correctly, good job. Time for next step, what should you do now?";

            CalcNextGraphState();

            if (currentIteration == gsh.GetIterationCount())
            {
                overviewText.text = "You have finished the algorithm, good job!";
            }
            else
            {
                overviewText.text = $"Iteration {currentIteration} | Step {currentStep}";
            }

            DisableScheduledTasks();
        }
        else if (CompareTasks(ref countOfCorrectTasks) && !CompareMaxIntensityInterval())
        {
            explanationText.text = "You edited all the tasks correctly, are you sure about the maximum intensity interval?";
        }
        else if (!CompareTasks(ref countOfCorrectTasks) && CompareMaxIntensityInterval())
        {
            explanationText.text = "You edited the maximum intensity interval correctly, but you should check the tasks, not all of them are correct.";
        }
        else
        {
            explanationText.text = "You should check both the tasks and the maximum intensity interval. They have not been edited correctly.";
        }



    }

    private void DisableScheduledTasks()
    {
        foreach (Task t in allTaskFromUserInput)
        {
            if (t.GetScheduled())
            {
                t.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void CalcNextGraphState()
    {
        if (currentStep == 3)
        {
            prevStep = currentStep;
            prevIteration = currentIteration;

            currentStep = 1;
            currentIteration = currentIteration + 1;
        }
        else
        {
            prevStep = currentStep;

            currentStep++;
            if(prevIteration == 0) { prevIteration = 1; }
        }
    }

    private void SetupDataToCompare()
    {
        /*setting up data from the graph state we need to compare to*/
        graphStateToCompareTo = gsh.GetGraphState(currentIteration, currentStep);

        allTaskDataFromGraphState = graphStateToCompareTo.GetTaskDatas();

        maxIntervalFromGraphState = graphStateToCompareTo.GetInterval();

        /*Setting up the max intensity interval presumably edited by the user*/
        maxIntervalUserInputSTART = (int)maxIntervalUserLEFT.value;
        maxIntervalUserInputEND = (int)maxIntervalUserRIGHT.value;
    }

    private bool CompareMaxIntensityInterval()
    {
        if (maxIntervalUserInputSTART == maxIntervalFromGraphState.GetStartInt() && maxIntervalUserInputEND == maxIntervalFromGraphState.GetEndInt())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
     * This method compares the tasks from userinput (the edited tasks) with the task from graph state
     * Since that tasks are removed from the graphstate tasks when scheduled, 
     * we check whether the user has edited tasks correctly equal to the number of tasks from the graph state.
     * What we could do in the future, is make it so tasks which has been edited correctly cannot be edited again
     * Their button function is set to inactive.
     */
    private bool CompareTasks(ref int countOfCorrectTasks)
    {
        /*General it seems that tasks in user input list and the graph state are ordered the same way
                 * But just in case they at somepoint ain't we have a for loop in a for loop
                 * to make sure the tasks has same ID
                 */
        for (int i = 0; i < allTaskDataFromGraphState.Count; i++)
        {
            var correctTask = allTaskDataFromGraphState[i];

            for (int j = 0; j < allTaskFromUserInput.Count; j++)
            {
                
                var user = allTaskFromUserInput[j];

                if (correctTask.GetId() == user.GetId())
                {
                    var defIntensity = correctTask.GetIntensity() - user.GetIntensity();
                    var defAcceptance = 0.0000001;

                    if (correctTask.GetRel() == user.GetRelease() && correctTask.GetDed() == user.GetDeadline() && correctTask.GetWrk() == user.GetWork() && defIntensity < defAcceptance && correctTask.GetScheduled() == user.GetScheduled())
                    {
                        countOfCorrectTasks++;
                    }
                    else { Debug.Log("Task: " + user.name + " is wrong"); }
                }
            }
        }

        if (countOfCorrectTasks == allTaskDataFromGraphState.Count)
        {
            countOfCorrectTasks = 0;
            return true;
        }
        countOfCorrectTasks = 0;
        return false;
    }
    /*
     * Resets the tasks to the previous step (the last step that was correctly done by the user)
     * If no step was completed, reset interval sliders to 0, otherwise reset as correct.
     */
    public void ResetGraph()
    {
        GraphState state = null;

        if(prevIteration == 0 && prevStep == 0)
        {
            state = gsh.GetGraphState(1, 1);
            IntervalData mii = state.GetInterval();

            maxIntervalUserLEFT.value = 0;
            maxIntervalUserRIGHT.value = 0;

        }
        else
        {
            Debug.Log($"prevIt: {prevIteration} - prevStep: {prevStep}");
            state = gsh.GetGraphState(prevIteration, prevStep);
            IntervalData mii = state.GetInterval();

            maxIntervalUserLEFT.value = mii.GetStartInt();
            maxIntervalUserRIGHT.value = mii.GetEndInt();
        }

        // DrawGraph code from GraphManager
        // Update information in individual tasks...
        foreach (Task t in allTaskFromUserInput)
        {
            // This can probably be done better with LINQ
            foreach (TaskData td in state.GetTaskDatas())
            {
                // If IDs match, update the task's fields
                if(td.GetId() == t.GetId())
                {
                    t.SetRelease(td.GetRel());
                    t.SetDeadline(td.GetDed());
                    t.SetWork(td.GetWrk());
                    t.SetIntensity(td.GetIntensity());
                    t.SetScheduled(td.GetScheduled());
                    t.SetStart(td.GetStart());
                    t.SetDuration(td.GetDuration());

                    // Updates the Dimensions of the Task
                    t.SetDimensionsOfTask();
                    t.SetPosition();
                }
            }

        }

    }
}
