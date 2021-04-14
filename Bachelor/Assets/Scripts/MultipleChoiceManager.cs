using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions = new Question[10];

    [SerializeField]
    private GameObject resultOverlay; 

    [SerializeField]
    private Text resultText;

    private Question currentQuestion;

    private (string,bool)[] answersForQuestions;

    // Used for setting text elements in scrollpane
    [SerializeField]
    private GameObject resultContainer;

    // Fields used for displaying and calculating the fraction of correct answers
    [SerializeField]
    Text fraction;
    int correctCount = 0;
    private bool displayingResults = false;

    private GameObject imagesQuestion4;


    // Start is called before the first frame update
    void Start()
    {
        resultOverlay.SetActive(false);
        currentQuestion = questions[0];
        questions[0].DisplayAllQuestionText();

        answersForQuestions = new (string,bool)[questions.Length];

        imagesQuestion4 = transform.parent.Find("Question4Images").gameObject;

    }


    /* When chosing an answer this method is called, in order to let the current 
     * questiong check wether the option is the correct answer.
     * 
     * The reason for having it here instead of Question, is that the toggles 
     * need a permant object to call a method on, so if we just have one of the Questions, 
     * (A)cas its object, it will always only check whether the the chosen anser correspondes to A's correct answer.
     */
    public void CheckingAnswer()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i] == currentQuestion)
            {
                var res = questions[i].CheckAnswer();
                if (res.Item1 != null)
                {
                    answersForQuestions[i] = res;
                }

            }
        }
    }

    //Method use to print result overview on the overlay
    public void PrintAnswers()
    {
        imagesQuestion4.SetActive(false);
        // Ensure we don't generate results when they already exist
        if(!displayingResults)
        {
            RemoveOldResultTexts();

            displayingResults = true;
            correctCount = 0;

            for (int i = 0; i < answersForQuestions.Length; i++)
            {
                // Making new text elements from resultText...
                GameObject textGO = Instantiate(resultText.gameObject, new Vector2(0,0), Quaternion.identity);

                // If answer is correct...
                if (answersForQuestions[i].Item2)
                {
                    textGO.GetComponent<Text>().text = "The answer for question " + (i+1) +  " is : " + answersForQuestions[i].Item1 + "\n      The answer is CORRECT";
                    correctCount++;
                }
                else // If answer is false...
                {
                    if (answersForQuestions[i].Item1 != null)
                    {
                        textGO.GetComponent<Text>().text = "The answer for question " + (i + 1) + " is : " + answersForQuestions[i].Item1 + "\n      The answer is WRONG";
                    }
                    else // If no answer was given
                    { 
                        textGO.GetComponent<Text>().text = "No answer given for question " + (i + 1); 
                    }
                    
                }

                // Make container parent of new text element
                textGO.transform.SetParent(resultContainer.transform);
                // resultText is inactive, so set new ones as active...
                textGO.SetActive(true);
                textGO.name = $"Question ({i+1})";
            }

            //resultText.text = finalResult;
            fraction.text = $"{correctCount}/{questions.Length} correct answers";
            resultOverlay.SetActive(true);
        }
    }

    //OnClick method for green button on the result overlay
    public void GoBackToQuestions()
    {
        DisplayImages();
        resultOverlay.SetActive(false);
        displayingResults = false;
    }

    //Setter which is used when a Question is click to make that the current question.
    public void SetCurrentQuestion(Question newQuestion)
    {
        currentQuestion = newQuestion;
        DisplayImages();

        var answerForToggle = answersForQuestions[Array.IndexOf(questions, currentQuestion)].Item1;

        //Needed for keeping user input
        currentQuestion.SetAnswerToggle(answerForToggle);
    }

    private void DisplayImages()
    {
        switch (currentQuestion.name)
        {
            case "Question (4)":
                imagesQuestion4.SetActive(true);
                break;
            default:
                imagesQuestion4.SetActive(false);
                break;
        }
    }

    private void RemoveOldResultTexts()
    {
        foreach (Transform tf in resultContainer.transform)
        {
            Destroy(tf.gameObject);
        }
    }
}