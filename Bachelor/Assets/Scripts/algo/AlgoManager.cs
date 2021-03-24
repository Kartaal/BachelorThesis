using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoManager : MonoBehaviour
{
    [SerializeField]
    public List<Task> tasks = new List<Task>();

    private int iterationYDS = 1;
    private int stepYDS = 1;

    /* Getters */
    public int GetIterationYDS() { return iterationYDS; }
    public int GetStepYDS() { return stepYDS; }
    
    /* Setters */
    public void SetIterationYDS(int newIter)
    {
        iterationYDS = newIter;
    }

    public void SetStepYDS(int newStep)
    {
        stepYDS = newStep;
    }

    // Ensure that when becoming active, iteration and step are both 1
    private void Awake() 
    {
        iterationYDS = 1;
        stepYDS = 1;
    }
}
