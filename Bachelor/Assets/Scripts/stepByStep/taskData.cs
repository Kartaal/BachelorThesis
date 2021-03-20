using System.Collections;
using System.Collections.Generic;


public class taskData
{

    /*
        A data structurte saving the most important data from a task to circumvent
        monobehaviour dependnecies.
    */
    private int r,d,id;
    private double w;
    private double intensity;

    public taskData(int rel, int ded, double wrk, int identity, double intens){
        // creates a non-monobehviour object from the most important information found within
        // a task
        r = rel;
        d = ded;
        w = wrk;
        id = identity;
        intensity = intens; //default debug falue
    }

    public int getRel(){ return r; }
    public int getDed(){ return d; }

    public double getWrk(){ return w; } 

    public int getId(){return id;}

    public double getIntensity(){return intensity;}

    // Potentially useless thingies.
    public void setId(int i ){ id = i; }
    
    public void setRel(int i){ r = i; }
    public void setDed(int i ){ d = i;}

    public void setWrk(double i){ w = i; }

    public void setIntensity(double i){ intensity = i; }
}
