using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class graphManager : MonoBehaviour
{
    // Constants for the (0,0) of the graph
    private const float xStart = -5f;
    private const float yStart = -5f;
    private const float scaleFactor = 1f;


    private float taskHeight = 50f;
    private float taskWidth = 50f;

    // Reference to Task Prefab
    [SerializeField]
    private GameObject task;
    [SerializeField]
    private Canvas mc; // main Canvas




    // Start is called before the first frame update
    void Start()
    {
        taskHeight = ((RectTransform)task.transform).rect.height;
        DEBUG();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DEBUG(){
        //Test Method, can safely be ignored upon merge to Master.
        var l = new List<Task>();
        l.Add(new Task(1,1,2));
        l.Add(new Task(1,1,3));
        l.Add(new Task(1,2,3));
        l.Add(new Task(1,2,5));

        GenerateGraph(l);

    }

    private void GenerateGraph(List<Task> taskList){
        float x = xStart;
        float y = yStart;
        float xReset = x;
    // Graph Generator(List<Task>) ( Generates the graph with relative placements, 10x10 should be a good starting point.)
        foreach(Task t in taskList){
            x = x + t.GetRelease();
            
            var newBar = GameObject.Instantiate(task, new Vector3(0,0,0), Quaternion.identity);

            newBar.name = "(r=" + t.GetRelease()+"|d=" + t.GetDeadline() + ")";
            
            var TaskData = newBar.GetComponent<Task>();
            TaskData.SetRelease(t.GetRelease());
            TaskData.SetDeadline(t.GetDeadline());
            TaskData.SetWork(t.GetWork());
            //TaskData.SetDimensionsOfTask(); //Causes issues with generations?
            newBar.transform.parent = mc.transform; //attatch new Task to the Canvas for display.
            
            var len = (float)((t.GetDeadline() - t.GetRelease()));
            //newBar.transform.localScale = new Vector3((len/2),scaleFactor,1f);
            //var rt = ((RectTransform)newBar.transform);
            //rt.sizeDelta = new Vector2((len*taskWidth), taskHeight);
            
            newBar.transform.localPosition = new Vector3(((t.GetRelease()*taskHeight)*(len/2)),y,1f);
            
            y = y + taskHeight + 1f;
            x = xReset;
        }

        /*
            x = -3f;
            y = -2f;
            var resetX = x;

            foreach(dat t in data){

                x = x + t.r;
                if (x > (x+limitX)){

                    Debug.Log("X var out of Bounds!");
                }
                var len = t.d-t.r;
                var newBar = GameObject.Instantiate(go,new Vector3(x + (len/2f),y,0), Quaternion.identity);
                
                
                newBar.name = "(r=" +t.r+"|d="+t.d+")";
                newBar.transform.localScale = new Vector3(len,0.8f,0.8f);
                
                y++;
                x = resetX;
            }
        */


    }

    private void AssignColourToTask(){
    // Colour Changer(instance of Task) (changes colour based on number of tasks available. 5-10 should be a good start.)


    }

}
