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

}
