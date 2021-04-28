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
    IEnumerator Start()
    {
        descriptionText.enabled = false;
        TIM = gameObject.transform.parent.parent.Find("Images").GetComponent<TermImageManager>();

        // Without this yield return, the DisplayDescription call doesn't work properly
        yield return null;
        // Auto "clicks" the first term
        if(gameObject.name == "Term(1)")
        {
            Debug.Log("Start running DisplayDescription for first term");
            DisplayDescription();
            GetComponent<Button>().Select();
        }
    }


    public void DisplayDescription()
    {
        descriptionText.text = description;
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
