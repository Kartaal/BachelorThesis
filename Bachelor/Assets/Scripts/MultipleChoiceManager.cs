using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions = new Question[5];

    private Question currentQuestion;




    // Start is called before the first frame update
    void Start()
    {
        questions[0].DisplayAllQuestionText();
    }

    // Update is called once per frame
    void Update()
    {
       
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
        foreach (Question q in questions)
        {
            if (q == currentQuestion)
            {
                q.CheckAnswer();
            }
        }
    }

    //Setter which is used when a Question is click to make that the current question.
    public void SetCurrentQuestion(Question newQuestion) { currentQuestion = newQuestion; }

}
