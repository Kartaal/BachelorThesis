using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject tt; //shorthand for tooltip
    [SerializeField]
    private Task taskData; // reference to Task.cs script attatched to this object for data.
    [SerializeField]
    private Text wrkTXT, relTXT, dedTXT;
    private Text IDTXT;


    void Start()
    {
        tt = gameObject.transform.Find("TextBlock").gameObject;
        taskData = gameObject.GetComponent<Task>();
        
        wrkTXT = tt.transform.Find("WrkValue").GetComponent<Text>();
        relTXT = tt.transform.Find("RelValue").GetComponent<Text>();
        dedTXT = tt.transform.Find("DedValue").GetComponent<Text>();
        IDTXT = tt.transform.Find("idText").GetComponent<Text>();
        initializeToolTipInformation();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log("Mouse Enter");
        tt.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData ped){
        Debug.Log("Mouse Exit");
        tt.SetActive(false);
    }

}
