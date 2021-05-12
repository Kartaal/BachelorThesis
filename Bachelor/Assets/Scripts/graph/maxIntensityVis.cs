using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maxIntensityVis : MonoBehaviour
{
    [SerializeField]
    private Slider leftInterval, rightInterval;

    // Call this method to visually define the maximum intensity interval from an IntervalData object.
    // This object may be found within the Worker.cs.
    public void IntervalDataToVisual(IntervalData intd)
    {
        leftInterval.value = intd.GetStartInt();
        rightInterval.value = intd.GetEndInt();
    }

    // Call this method to visually define the maximum intensity interval from two numbers.
    
    public void IntegersToVisual(int startInt, int endInt)
    {
        leftInterval.value = startInt;
        rightInterval.value = endInt;
        ConsistincyCheck();
    }

    // Internal method to ensure consistincy of the visual elements. If the left is larger than right
    // Defaults both values to the value of  the left.
    private void ConsistincyCheck()
    {
        if (rightInterval.value < leftInterval.value )
        {
            rightInterval.value = leftInterval.value;
        }

    }

}
