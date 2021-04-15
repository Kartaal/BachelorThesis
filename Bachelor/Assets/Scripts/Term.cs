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
    [Multiline]
    string description = "";
    // Start is called before the first frame update
    [SerializeField]
    GameObject imgContainer;

    [SerializeField]
    private int termImageID;

    private TermImageManager TIM;
    void Start()
    {
        descriptionText.enabled = false;
        TIM = gameObject.transform.parent.parent.Find("Images").GetComponent<TermImageManager>();
    }


    public void DisplayDescription()
    {
        descriptionText.text = description;
        Debug.Log("Display description for term");
        descriptionText.enabled = true;
        TIM.ActivateImage(termImageID); // Call to TIM to make image (if applicable) appear or dissapear.
    }

    /*This is not needed since each term just updates its own description text
     If we need it for some reason the class needs to implement this interface: IDeselectHandler*/
    /*public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
        //descriptionText.enabled = false;
    }*/

}
