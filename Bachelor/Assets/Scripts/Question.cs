using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;


public class Question : MonoBehaviour, IDeselectHandler
{
    [SerializeField]
    private string[] answerOptions = new string[4];
    [SerializeField]
    private int corretAnswerIndex = -1; //What is the index of string answer in the answerOptions

    [SerializeField]
    private string question = ""; //The actual question
    [SerializeField]
    Text descriptionText;


    [SerializeField]
    private ToggleGroup answers;

    private Toggle[] answerOptionToggles;
    private Toggle theActiveToggle;


    // Start is called before the first frame update
    void Start()
    {
        answerOptionToggles = answers.GetComponentsInChildren<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayAllQuestionText()
    {
        ResetToggles();
        DisplayDescription();
        DisplayAnswers();
    }

    //Checks whether the currently select toggle (answer) is correct
    public void CheckAnswer()
    {
        theActiveToggle = answers.ActiveToggles().FirstOrDefault();

        if (theActiveToggle != null)
        {
            string answer = theActiveToggle.GetComponentInChildren<Text>().text;

            SetColourOfToggle(theActiveToggle, answer);
        }
    }

    //Displays the actual question text in the text area
    private void DisplayDescription()
    {
        descriptionText.text = question;
        descriptionText.enabled = true;
    }

    //Display the answer options for the this question in the toggles label.
    private void DisplayAnswers()
    {
        for (int i = 0; i < answerOptions.Length; i++)
        {
            answerOptionToggles[i].GetComponentInChildren<Text>().text = answerOptions[i];
        }
    }

    //Reset the toggles so when you click a new question, all toggles are white and none is selected.
    private void ResetToggles()
    {
        theActiveToggle = answers.ActiveToggles().FirstOrDefault();
        if (theActiveToggle != null) { theActiveToggle.isOn = false; }

        foreach (Toggle t in answerOptionToggles)
        {
            t.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    //Setting the colour of the toggle depending on whether it is correct or not
    //Green when correct, red when wrong.
    private void SetColourOfToggle(Toggle theActiveToggle, string answer)
    {
        if (Array.IndexOf(answerOptions, answer) == corretAnswerIndex)
        {
            theActiveToggle.GetComponentInChildren<Image>().color = new Color32(92, 197, 101, 255);
        }
        else
        {
            theActiveToggle.GetComponentInChildren<Image>().color = new Color32(198, 92, 92, 255);
        }
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
    }

    //public string GetQuestion() { return question; }

    //public int GetAnswer() { return answerIndex; }

    //public string[] GetAnswerOptions() { return answerOptions; }

    //public bool IsActive() { return isActive; }
}
