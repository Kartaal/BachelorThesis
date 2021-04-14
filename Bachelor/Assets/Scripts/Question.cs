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
    private Sprite imageForQuestion;

    [SerializeField]
    private string question = ""; //The actual question
    [SerializeField]
    Text descriptionText;
    [SerializeField]
    Text questionTitle;
    [SerializeField]
    Image questionImageField;


    [SerializeField]
    private ToggleGroup answers;

    private Toggle[] answerOptionToggles;
    private Toggle theActiveToggle;

    private string currentAnswer = "";

    void Awake()
    {
        answerOptionToggles = answers.GetComponentsInChildren<Toggle>();
    }

    // PUBLIC METHODS

    public void DisplayAllQuestionText()
    {
        ResetToggles();
        DisplayDescription();
        DisplayAnswers();

        if (imageForQuestion != null)
        {
            questionImageField.gameObject.SetActive(true);
            questionImageField.sprite = imageForQuestion;
        }
        else
        {
            questionImageField.gameObject.SetActive(false);
        }
        
    }

    //Checks whether the currently select toggle (answer) is correct
    public (string,bool) CheckAnswer()
    {
        theActiveToggle = answers.ActiveToggles().FirstOrDefault();

        if (theActiveToggle != null)
        {
            currentAnswer = theActiveToggle.GetComponentInChildren<Text>().text;

            return GenerateResult(theActiveToggle);
        }

        return (null,false);
    }


    //Needed to save the user answer, so when they go back and forth they can see what they previously put in. 
    public void SetAnswerToggle(string answer)
    {
        foreach (Toggle t in answerOptionToggles)
        {
            if (t.GetComponentInChildren<Text>().text == answer)
            {
                t.isOn = true;
            }
        }
    }

    public void OnDeselect(BaseEventData data)
    {
        // Debug.Log("Deselected");
    }

    // PRIVATE METHODS

    //Displays the actual question text in the text area
    private void DisplayDescription()
    {
        descriptionText.text = question;
        questionTitle.text = this.name;
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

    /*Generating the result which check answer should return to the multipleChoiceManager in order
    *To save the answer.
    *The bool is given so that multiplechoice manager can print the correct statement in result.
    *Maybe this could be kept in CheckAnswers.
    */
    private (string,bool) GenerateResult(Toggle theActiveToggle)
    {
        if (Array.IndexOf(answerOptions, currentAnswer) == corretAnswerIndex)
        {
            string res =  currentAnswer;

            return (res,true);
        }
        else
        {
            string res = currentAnswer;

            return (res,false);
        }
    }


}