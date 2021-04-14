using System.Collections;
using System.Collections.Generic;


public class TaskData
{

    /*
        A data structurte saving the most important data from a task to circumvent
        monobehaviour dependnecies.
    */
    private int r,d,id;
    private double w;
    private double intensity;
    private bool scheduled;
    private double start, runDuration;

    public TaskData(int rel, int ded, double wrk, int identity, double intens, bool sche, double strt, double dur)
    {
        // creates a non-monobehviour object from the most important information found within
        // a task
        r = rel;
        d = ded;
        w = wrk;
        id = identity;
        scheduled = sche;
        intensity = intens; //default debug falue
        start = strt;
        runDuration = dur;
    }

    /* Getters */
    public int GetRel() { return r; }
    public int GetDed() { return d; }

    public double GetWrk() { return w; } 

    public int GetId() { return id; }

    public double GetIntensity() { return intensity; }
    public bool GetScheduled() { return scheduled; }
    public double GetStart() { return start; }
    public double GetDuration() { return runDuration; }

    
    /* Setters */
    public void SetId(int i ) { id = i; }
    
    public void SetRel(int i) { r = i; }
    public void SetDed(int i ) { d = i;}

    public void SetWrk(double i) { w = i; }

    public void SetIntensity(double i) { intensity = i; }
    public void SetScheduled(bool s) { scheduled = s; }
    public void SetStart(double newStart) { start = newStart; }
    public void SetDuration(double newDuration) { runDuration = newDuration; }
}
