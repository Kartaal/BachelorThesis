using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Term : MonoBehaviour
{
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    string description = "";
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayDescription()
    {
        descriptionText.text = description;
        Debug.Log("Display description for term");
        descriptionText.enabled = true;
    }

    /*This is not needed since each term just updates its own description text
     If we need it for some reason the class needs to implement this interface: IDeselectHandler*/
    /*public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
        //descriptionText.enabled = false;
    }*/
}
