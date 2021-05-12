using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();

    [SerializeField]
    private GameObject taskPrefab;
    [SerializeField]
    private GameObject taskEditablePrefab;
    private int iterationYDS = 1;
    private int stepYDS = 1;

    /* Getters */
    public int GetIterationYDS() { return iterationYDS; }
    public int GetStepYDS() { return stepYDS; }
    
    /* Setters */
    public void SetIterationYDS(int newIter)
    {
        iterationYDS = newIter;
    }

    public void SetStepYDS(int newStep)
    {
        stepYDS = newStep;
    }

    // Ensure that when becoming active, iteration and step are both 1 and generate tasks
    private void Awake() 
    {
        iterationYDS = 1;
        stepYDS = 1;
    }

    public void GenerateLockedYDSTasks()
    {
        // Make lists of task (releases, deadlines, works)
        List<(int, int, double)> taskReleasesDeadlinesWork = new List<(int, int, double)>();
        taskReleasesDeadlinesWork.Add((1, 3, 5.0));
        taskReleasesDeadlinesWork.Add((2, 5, 12.0));
        taskReleasesDeadlinesWork.Add((4, 6, 3.0));
        taskReleasesDeadlinesWork.Add((6, 8, 10.0));
        taskReleasesDeadlinesWork.Add((8, 10, 5.0));
        taskReleasesDeadlinesWork.Add((10, 12, 7.0));
        taskReleasesDeadlinesWork.Add((7, 9, 8.0));


        Transform canvasTransform = gameObject.transform.parent.gameObject.transform;
        //Transform taskContainerTransform = canvasTransform.Find("TaskContainer").transform;
        var tmp = canvasTransform.Find("OutputContainer").transform;
        Transform taskContainerTransform = tmp.Find("TaskContainer").transform;

        foreach (Transform trans in taskContainerTransform)
        {
            trans.gameObject.SetActive(false);
        }

        int taskCount = taskReleasesDeadlinesWork.Count;

        // Generate tasks and assign field values
        for (int i = 0 ; i < taskCount ; i++)
        {
            GameObject taskGO = Instantiate(taskPrefab, this.transform.position, this.transform.rotation);
            Task task = taskGO.GetComponent<Task>();
            task.SetId(i);
            task.SetRelease(taskReleasesDeadlinesWork[i].Item1);
            task.SetDeadline(taskReleasesDeadlinesWork[i].Item2);
            task.SetWork(taskReleasesDeadlinesWork[i].Item3);

            tasks.Add(task);

            taskGO.name = $"Task ({i})";

            taskGO.transform.SetParent(taskContainerTransform);
        }
    }

    public void GenerateDIYYDSTasks()
    {
        // Make lists of task (releases, deadlines, works)
        List<(int, int, double)> taskReleasesDeadlinesWork = new List<(int, int, double)>();
        taskReleasesDeadlinesWork.Add((1, 5, 3.0));
        taskReleasesDeadlinesWork.Add((3, 5, 14.0));
        taskReleasesDeadlinesWork.Add((5, 7, 10.0));
        taskReleasesDeadlinesWork.Add((6, 8, 7.0));
        taskReleasesDeadlinesWork.Add((11, 12, 7.0));
        taskReleasesDeadlinesWork.Add((9, 12, 5.0));
        taskReleasesDeadlinesWork.Add((7, 10, 2.0));


        Transform canvasTransform = gameObject.transform.parent.gameObject.transform;
        var tmp = canvasTransform.Find("OutputContainer").transform;
        Transform taskContainerTransform = tmp.Find("TaskContainer").transform;

        foreach (Transform trans in taskContainerTransform)
        {
            trans.gameObject.SetActive(false);
        }

        int taskCount = taskReleasesDeadlinesWork.Count;

        // Generate tasks and assign field values
        for (int i = 0; i < taskCount; i++)
        {
            GameObject taskGO = Instantiate(taskEditablePrefab, this.transform.position, this.transform.rotation);
            Task task = taskGO.GetComponent<Task>();
            task.SetId(i);
            task.SetRelease(taskReleasesDeadlinesWork[i].Item1);
            task.SetDeadline(taskReleasesDeadlinesWork[i].Item2);
            task.SetWork(taskReleasesDeadlinesWork[i].Item3);

            tasks.Add(task);

            taskGO.name = $"Task ({i})";

            taskGO.transform.SetParent(taskContainerTransform);
        }
    }
    
}
