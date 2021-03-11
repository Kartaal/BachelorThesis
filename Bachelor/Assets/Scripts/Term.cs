using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Term : MonoBehaviour, IDeselectHandler
{
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    string description = "";
    // Start is called before the first frame update
    void Start()
    {
        descriptionText.text = description;
        descriptionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayDescription()
    {
        Debug.Log("Display description for term");
        descriptionText.enabled = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
        descriptionText.enabled = false;
    }
}
