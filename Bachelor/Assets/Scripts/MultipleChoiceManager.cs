using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions = new Question[5];

    [SerializeField]
    private GameObject resultOverlay; 

    [SerializeField]
    private Text resultText;

    private Question currentQuestion;

    private (string,bool)[] answersForQuestions;

    private string finalResult;



    // Start is called before the first frame update
    void Start()
    {
        resultOverlay.SetActive(false);
        currentQuestion = questions[0];
        questions[0].DisplayAllQuestionText();

        answersForQuestions = new (string,bool)[questions.Length];
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
        for (int i = 0; i < answersForQuestions.Length; i++)
        {
            if (answersForQuestions[i].Item2)
            {
                finalResult += "The answer for question " + (i+1) +  " is : " + answersForQuestions[i].Item1 + "\n      The answer is CORRECT \n\n";
            }
            else
            {
                if (answersForQuestions[i].Item1 != null)
                {
                    Debug.Log(answersForQuestions[i].Item1 );
                    finalResult += "The answer for question " + (i + 1) + " is : " + answersForQuestions[i].Item1 + "\n      The answer is WRONG \n\n";
                }
                else { finalResult += "No answer given for question " + (i + 1) + "\n\n"; }
                
            }
        }

        resultText.text = finalResult;
        resultOverlay.SetActive(true);

        Debug.Log(finalResult);
    }

    //OnClick method for green button on the result overlay
    public void GoBackToQuestions()
    {
        finalResult = "";
        resultOverlay.SetActive(false);
    }

    //Setter which is used when a Question is click to make that the current question.
    public void SetCurrentQuestion(Question newQuestion)
    {
        currentQuestion = newQuestion;
        var answerForToggle = answersForQuestions[Array.IndexOf(questions, currentQuestion)].Item1;

        //Needed for keeping user input
        currentQuestion.SetAnswerToggle(answerForToggle);
    }
}