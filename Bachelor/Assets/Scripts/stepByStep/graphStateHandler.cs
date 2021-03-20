﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class graphStateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<graphState> histogram;

    private static Schedule lastKnownSchedule;

    void Start()
    {
        lastKnownSchedule = new Schedule();
        histogram = new List<graphState>();
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
            -(3) The Schdeule is updated from the last known variant of the schedule 
            in graphStatHandler (this one). And written down.
        */

        var result = new graphState(); // graphState Object to save

        result.SetMaxIntensity(intDat); // Sets the Max Intensity INterval for the graphState

        var res = new List<taskData>(); // Prepares a list of TaskData, This is used for retracing to a previous stage.
        foreach(Task t in tl){
            res.Add(new taskData(t.GetRelease(), t.GetDeadline(), t.GetWork(), t.GetId()));
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
}
