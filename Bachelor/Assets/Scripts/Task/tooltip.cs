using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private GameObject tt; //shorthand for tooltip
    
    private Task taskData; // reference to Task.cs script attatched to this object for data.
    
    private Text wrkTXT, relTXT, dedTXT, IDTXT, intesityTXT;


    void Start()
    {
        tt = gameObject.transform.parent.parent.Find("ToolTip").gameObject;
        taskData = gameObject.GetComponent<Task>();

        initializeToolTipInformation();
    }

    //Called once to update ID and all fields to initial task values.
    private void initializeToolTipInformation(){
        // Creates references for the Text fields. So values can be edited. Should only be called when initializing.
        wrkTXT = tt.transform.Find("WrkValue").GetComponent<Text>();
        relTXT = tt.transform.Find("RelValue").GetComponent<Text>();
        dedTXT = tt.transform.Find("DedValue").GetComponent<Text>();
        IDTXT = tt.transform.Find("idText").GetComponent<Text>();
        IDTXT.text = "ID-" + taskData.GetId();
        intesityTXT = tt.transform.Find("IntensityValue").GetComponent<Text>();
        // Calls the Update method to overwrite default values
    }

    // Can be called from anywhere to update all fields of text.
    // All data is obtained from the associated task object (monobehaviour)
    public void UpdateToolTipInformation(){
        wrkTXT.text = "" + taskData.GetWork();
        relTXT.text = "" + taskData.GetRelease();
        dedTXT.text = "" + taskData.GetDeadline();
        intesityTXT.text = "" + taskData.GetIntensity();
    }

/*
    Using unity's event system to record mouse interactions for tooltips popup
*/
    public void OnPointerEnter(PointerEventData ped){
        UpdateToolTipInformation();
        RepositionToolTip();
        tt.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData ped){
        tt.SetActive(false);
    }

    // Function for repositioning the singular tooltip element relative to the hovered task
    private void RepositionToolTip()
    {
        Task task = this.GetComponent<Task>();

        float x = (this.transform.position.x) + (0.5f * task.GetWidth());
        float y = (this.transform.position.y) + ((float) task.GetHeight());

        // Attempt at correctly positioning tooltip when resolution != 1920x1080
        //tt.transform.SetParent(task.gameObject.transform.parent);

        tt.transform.position = new Vector2(x, y);
    }

}
