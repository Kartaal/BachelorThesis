using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class TaskInput : MonoBehaviour
{
    private GameObject inputBox;
    private GameObject tt;

    private InputField[] input;
    private Toggle scheduleToggle;
    private Task task;

    // Start is called before the first frame update
    void Start()
    {
        inputBox = gameObject.transform.parent.parent.parent.Find("EditTask").gameObject;
        input = inputBox.GetComponentsInChildren<InputField>();
        scheduleToggle = inputBox.GetComponentInChildren<Toggle>();

    }

    //Method for displaying the input field on click
    public void DisplayInputField()
    {
        if (inputBox.active)
        {
            inputBox.SetActive(false);
            ResetFields();
        }
        else
        {
            tt = gameObject.transform.parent.parent.parent.Find("ToolTip").gameObject;

            SetListeners();
            task = this.GetComponent<Task>();

            RepositionInputField();
            tt.SetActive(false);
            inputBox.SetActive(true);
            SetUpInputFields();
        }

    }

    private void SetUpInputFields()
    {
        scheduleToggle.isOn = task.GetScheduled();
        input[0].placeholder.GetComponent<Text>().text = task.GetRelease().ToString();
        input[1].placeholder.GetComponent<Text>().text = task.GetDeadline().ToString();
        input[2].placeholder.GetComponent<Text>().text = task.GetWork().ToString();
        input[3].placeholder.GetComponent<Text>().text = task.GetIntensity().ToString();
    }

    //Same method as tooltip to set position of the inputfield
    private void RepositionInputField()
    {
        Task task = this.GetComponent<Task>();

        float x = (this.transform.position.x) + (0.5f * task.GetWidth());
        float y = (this.transform.position.y) + ((float)task.GetHeight());

        // Attempt at correctly positioning tooltip when resolution != 1920x1080
        //tt.transform.SetParent(task.gameObject.transform.parent);

        inputBox.transform.position = new Vector2(x, y);
    }

    //Method for updating the task and it's position when it has been edited
    private void UpdateTask()
    {
        
        task.SetDimensionsOfTask();
        task.SetPosition();
        RepositionInputField();
    }

    //Method for clearing all fields in the input, so old data isn't keep around when you click a new task
    private void ResetFields()
    {
        scheduleToggle.onValueChanged.RemoveAllListeners();
        foreach (InputField i in input)
        {
            i.text = "";
        }
    }

    //Setting listeners for when the user has edited fields
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


        scheduleToggle.onValueChanged.AddListener((value) => SubmitScheduled(value));

    }

    //***********SUBMIT METHODS**********************

    private void SubmitRelease(string arg0)
    {

        try
        {
            int result = Int32.Parse(arg0);
            task.SetRelease(result);

            task.CalcIntensity();
            UpdateTask();
           
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitDeadline(string arg0)
    {

        try
        {
            int result = Int32.Parse(arg0);
            task.SetDeadline(result);

            task.CalcIntensity();
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitWork(string arg0)
    {

        try
        {
            int result = Int32.Parse(arg0);

            task.SetWork(result);

            task.CalcIntensity();
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    private void SubmitIntensity(string arg0)
    {
        try
        {
            double result = Convert.ToDouble(arg0);

            task.SetIntensity(result);
            UpdateTask();
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{arg0}'");
        }
    }

    public void SubmitScheduled(bool value)
    {
        task.SetScheduled(value);
        UpdateTask();
    }



}
