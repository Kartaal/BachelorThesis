using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class graphManager : MonoBehaviour
{
    // Constants for the (0,0) of the graph
    private const float xStart = 0f;//-550f;
    private const float yStart = 0f;//-350f;

    private float taskHeight = 50f; // default value.
    private float taskWidth = 100f; // default value.
    private float xScale = 100f;

    //[SerializeField]
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

    private graphStateHandler gsh;

    // Start is called before the first frame update
    void Start()
    {
        gsh = algoManager.GetComponent<graphStateHandler>();

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

        taskHeight = ((RectTransform)task.transform).rect.height;
        taskWidth = ((RectTransform)task.transform).rect.width;
        //DEBUG(); // Should be removed upon merge to Master.

        Worker worker = algoManager.GetComponent<Worker>();

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DEBUG(){
        //Test Method, can safely be ignored upon merge to Master.
        
        // Creating instances of Monobehaviours is not allowed. Making dummy data struct for input testing.
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
        List<Task> sortedTasks = tl.OrderByDescending(t => t.GetRelease()).ToList();

        foreach (Task t in sortedTasks)
        {

            t.SetDimensionsOfTask();
            AssignColourToTask(t);

            t.SetPosition();

            t.GetComponent<tooltip>().UpdateToolTipInformation();
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

        graphState state = gsh.GetGraphState(iteration, step);

        // Do nothing if retrieved state is null...
        if(state != null)
        {
            List<Task> tasks = algoManager.tasks;

            List<taskData> taskDataList = state.GetTaskDatas();

            // Update information in individual tasks...
            foreach (Task t in tasks)
            {
                // This can probably be done better with LINQ
                foreach (taskData td in taskDataList)
                {
                    // If IDs match, update the task's fields
                    if(td.getId() == t.GetId())
                    {
                        t.SetRelease(td.getRel());
                        t.SetDeadline(td.getDed());
                        t.SetWork(td.getWrk());
                        t.SetIntensity(td.getIntensity());
                    }
                }

                // Remember to update the dimensions...
                t.SetDimensionsOfTask();
                t.SetPosition();
            }

            IntervalData stepMII = state.GetInterval();

            miiTool.IntervalDataToVisual(stepMII);


            // Update iteration and step correctly... looping at step = 3
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
