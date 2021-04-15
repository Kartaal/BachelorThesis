using System.Collections.Generic;

public class IntervalData

{
    /*
    Data structure to help contain the information of a given interval
    including start, end and workload.

    This data structure's use is twofold. Each Interval is a representation of
    a timeframe between two points in time. Secondly it is used to represent
    workload / Intensity for a given timeframe depending on the tasks
    associated with said timeframe.
        */

    private int StartInt;                           // Integer denoting the start of the interval.
    private int EndInt;                             // Integer denoting the end of the interval 
    private double Intensity;                        // The calculated result of the intensity, distributed over the cause of the interval.
    private List<Task> Tasks = new List<Task>();    // List containing tasks that fit inside interval (when scheduled)

    public IntervalData(int s, int e)
    {
        //Marking Intervals
        StartInt = s;
        EndInt = e;
        Intensity = -1; //Default value. useful to spot errors with intensity calculations.
    }

    public int GetStartInt() { return StartInt; }
    public int GetEndInt() { return EndInt; }
    public double GetIntensity() { return Intensity; }

    public List<Task> GetTasks() { return Tasks; }

    public void AddTask(Task t)
    {
        Tasks.Add(t);
    }

    public void SetTasks(List<Task> newTasks)
    {
        Tasks = newTasks;
    }

    // Additional method for cleaning up tasks in an interval.
    public void RemoveAllTasks()
    {
        Tasks = new List<Task>();
    }

    public void CalcIntensity(double accWork)
    {
        /* 
            Calculates the workload, takes the sum of the workload recorded within the interval, 
            dividing it with the timespan of the interval. 
        */

        Intensity = accWork / (EndInt - StartInt);
    }

    //Debug method to convert contents to string.
    public override string ToString()
    {
        return "START: " + StartInt + " | " +
                "END: " + EndInt + " | " +
                "INTENSITY: " + Intensity;
    }

    // Outputs a string consisting of all tasks within this interval seperated by newlines.
    public string TasksToString()
    {
        string res = "";
        foreach (Task t in Tasks)
        {
            res += t.ToString() + "\n";
        }
        return res;
    }
}