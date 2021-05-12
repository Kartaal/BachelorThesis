using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphStateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    private static List<GraphState> histogram = new List<GraphState>();
    
    private static Schedule lastKnownSchedule = new Schedule();

    public int GetStatesCount()
    {
        return histogram.Count;
    }

    public int GetIterationCount()
    {
        return GetStatesCount() / 3;
    }

    public static void SaveState(List<Task> tl, Schedule s, IntervalData intDat)
    {
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

        var result = new GraphState(); // graphState Object to save

        result.SetInterval(intDat); // Sets the Max Intensity INterval for the graphState

        var res = new List<TaskData>(); // Prepares a list of TaskData, This is used for retracing to a previous stage.
        foreach(Task t in tl)
        {
            res.Add(new TaskData(t.GetRelease(), t.GetDeadline(), t.GetWork(), t.GetId(), t.GetIntensity(), t.GetScheduled(), t.GetStart(), t.GetDuration()));
        }

        result.SetTaskData(res); // Saves the TaskData List.

        // If the provided schedule is empty, then don't update. Otherwise, do UPDATE!
        if ( !(s == new Schedule()) )
        {
    	        lastKnownSchedule = s;
        }

        result.SetSchedule(lastKnownSchedule); // adds the last known schedule to the graphState.

        histogram.Add(result); // Add the data to the Histogram. Appended at the end of the list.
    }

    // Gets a graph state from the histogram list based on iteration and step
    public GraphState GetGraphState(int iteration, int step)
    {
        // Subtract 1 at the end because list is 0 indexed
        int stateIndex = (((iteration - 1) * 3) + step) - 1;

        GraphState result;

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

    // Method to setup GSH on a new scene (to avoid states persisting across scenes)
    public void InitGSH()
    {
        histogram.Clear();
        lastKnownSchedule = new Schedule();
    }
}
