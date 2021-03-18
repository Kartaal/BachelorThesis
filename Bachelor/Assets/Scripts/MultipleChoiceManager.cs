using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MultipleChoiceManager : MonoBehaviour
{
    [SerializeField]
    private Question[] questions = new Question[5];

    [SerializeField]
    private Toggle[] answerOptions;

    [SerializeField]
    private ToggleGroup answers;

    private Question currentQuestion;




    // Start is called before the first frame update
    void Start()
    {
        answerOptions = answers.GetComponentsInChildren<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DisplayAnswers()
    {
        foreach (Question q in questions)
        {
            Toggle theActiveToggle = answers.ActiveToggles().FirstOrDefault();
            if(theActiveToggle != null) { theActiveToggle.isOn = false; }
            
            foreach (Toggle t in answerOptions)
            {
                t.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
            }
            if (q.IsActive())
            {
                currentQuestion = q;
                var temp = q.GetAnswerOptions();
                for (int i = 0; i < temp.Length; i++)
                {
                    answerOptions[i].GetComponentInChildren<Text>().text = temp[i];
                }
            }
        }
    }

    public void CheckAnswer()
    {
        Toggle theActiveToggle = answers.ActiveToggles().FirstOrDefault();
        if (theActiveToggle != null)
        {
            string answer = theActiveToggle.GetComponentInChildren<Text>().text;

            foreach (Question q in questions)
            {
                if (q == currentQuestion)
                {
                    if (Array.IndexOf(q.GetAnswerOptions(), answer) == q.GetAnswer())
                    {
                        theActiveToggle.GetComponentInChildren<Image>().color = new Color32(92, 197, 101, 255);
                    }
                    else
                    {
                        theActiveToggle.GetComponentInChildren<Image>().color = new Color32(198, 92, 92, 255);
                    }
                }
            }
        }
    }

}
