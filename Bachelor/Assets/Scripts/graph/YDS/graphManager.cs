using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GraphManager : MonoBehaviour
{
    // Constants for the (0,0) of the graph
    private const float xStart = 0f;//-550f;
    private const float yStart = 0f;//-350f;

    private float taskHeight = 50f; // default value.
    private float taskWidth = 100f; // default value.

    private string maxStepAndIteration;

    private Color32[] colr;

    // Reference to Task Prefab
    [SerializeField]
    private GameObject task;        // In-unity reference to Task prefab.
    [SerializeField]
    private Canvas mc;              // main Canvas
    
    [SerializeField]
    private AlgoManager algoManager;

    [SerializeField]
    private maxIntensityVis miiTool;

    private GraphStateHandler gsh;

    private Worker worker;
    private Text graphStateInfo;

    private void Awake() 
    {
        worker = algoManager.GetComponent<Worker>();

        gsh = algoManager.GetComponent<GraphStateHandler>();

        // Needed to ensure graph output states aren't persisted between scenes somehow
        gsh.InitGSH();

        //Run task generation based on Scene index
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            // Index 3 is YDS locked walkthrough
            case 3:
                    algoManager.GenerateLockedYDSTasks();
                    break;
            // Index 5 is YDS DIY
            case 5:
                    algoManager.GenerateDIYYDSTasks(); // This method does not yet exist!
                    break;
            default:
                    break;
        }
        
        graphStateInfo = gameObject.transform.parent.parent.Find("GraphStateInfo").GetComponent<Text>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {

        colr = new Color32[] 
            {
                new Color32(0,102,255,100), 
                new Color32(0,204,153,100), 
                new Color32(153,255,51,100), 
                new Color32(255,204,0,100), 
                new Color32(255,51,0,100), 
                new Color32(204,0,102,100), 
                new Color32(0,153,153,100), 
                new Color32(255,255,255,100)
            };

        taskHeight = ((RectTransform) task.transform).rect.height;
        taskWidth = ((RectTransform) task.transform).rect.width;
        
        yield return new WaitForSeconds(1);

        var inputTaskContainerTransform = gameObject.transform.parent.Find("InputContainer").transform.Find("InputTaskContainer");
        var taskContainerTransform = gameObject.transform.parent.Find("OutputContainer").transform.Find("TaskContainer");

        List<Task> inputTasks = new List<Task>();

        foreach( Transform taskTransform in taskContainerTransform ){
            GameObject taskGO = Instantiate(taskTransform.gameObject, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            taskGO.transform.SetParent(inputTaskContainerTransform);
            taskGO.transform.localScale = Vector3.one;
            inputTasks.Add(taskGO.GetComponent<Task>());

            //For disabling buttons on DIY for the input tasks
            var buttonForEditableTasks = taskGO.GetComponent<Button>();
            if (buttonForEditableTasks != null)
            {
                buttonForEditableTasks.interactable = false;
            }
        }

        GenerateGraph(inputTasks);

        RunYDS();

        ResetSteps();
    }

    void RunYDS()
    {
        Schedule schedule = worker.YDS(algoManager.tasks, 1);

        // Run this snippet after YDS... sets the algoManager's task list to contain the Tasks 
        // visualised in the graph
        
        var taskContainerTransform = gameObject.transform.parent.Find("OutputContainer").transform.Find("TaskContainer");

        foreach (Transform taskTransform in taskContainerTransform)
        {
            Task task = taskTransform.gameObject.GetComponent<Task>();
            algoManager.tasks.Add(task);
        }

        GenerateGraph(schedule.GetTaskList());

    }

    /*
        Main method for Generating the graph. Does not provide Cleanup if called again with already existing 
        elements. Takes a list of Task Objects Currently. Should be rewritten with propper functionality in mind.
        Also, it shouldn't take Monobehaviours.
    */
    private void GenerateGraph(List<Task> tl)
    {

        foreach (Task t in tl)
        {

            t.SetDimensionsOfTask();
            AssignColourToTask(t);

            t.SetPosition();
        }

    }

    /*
     * Assigns color to a task GameObject by its associated Task object based on its id
     * NB! Currently have a limit of 8 colors, more than 8 tasks breaks this system.
     *      Breaks by throwing an error and misdrawing the 8th task(?), 
     *      all tasks after the 8th are never updated for position or color
    */
    private void AssignColourToTask(Task task)
    {
    // Colour Changer(instance of Task) 
        
        //WARNING: Can cause Index Out of bounds errors
        var image = task.gameObject.GetComponent<Image>();

        int id = task.GetId();

        image.color = colr[id];

    }

    //Updates an Info Textbox to let the user know which step they are on.
    private void UpdateInfo(int iter, int step)
    {
        // should ideally only be called once, small performance sink if run every time we go back OR forth.
        if (maxStepAndIteration == null){maxStepAndIteration = MaxStepAndIteration();}
     
        graphStateInfo.text = "Iteration: " + iter + " | Step: " + step + " - out of: " + maxStepAndIteration;
    }

    //Finds the max step and iteration to give the use an indication of where the simulation ends.
    private string MaxStepAndIteration()
    {
        int statesCount = gsh.GetStatesCount();

        // Every 3 steps is 1 iteration, so number of iterations is number of steps/states divided by 3
        int iteration = statesCount / 3;
        // Step is always 3, otherwise the algorithm didn't run to completion
        int step = 3;

        // returns a string of the max elements.
        return "Iteration: " + iteration + " | Step: " + step;
    }

    // Steps through the states made by YDS
    public void StepForward()
    {
        int iteration = algoManager.GetIterationYDS();
        int step = algoManager.GetStepYDS();

        /*
            Update current iteration, as each iteration is counted in sets of three
            GraphStates.
        */
        if(step == 3)
        {
            step = 1;
            iteration = iteration+1;
        }
        else
        {
            step++;
        }

        GraphState state = gsh.GetGraphState(iteration, step);

        if(state != null)
        {

            DrawGraph(state);
            UpdateInfo(iteration, step);

            // Update the iteration and step numbers in algoManager
            algoManager.SetIterationYDS(iteration);
            algoManager.SetStepYDS(step);
        }

    }

    public void StepBackwards()
    {
        int iteration = algoManager.GetIterationYDS();
        int step = algoManager.GetStepYDS();

        /*
            Update current iteration, as each iteration is counted in sets of three
            GraphStates.
        */
        if(step == 1)
        {
            if (iteration != 1)
            {
                step = 3;
                iteration = iteration-1;
            }
        }
        else
        {
            step--;
        }

        GraphState state = gsh.GetGraphState(iteration, step);
            
        // Do nothing if retrieved state is null...
        if(state != null)
        {
            DrawGraph(state);
            UpdateInfo(iteration, step);
            // Update the iteration and step numbers in algoManager
            algoManager.SetIterationYDS(iteration);
            algoManager.SetStepYDS(step);
        } 

    }


    private void DrawGraph(GraphState state)
    {
        List<Task> tasks = algoManager.tasks;

        List<TaskData> taskDataList = state.GetTaskDatas();

        // Update information in individual tasks...
        foreach (Task t in tasks)
        {
            // This can probably be done better with LINQ
            foreach (TaskData td in taskDataList)
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

        PositionScheduledTasks(state);

        IntervalData stepMII = state.GetInterval();
        miiTool.IntervalDataToVisual(stepMII);   
    }

    private void PositionScheduledTasks(GraphState state)
    {
        List<IntervalData> intervals = state.GetSchedule().GetIntervals();

        foreach (IntervalData id in intervals)
        {
            // Start task positioning from start of interval
            double prevFinish = id.GetStartInt();

            foreach (Task t in id.GetTasks())
            {
                if(t.GetScheduled())
                {
                    t.SetScheduledDimensionsOfTask();
                    t.SetScheduledPosition(prevFinish);

                    prevFinish += t.GetDuration();
                }
            }
        }
    }

    // A reset for stepping through the algorithm
    public void ResetSteps()
    {
        algoManager.SetStepYDS(1);
        algoManager.SetIterationYDS(1);

        // Run DrawGraph() to reflect the reset on graph + update info to reflect reset.
        GraphState state = gsh.GetGraphState(1,1);
        UpdateInfo(1, 1);
        DrawGraph(state);
    }

}
