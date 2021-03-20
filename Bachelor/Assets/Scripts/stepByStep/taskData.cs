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

    public taskData(int rel, int ded, double wrk, int i){
        // creates a non-monobehviour object from the most important information found within
        // a task
        r = rel;
        d = ded;
        w = wrk;
        id = i;
    }

    public int getRel(){ return r; }
    public int getDed(){ return d; }

    public double getWrk(){ return w; } 

    public int getId(){return id;}

    // Potentially useless thingies.
    public void setId(int i ){ id = i; }
    
    public void setRel(int i){ r = i; }
    public void setDed(int i ){ d = i;}

    public void setWrk(double i){ w = i; }

}
