using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DIYManager : MonoBehaviour
{
    [SerializeField]
    private AlgoManager algoManager;

    private GraphStateHandler gsh;

    private int currentStep = 1;
    private int currentIteration = 1;

    private List<Task> allTaskFromUserInput;
    private int maxIntervalUserInputSTART;
    private int maxIntervalUserInputEND;


    private GraphState graphStateToCompareTo;
    private List<TaskData> allTaskDataFromGraphState;
    //private List<Task> allTasksFromScheduleInGraphState;
    private IntervalData maxIntervalFromGraphState;


   
    void Start()
    {
        gsh = algoManager.GetComponent<GraphStateHandler>();
        allTaskFromUserInput = transform.parent.Find("GraphContainer").Find("TaskContainer").GetComponentsInChildren<Task>().ToList<Task>();
    }

    public void CheckUserAnswer()
    {
        SetupDataToCompare();

        var countOfCorrectTasks = 0;

        if (CompareTasks(ref countOfCorrectTasks))
        {
            if (CompareMaxIntensityInterval())
            {
                Debug.Log("You did everythign correct! Nice job, time for next step, what do you do now?");
                if (currentStep == 3)
                {
                    currentStep = 1;
                    currentIteration = currentIteration + 1;
                }
                else
                {
                    currentStep++;
                }
            }
            else
            {
                Debug.Log("You set all the tasks correctly, are you sure about the max interval?");
            }

        }
        else
        {
            if (CompareMaxIntensityInterval())
            {
                Debug.Log("did the max interval correctly, but you should check the tasks, not all are correct");
            }
            else
            {
                Debug.Log("You should check both the tasks and the max interval, non is correct");
            }
        }


    }

    private void SetupDataToCompare()
    {
        /*setting up data from the graph state we need to compare to*/
        graphStateToCompareTo = gsh.GetGraphState(currentIteration, currentStep);

        allTaskDataFromGraphState = graphStateToCompareTo.GetTaskDatas();

        maxIntervalFromGraphState = graphStateToCompareTo.GetInterval();

        /*Setting up the max intensity interval presumably edited by the user*/
        maxIntervalUserInputSTART = (int)transform.parent.Find("LeftBar").GetComponent<Slider>().value;
        maxIntervalUserInputEND = (int)transform.parent.Find("RightBar").GetComponent<Slider>().value;
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
     *This method compares the tasks from userinput (the edited tasks) with the task from graph state
     *Since that tasks are removed from the graphstate tasks when scheduled, 
     *we check whether the user has edited tasks correctly equal to the number of tasks from the graph state.
     *What we could do in the future, is make it so tasks which has been edited correctly cannot be edited again
     *Their button function is set to inactive.
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

                    if (correctTask.GetRel() == user.GetRelease() && correctTask.GetDed() == user.GetDeadline() && correctTask.GetWrk() == user.GetWork() && defIntensity < defAcceptance)
                    {
                        Debug.Log("Task: " + user.name + " is correct well done!");
                        countOfCorrectTasks++;
                        Debug.Log("Current correct Tasks: " + countOfCorrectTasks);
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

    private static void DEBUG(TaskData correctTask, Task user)
    {
        Debug.Log("USER TASK: " + user.GetId() + " CORRECT TASK: " + correctTask.GetId());
        Debug.Log("USER TASK REL: " + user.GetRelease() + " CORRECT TASK REL: " + correctTask.GetRel());
        Debug.Log("USER TASK DEAD: " + user.GetDeadline() + " CORRECT TASK DEAD: " + correctTask.GetDed());
        Debug.Log("USER TASK WORK: " + user.GetWork() + " CORRECT TASK WORK: " + correctTask.GetWrk());
        Debug.Log("USER TASK INTENSITY: " + user.GetIntensity() + " CORRECT TASK INTENSITY: " + correctTask.GetIntensity());
    }
}
