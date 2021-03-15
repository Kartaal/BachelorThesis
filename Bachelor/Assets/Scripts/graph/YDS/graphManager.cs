using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphManager : MonoBehaviour
{
    // Constants for the (0,0) of the graph
    private const float xStart = 0f;//-550f;
    private const float yStart = 0f;//-350f;

    private float taskHeight = 50f; // default value.
    private float taskWidth = 100f; // default value.
    private float xScale = 100f;

    private static int taskNum = 0;

    private Color32[] colr;

    // Reference to Task Prefab
    [SerializeField]
    private GameObject task;        // In-unity reference to Task prefab.
    [SerializeField]
    private Canvas mc;              // main Canvas
    
    [SerializeField]
    private AlgoManager algoManager;


    // Start is called before the first frame update
    void Start()
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

        taskHeight = ((RectTransform)task.transform).rect.height;
        taskWidth = ((RectTransform)task.transform).rect.width;
        //DEBUG(); // Should be removed upon merge to Master.

        Worker worker = algoManager.GetComponent<Worker>();


        Schedule schedule = worker.YDS(algoManager.tasks, 1);


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
        //local variables are copied from const, as they may vary throughout the loop.
        float x = xStart;
        float y = yStart;
        RectTransform rt = null;


        foreach (Task t in tl)
        {
            rt = (RectTransform) t.transform;

            var startX = (x + t.GetRelease()) * xScale;

            var startY = y + (t.GetId() * taskHeight);

            rt.localPosition = new Vector2(startX, startY);
            AssignColourToTask(t.gameObject);


            /* RectTransform min and max x and y values (actual coordinates)
                float left   =  rt.offsetMin.x;
                float right  =  rt.offsetMax.x;
                float top    =  rt.offsetMax.y;
                float bottom =  rt.offsetMin.y;
            */
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
