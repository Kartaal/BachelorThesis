using System;
using System.Collections.Generic;
using System.Text;

public class Schedule
{
    /*
        Schedule is a data structure containing the output of an offline algorithm.
        Meaning the sorted schedule of intensities.
        */
    private List<IntervalData> Intervals;

    public Schedule()
    {
        Intervals = new List<IntervalData>();
    }

    public void AddInterval(IntervalData intervalToAdd)
    {
        Intervals.Add(intervalToAdd);
    }

    public List<IntervalData> GetIntervals() { return Intervals; }

    // Outputs string of the ToString output of all intervals in schedule
    public override string ToString()
    {
        string tmp = "";
        foreach (IntervalData id in Intervals)
        {
            tmp += id.ToString() + "\n ============================== \n";
        }
        return tmp;
    }

    // Outputs a string of all the tasks in the schedule
    public string ScheduleToString()
    {
        string res = "";

        SortIntervals();
        foreach (IntervalData id in Intervals)
        {
            res += id.TasksToString();
        }

        return res;
    }

    //uses selection sort to sort based on end of an interval. (Earliest Deadline First)
    private void SortIntervals()
    {
        int n = Intervals.Count;
        IntervalData tmp = null;
        int min = -1;

        for (int i = 0; i < n; i++)
        {
            min = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Intervals[j].GetEndInt() < Intervals[min].GetEndInt())
                {
                    min = j;
                }
            }
            tmp = Intervals[i];
            Intervals[i] = Intervals[min];
            Intervals[min] = tmp;
        }
    }

    //Returns a list of all tasks in the schedule
    public List<Task> GetTaskList()
    {
        List<Task> output = new List<Task>();
        foreach(IntervalData i in Intervals)
        {
            foreach(Task t in i.GetTasks())
            {
                output.Add(t);
            }
        }

        return output;
    }

    // merges another schedule into this one.
    // Meant as a support method for retracability using the graphStateHandler
    public void MergeSchedules(Schedule s){
        foreach(IntervalData idat in s.GetIntervals()){
            Intervals.Add(idat);
            // Ensure consistency / make sure there are no duplicates.
        }
    }

}
