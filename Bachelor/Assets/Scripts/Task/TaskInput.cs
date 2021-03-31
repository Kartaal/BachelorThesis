using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class TaskInput : MonoBehaviour
{
    private GameObject inputBox;
    private GameObject tt;

    private InputField[] input;
    private Task task;
    // Start is called before the first frame update
    void Start()
    {
        inputBox = gameObject.transform.parent.parent.Find("EditTask").gameObject;
        input = inputBox.GetComponentsInChildren<InputField>();
       
    }

    public void DisplayInputField()
    {
        if (inputBox.active)
        {
            inputBox.SetActive(false);
        }
        else
        {
            tt = gameObject.transform.parent.parent.Find("ToolTip").gameObject;

            SetListeners();
            task = this.GetComponent<Task>();

            RepositionInputField();
            tt.SetActive(false);
            inputBox.SetActive(true);
        }
        
    }

   
    private void RepositionInputField()
    {
        Task task = this.GetComponent<Task>();

        float x = (this.transform.position.x) + (0.5f * task.GetWidth());
        float y = (this.transform.position.y) + ((float)task.GetHeight());

        // Attempt at correctly positioning tooltip when resolution != 1920x1080
        //tt.transform.SetParent(task.gameObject.transform.parent);

        inputBox.transform.position = new Vector2(x, y);
    }


    private void UpdateTask()
    {
        task.SetDimensionsOfTask();
        task.SetPosition();
        RepositionInputField();
    }

    private void SetListeners()
    {
        var seRelease = new InputField.SubmitEvent();
        seRelease.AddListener(SubmitRelease);
        input[0].onEndEdit = seRelease;

        var seDeadline = new InputField.SubmitEvent();
        seDeadline.AddListener(SubmitDeadline);
        input[1].onEndEdit = seDeadline;

        var seWork = new InputField.SubmitEvent();
        seWork.AddListener(SubmitWork);
        input[2].onEndEdit = seWork;

        var seIntensity = new InputField.SubmitEvent();
        seIntensity.AddListener(SubmitIntensity);
        input[3].onEndEdit = seIntensity;
    }


    private void SubmitRelease(string arg0)
    {
        var inputText = input[0].GetComponentInChildren<Text>().text;

        try
        {
            int result = Int32.Parse(inputText);
            task.SetRelease(result);
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitDeadline(string arg0)
    {
        var inputText = input[1].GetComponentInChildren<Text>().text;

        try
        {
            int result = Int32.Parse(inputText);

            task.SetDeadline(result);
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitWork(string arg0)
    {
        var inputText = input[2].GetComponentInChildren<Text>().text;

        try
        {
            int result = Int32.Parse(inputText);

            task.SetWork(result);
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitIntensity(string arg0)
    {
        var inputText = input[3].GetComponentInChildren<Text>().text;

        try
        {
            double result = Convert.ToDouble(inputText);

            task.SetIntensity(result);
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }
}
