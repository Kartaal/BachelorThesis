using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphStateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    private static List<graphState> histogram = new List<graphState>();
    
    private static Schedule lastKnownSchedule = new Schedule();

    void Start()
    {
        
    }

    public static void saveState(List<Task> tl, Schedule s, IntervalData intDat){
        // saves the result of the graphState asfter step 1 -OR- 2 -OR- 3
        /*
            SAVELIST
            -(1) The Initial graph state (no changes)
            -(1)  The Max Interval saved as an IntervalData object in the graphState.
            This can be used to draw the max interval on screen
            -(2) The altered graph state. Meaning Intensities have been calculated for affected tasks.
            This graph state wil be very simillar to the phase 1.
            -(2) Max Interval is still prevalent here. For highlighting purposes. (maybe)
            -(3) The Schedule is updated from the last known variant of the schedule 
            in graphStatHandler (this one). And written down.
        */

        var result = new graphState(); // graphState Object to save

        result.SetMaxIntensity(intDat); // Sets the Max Intensity INterval for the graphState

        var res = new List<taskData>(); // Prepares a list of TaskData, This is used for retracing to a previous stage.
        foreach(Task t in tl){
            res.Add(new taskData(t.GetRelease(), t.GetDeadline(), t.GetWork(), t.GetId(), t.GetIntensity()));
        }

        result.SetTaskData(res); // Saves the TaskData List.

        // If the provided schedule is empty, then don't update. Otherwise, do UPDATE!
        if ( !(s == new Schedule()) ){
            // might be faulty !?
    	        lastKnownSchedule = s;
        }

        //lastKnownSchedule.MergeSchedules(s); // adds the new schedule on top of the last known schedule
        result.SetSchedule(lastKnownSchedule); // adds the last known schedule to the graphState.

        histogram.Add(result); // Add the data to the Histogram. Appended at the end of the list.
    }

    public void DEBUG(){

        for(int i = 0; i < 7; i++){
                
            var td = new List<taskData>();
            td = histogram[i].GetTaskDatas();
            Debug.Log("histogram Index: " + (i) + " Step(" + ((i % 3)+1) + ")");
            Debug.Log("Length of histogram - GetTaskDatas() " + td.Count);

            foreach (taskData t in td){
                Debug.Log( "ID: " + t.getId() + " |REL: " + t.getRel() + " |DED: " + t.getDed() + " |WRK: " + t.getWrk() + " |INT: " + t.getIntensity() );
            }

        }

    }

    // Gets a graph state from the histogram list based on iteration and step
    public graphState GetGraphState(int iteration, int step)
    {
        // Subtract 1 at the end because list is 0 indexed
        int stateIndex = (((iteration-1)*3) + step)-1;

        graphState result;

        if(stateIndex < histogram.Count)
        {
            result = histogram[stateIndex];
        }
        else // Return null if next step doesn't exist
        {
            result = null;
        }

        return result;
    }
}
