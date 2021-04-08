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

    private void Awake() 
    {
        worker = algoManager.GetComponent<Worker>();
        algoManager.GenerateLockedYDSTasks();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {

        gsh = algoManager.GetComponent<GraphStateHandler>();

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
        //DEBUG(); // Should be removed upon merge to Master.
        
        yield return new WaitForSeconds(1);
    //---------------
        var inputTaskContainerTransform = gameObject.transform.parent.Find("InputTaskContainer");
        var taskContainerTransform = gameObject.transform.parent.Find("TaskContainer");
        
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
    //----------------

        RunYDS();

        ResetSteps();
    }

    void RunYDS()
    {
        Schedule schedule = worker.YDS(algoManager.tasks, 1);

        // Run this snippet after YDS... sets the algoManager's task list to contain the Tasks visualised in the graph
        var taskContainerTransform = gameObject.transform.parent.Find("TaskContainer");

        foreach (Transform taskTransform in taskContainerTransform)
        {
            Task task = taskTransform.gameObject.GetComponent<Task>();
            algoManager.tasks.Add(task);
        }

        GenerateGraph(schedule.GetTaskList());

    }

    private void DEBUG(){
        //Test Method, can safely be ignored upon merge to Master.
        
        var l = new List<Task>();
        l.Add(new Task(1,1,2));
        l.Add(new Task(1,1,3));
        l.Add(new Task(1,2,3));
        l.Add(new Task(1,2,5));
        l.Add(new Task(4,2,7));
        l.Add(new Task(5,8,9));
        l.Add(new Task(2,4,6));

        GenerateGraph(l);

    }


    /*
        Main method for Generating the graph. Does not provide Cleanup if called again with already existing elements.
        Takes a list of Task Objects Currently. Should be rewritten with propper functionality in mind.
        Also, it shouldn't take Monobehaviours.
    */
    private void GenerateGraph(List<Task> tl){
        //List<Task> sortedTasks = tl.OrderByDescending(t => t.GetRelease()).ToList();

        foreach (Task t in tl)
        {

            t.SetDimensionsOfTask();
            AssignColourToTask(t);

            t.SetPosition();

            //t.GetComponent<tooltip>().UpdateToolTipInformation();
            /* RectTransform min and max x and y values (actual coordinates)
                float left   =  rt.offsetMin.x;
                float right  =  rt.offsetMax.x;
                float top    =  rt.offsetMax.y;
                float bottom =  rt.offsetMin.y;
            */
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


    // Steps through the states made by YDS
    public void Step()
    {
        int iteration = algoManager.GetIterationYDS();
        int step = algoManager.GetStepYDS();

        Debug.Log($"Iteration: {iteration} - Step: {step}");

        GraphState state = gsh.GetGraphState(iteration, step);

        // Do nothing if retrieved state is null...
        if(state != null)
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
                    }
                }

                // Updates the Dimensions of the Task.
                t.SetDimensionsOfTask();
                t.SetPosition();
            }

            IntervalData stepMII = state.GetInterval();

            miiTool.IntervalDataToVisual(stepMII);


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

            // Update the iteration and step numbers in algoManager
            algoManager.SetIterationYDS(iteration);
            algoManager.SetStepYDS(step);
        }
    }

    // A reset for stepping through the algorithm
    public void ResetSteps()
    {
        algoManager.SetStepYDS(1);
        algoManager.SetIterationYDS(1);

        // Run Step() to reflect the reset on graph
        Step();
    }

}
