﻿using System.Collections;
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


    void Awake()
    {
        answerOptionToggles = answers.GetComponentsInChildren<Toggle>();
    }

    public void DisplayAllQuestionText()
    {
        ResetToggles();
        DisplayDescription();
        DisplayAnswers();
    }

    //Checks whether the currently select toggle (answer) is correct
    public string CheckAnswer()
    {
        theActiveToggle = answers.ActiveToggles().FirstOrDefault();

        if (theActiveToggle != null)
        {
            string answer = theActiveToggle.GetComponentInChildren<Text>().text;

            return GenerateResult(theActiveToggle, answer);
        }

        return null;
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
    private string GenerateResult(Toggle theActiveToggle, string answer)
    {
        if (Array.IndexOf(answerOptions, answer) == corretAnswerIndex)
        {
            //theActiveToggle.GetComponentInChildren<Image>().color = new Color32(92, 197, 101, 255);
            string res = this.gameObject.GetComponent<Text>().text + "'s answer is: " + answer + " This is correct";

            return res;
        }
        else
        {
            //theActiveToggle.GetComponentInChildren<Image>().color = new Color32(198, 92, 92, 255);

            string res = this.gameObject.GetComponent<Text>().text + "'s answer is: " + answer + " This is wrong";

            return res;
        }
    }

    public void OnDeselect(BaseEventData data)
    {
        Debug.Log("Deselected");
    }

}