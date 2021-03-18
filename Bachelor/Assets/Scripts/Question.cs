using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Question : MonoBehaviour, IDeselectHandler
{
    [SerializeField]
    string[] answerOptions = new string[4];

    [SerializeField]
    private string question = "";

    [SerializeField]
    private int answer = -1;

    [SerializeField]
    Text descriptionText;

    private bool isActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayDescription()
    {
        descriptionText.text = question;
        Debug.Log("Display description for term");
        descriptionText.enabled = true;
        isActive = true;
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
        isActive = false;
    }

    public string GetQuestion() { return question; }

    public int GetAnswer() { return answer; }

    public string[] GetAnswerOptions() { return answerOptions; }

    public bool IsActive() { return isActive; }
}
