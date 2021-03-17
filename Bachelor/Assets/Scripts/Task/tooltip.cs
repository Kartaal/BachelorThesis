using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private GameObject tt; //shorthand for tooltip
    
    private Task taskData; // reference to Task.cs script attatched to this object for data.
    
    private Text wrkTXT, relTXT, dedTXT, IDTXT;


    void Start()
    {
        tt = gameObject.transform.Find("TextBlock").gameObject;
        taskData = gameObject.GetComponent<Task>();
        
        //initializes Text Fields so they can be edited.
        wrkTXT = tt.transform.Find("WrkValue").GetComponent<Text>();
        relTXT = tt.transform.Find("RelValue").GetComponent<Text>();
        dedTXT = tt.transform.Find("DedValue").GetComponent<Text>();
        IDTXT = tt.transform.Find("idText").GetComponent<Text>();
        initializeToolTipInformation();
    }

    //Called once to update ID and all fields to initial task values.
    private void initializeToolTipInformation(){
        IDTXT.text = "ID-" + taskData.GetId();
        UpdateToolTipInformation();
    }

    //can be called from anywhere to update all fields of text.
    public void UpdateToolTipInformation(){
        wrkTXT.text = "" + taskData.GetWork();
        relTXT.text = "" + taskData.GetRelease();
        dedTXT.text = "" + taskData.GetDeadline();
    }

/*
    Using unity's event system to record mouse interactions for tooltips popup
*/
    public void OnPointerEnter(PointerEventData ped){
        tt.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData ped){
        tt.SetActive(false);
    }

}
