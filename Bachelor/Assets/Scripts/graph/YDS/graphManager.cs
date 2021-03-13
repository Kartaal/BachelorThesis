using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphManager : MonoBehaviour
{
    // Constants for the (0,0) of the graph
    private const float xStart = -550f;
    private const float yStart = -350f;

    private float taskHeight = 50f; // default value.
    private float taskWidth = 100f; // default value.

    private static int taskNum = 0;

    private Color32[] colr;

    // Reference to Task Prefab
    [SerializeField]
    private GameObject task;        // In-unity reference to Task prefab.
    [SerializeField]
    private Canvas mc;              // main Canvas


    // Start is called before the first frame update
    void Start()
    {
        colr = new Color32[] {new Color32(0,102,255,100), new Color32(0,204,153,100), new Color32(153,255,51,100), 
                                new Color32(255,204,0,100), new Color32(255,51,0,100), new Color32(204,0,102,100), 
                                 new Color32(0,153,153,100), new Color32(255,255,255,100)};
        taskHeight = ((RectTransform)task.transform).rect.height;
        taskWidth = ((RectTransform)task.transform).rect.width;
        DEBUG(); // Should be removed upon merge to Master.
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
        //local variables are copied from const, as they may vary throughout the loop.
        float x = xStart;
        float y = yStart;
        float xReset = xStart;

        // Main loop for generation. iterates over all given tasks and assigns elements propperly.
        foreach(Task t in tl){
            // Creates a new task object, and sets the name. Name is purely for Debug purposes.
            var newBar = GameObject.Instantiate(task,new Vector3(0,0,0), Quaternion.identity);
            newBar.name = "(r=" + t.GetRelease()+"|d=" + t.GetDeadline() + ")";

            // Aquires the Task.cs component of the instantiated task. Sets the variables based on input.
            var TaskData = newBar.GetComponent<Task>();
            TaskData.SetRelease(t.GetRelease());
            TaskData.SetDeadline(t.GetDeadline());
            TaskData.SetWork(t.GetWork());


            //assigns a color to the Task, to help differentiate them.
            AssignColourToTask(newBar);

            // Sets parent to canvas to ensure propper visibility.
            newBar.transform.SetParent(mc.transform);

            // Calculates the length of the task based on the release and deadlines, factored up to the taskWidth divided by 2.
            float len = (taskWidth/2) * (t.GetDeadline() - t.GetRelease());

            /* Sets the center of the instantiated task with an offset based on it's release, 
               and the relative length of the task for consistency.
                A new task is located, starting from x, with an offset of taskWidth times the release time
                of the given task. Added with our length calculation to put the center in the right location.
            */

            newBar.transform.localPosition = new Vector3((x + (t.GetRelease() * taskWidth)) + len, y, 1f);

            // Updates Y with task height to ensure tasks stacking on top of eachother. With an offset of +2
            // To make sure there is room in between eac htask. Dynamically scales with taskHeight.
            y = y + taskHeight + 2f;

            // Resets the x value to provide base Offset.
            x = xReset;

        }

    }


    private void AssignColourToTask(GameObject img){
    // Colour Changer(instance of Task) (changes colour based on number of tasks available. 5-10 should be a good start.)
        

        //WARNING: Can cause Index Out of bounds errors
        var image = img.GetComponent<Image>();

        image.color = colr[taskNum];

        taskNum++;
        

    }

}
