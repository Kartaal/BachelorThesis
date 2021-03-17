using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject tt; //shorthand for tooltip

    void Start()
    {
        tt = gameObject.transform.Find("TextBlock").gameObject;
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData ped){
        Debug.Log("Mouse Enter");
        tt.SetActive(true);
    }
    
    public void OnPointerExit(PointerEventData ped){
        Debug.Log("Mouse Exit");
        tt.SetActive(false);
    }
     /*
 
     public void OnPointerEnter(PointerEventData eventData)
     {
         mouse_over = true;
         Debug.Log("Mouse enter");
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         mouse_over = false;
         Debug.Log("Mouse exit");
     }
     
     */

}
