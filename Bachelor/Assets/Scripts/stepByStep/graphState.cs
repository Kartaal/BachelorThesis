using System.Collections;
using System.Collections.Generic;

public class GraphState
{

    /*
        A Data class holding all the information of the graph in a given point in time
        This data can be used to retrace and repaint a graph from an earlier
        iteration in time.
    */

    private List<TaskData> taskData;    // Contains the taskData of the graph at a given point in time.
    private IntervalData maxIntensity;  // A reference to the max intensity interval of the given iteration.

    private Schedule schedule;          // A Reference to the already calculated schedule.

    public GraphState()
    {
        taskData = new List<TaskData>(); // safety, not sure if there is dependencies on this.
        schedule = new Schedule(); // NESSECARY, avoids null pointer exceptions
    }

    private void Awake() 
    {
        taskData = new List<TaskData>(); // safety, not sure if there is dependencies on this.
        schedule = new Schedule(); // NESSECARY, avoids null pointer exceptions
    }

    public void SetTaskData(List<TaskData> tl) { taskData = tl; }
    public void SetSchedule(Schedule s) {schedule = s; }
    public void SetInterval(IntervalData intdat) { maxIntensity = intdat; }

    public Schedule GetSchedule() { return schedule; }
    public List<TaskData> GetTaskDatas() { return taskData; }
    public IntervalData GetInterval() { return maxIntensity; }
}