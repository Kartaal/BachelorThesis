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

    private string[] answersForQuestions;

    private string finalResult;



    // Start is called before the first frame update
    void Start()
    {
        resultOverlay.SetActive(false);
        currentQuestion = questions[0];
        questions[0].DisplayAllQuestionText();

        answersForQuestions = new string[questions.Length];
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
        /*foreach (Question q in questions)
        {
            if (q == currentQuestion)
            {
                Debug.Log(q.CheckAnswer());
            }
        }*/

        for (int i = 0; i < questions.Length; i++)
        {
            if (questions[i] == currentQuestion)
            {
                var res = questions[i].CheckAnswer();
                if (res != null)
                {
                    answersForQuestions[i] = res;
                    //finalResult += res + "\n";
                    //Debug.Log(finalResult);
                }

            }
        }
    }

    public void PrintAnswers()
    {
        foreach (string s in answersForQuestions)
        {
            finalResult += s + "\n ";
        }

        resultText.text = finalResult;
        resultOverlay.SetActive(true);

        Debug.Log(finalResult);
    }

    public void GoBackToQuestions() { resultOverlay.SetActive(false); }

    //Setter which is used when a Question is click to make that the current question.
    public void SetCurrentQuestion(Question newQuestion) { currentQuestion = newQuestion; }
}