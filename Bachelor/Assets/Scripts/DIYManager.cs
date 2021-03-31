using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DIYManager : MonoBehaviour
{
    [SerializeField]
    private AlgoManager algoManager;

    private GraphStateHandler gsh;

    private int currentStep = 1;
    private int currentIteration = 1;

    private List<Task> allTaskFromUserInput;
    private IntervalData maxIntervalUserInput;

    private GraphState graphStateToCompareTo;
    private List<TaskData> allTaskDataFromGraphState;
    private List<Task> allTasksFromScheduleInGraphState;
    private IntervalData maxIntervalFromGraphState;


    // Start is called before the first frame update
    void Start()
    {
        gsh = algoManager.GetComponent<GraphStateHandler>();
    }

    public void CheckUserAnswer()
    {
        /*setting up data from the graph state we need to compare to*/
        graphStateToCompareTo = gsh.GetGraphState(currentIteration, currentStep);

        allTaskDataFromGraphState = graphStateToCompareTo.GetTaskDatas();


        /*gonna start of by comparing task and taskdata since we can't edit max intensity yet, this is needed to check max
        maxIntervalFromGraphState = graphStateToCompareTo.GetInterval();*/

        /*Setting up the tasks presumably edited by the user*/
        allTaskFromUserInput = transform.parent.Find("GraphContainer").Find("TaskContainer").GetComponentsInChildren<Task>().ToList<Task>();

        var countOfCorrectTasks = 0;

        if (CompareTasks(ref countOfCorrectTasks))
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
            Debug.Log("Not all tasks are correct, try again");
        }


        //This is for the future for when comparemax intensity is implemented
        /*if (CompareTasks())
        {
            if (CompareMax())
            {
                Debug.Log("You did everythign correct! Nice job, time for next step, what do you do now?");
                /* if (currentStep == 3)
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
            if (CompareMax())
            {
                Debug.Log("did the max interval correctly, but you should check the tasks, not all are correct");
            }
            else
            {
                Debug.Log("You should check both the tasks and the max interval, non is correct");
            }
        }*/


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
                    Debug.Log("USER TASK: " + user.GetId() + " CORRECT TASK: " + correctTask.GetId());
                    Debug.Log("USER TASK REL: " + user.GetRelease() + " CORRECT TASK REL: " + correctTask.GetRel());
                    Debug.Log("USER TASK DEAD: " + user.GetDeadline() + " CORRECT TASK DEAD: " + correctTask.GetDed());
                    Debug.Log("USER TASK WORK: " + user.GetWork() + " CORRECT TASK WORK: " + correctTask.GetWrk());
                    Debug.Log("USER TASK INTENSITY: " + user.GetIntensity() + " CORRECT TASK INTENSITY: " + correctTask.GetIntensity());

                    if (correctTask.GetRel() == user.GetRelease() && correctTask.GetDed() == user.GetDeadline() && correctTask.GetWrk() == user.GetWork() && correctTask.GetIntensity().Equals(user.GetIntensity()))
                    {
                        Debug.Log("Task: " + user.name + " is correct well done!");
                        countOfCorrectTasks++;
                        Debug.Log("Current correct Tasks: " + countOfCorrectTasks);
                    }
                    else { Debug.Log("Task: " + user.name + " is wrong"); }
                }
            }
        }
        /*for (int i = 0; i < allTaskDataFromGraphState.Count; i++)
        {
            var correctTask = allTaskDataFromGraphState[i];
            var user = allTaskFromUserInput[i];

            if (correctTask.GetId() == user.GetId())
            {

                if (correctTask.GetRel() == user.GetRelease() && correctTask.GetDed() == user.GetDeadline() && correctTask.GetWrk() == user.GetWork())
                {
                    Debug.Log("Task: " + user.name + " is correct well done!");
                    countOfCorrectTasks++;
                    Debug.Log("Current correct Tasks: " + countOfCorrectTasks);
                }
                else { Debug.Log("Task: " + user.name + " is wrong"); }
            }
        }*/

        if (countOfCorrectTasks == allTaskDataFromGraphState.Count)
        {
            countOfCorrectTasks = 0;
            return true;
        }
        countOfCorrectTasks = 0;
        return false;
    }
}
