using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;


public class Worker : MonoBehaviour
{
    private GraphStateHandler gsh; // unit that stores data for every step of an iteration

    void Start() 
    {
        gsh = gameObject.transform.GetComponent<GraphStateHandler>();
    }

    /* 
        Creates a worker unit. The worker is the engine for the algorithms, 
        and all algorithms should be present here 
        The Worker plans out the schedule and runs it. 
        Returning statistics for analysis.
    */

    public void RunYDS(UnityEngine.Object taskList)
    {
        //do YDS things
        
        Schedule schedule = new Schedule();
        
        //schedule = YDS();
    }

    public Schedule YDS(List<Task> tasks, int debugFlag)
    {
        // Mounts the non-sorted Schedule and ready for action.
        // Plans out the Schedule to YDS algorithm.

        Console.WriteLine("YDS reporting for duty!");

        // Step 1. Calculate max intensity interval
        Console.WriteLine("PHASE 1: Preparing intensity...");

        Schedule schedule = new Schedule();


        IntervalData maxIntensityInterval = null;

        while ( tasks.Count > 0 )
        { 
            // Calculate maximum intensity interval for this iteration
            maxIntensityInterval = Step1(tasks);

            // Debug print out!!
            Console.WriteLine("\n===============================");
            Console.WriteLine("Interval of max intensity is:");
            Console.WriteLine(maxIntensityInterval);
            Console.WriteLine("===============================");

            // Step 2. Sort, rearrange and schedule
            Console.WriteLine("PHASE 2: Sorting Schedule...");
            // Scheduling tasks from maximum intensity interval
            Step2(tasks, schedule, maxIntensityInterval);

            // Step 3. Remove max intensity interval from instance - Is done? or repeat from step 1?
            // Update instance after scheduling tasks
            Step3(tasks, maxIntensityInterval);

            Console.WriteLine("===============================");
            Console.WriteLine("End of iteration.....");
            AllTasksDebugOutput(tasks);
            Console.WriteLine("===============================");
        }

        Console.WriteLine();

        Console.WriteLine("Schedule in YDS iteration interval order...");
        Console.WriteLine(schedule.ScheduleToString());

        // Only works when ScheduleToString() has been called, otherwise it is not sorted.
        return schedule;
    }

    private static IntervalData Step1(List<Task> tasks)
    {
        // List of recorded intervals initialized from tasks. 
        // Updated in the second loop of phase 1 with accumulated workloads. 
        List<IntervalData> intervals = new List<IntervalData>();

        // Helper variable to keep track of total work load in a given interval.
        double accumulatedWork = 0;


        Console.WriteLine("Initializing Intervals, stand by . . .");
        List<int> intervalLimits = new List<int>();

        // Initializes the intervals as Interval Data.
        foreach (Task t in tasks)
        {
            if (!intervalLimits.Contains(t.GetDeadline()))
            {
                intervalLimits.Add(t.GetDeadline());

            }
            if (!intervalLimits.Contains(t.GetRelease()))
            {
                intervalLimits.Add(t.GetRelease());
            }
        }

        // Sort as prework to making intervals
        intervalLimits.Sort();

        // Make intervals by ordering, end of interval is at least next number in sequence so start+1
        for (int i = 0; i < intervalLimits.Count; i++)
        {
            int start = intervalLimits[i];
            // For each start, find every viable interval end
            for (int j = i + 1; j < intervalLimits.Count; j++)
            {
                int end = intervalLimits[j];
                intervals.Add(new IntervalData(start, end));
            }
        }

        Console.WriteLine("Total Intervals recorded: " + intervals.Count + "\nCalculaitng intensities, please stand by . . .");

        /* Calculates the intensity for any given interval.
            Any task with a release date equal to or after 
            the start of the interval is considered part of the interval.
            The same goes for any task with a deadline that has a deadline
            before the end of the interval. Regardless of start position.
        */
        foreach (IntervalData id in intervals)
        {
            foreach (Task t in tasks)
            {
                /* Make sure task is run within interval
                    Task has release at least t1 (interval start) and deadline at most t2 (interval end)
                */

                if (t.GetRelease() >= id.GetStartInt() && t.GetDeadline() <= id.GetEndInt())
                {
                    accumulatedWork += t.GetWork();
                }
            }
            id.CalcIntensity(accumulatedWork);
            accumulatedWork = 0;
        }

        // Find the interval of maximum intensity
        IntervalData maxIntensityInterval = new IntervalData(0, 0);
        foreach (IntervalData id in intervals)
        {
            if (id.GetIntensity() > maxIntensityInterval.GetIntensity())
            {
                maxIntensityInterval = id;
            }
        }

        //------------------------
        // the new Schedule should be empty, as it is handled by the SaveState implicitly. 
        // see GraphStatehandler.
        GraphStateHandler.SaveState(tasks, new Schedule() , maxIntensityInterval);
        //------------------------

        return maxIntensityInterval;
    }

    private void Step2(List<Task> tasks, Schedule schedule, IntervalData maxIntensityInterval)
    {
        foreach (Task t in tasks)
        {
            /* Start off by ensuring that the data is within the interval.
                Task has release at least t1 (interval start) and deadline at most t2 (interval end) 
                taskRelease >= intervalStart && taskDeadline <= intervalEnd  
            */

            if (t.GetRelease() >= maxIntensityInterval.GetStartInt() && t.GetDeadline() <= maxIntensityInterval.GetEndInt())
            {
                maxIntensityInterval.AddTask(t);
            }
        }

        maxIntensityInterval.SetTasks(SelecSortList(maxIntensityInterval.GetTasks()));

        //Adding max intensity interval and the scheduled tasks to the final Schedule
        schedule.AddInterval(maxIntensityInterval);

        // Tell tasks what their intensity (CPU speed) should be
        foreach (Task t in maxIntensityInterval.GetTasks())
        {
            t.SetIntensity(maxIntensityInterval.GetIntensity());
            t.SetScheduled(true);

            t.SetStart(t.GetRelease());
            double duration = t.GetWork() / t.GetIntensity();
            t.SetDuration(duration);
        }

        GraphStateHandler.SaveState(tasks, schedule, maxIntensityInterval);

        
    }

    private static void Step3(List<Task> tasks, IntervalData maxIntensityInterval)
    {
        //Removed scheduled tasks from the list of tasks yet to be scheduled
        foreach (Task t in maxIntensityInterval.GetTasks())
        {
            tasks.Remove(t);
        }

        //Running through non-scheduled tasks to update deadline and release
        foreach (Task t in tasks)
        {
            //If Deadline is within the interval set the deadline to start of maxintensity interval
            if (t.GetDeadline() > maxIntensityInterval.GetStartInt() && t.GetDeadline() <= maxIntensityInterval.GetEndInt())
            {
                t.SetDeadline(maxIntensityInterval.GetStartInt());
                // Called to ensure task area (workload visual) remains the same
                t.CalcIntensity();
            }

            //If Release is within the interval set the release to the end of maxintesity interval
            if (t.GetRelease() >= maxIntensityInterval.GetStartInt() && t.GetRelease() < maxIntensityInterval.GetEndInt())
            {
                t.SetRelease(maxIntensityInterval.GetEndInt());
                // Called to ensure task area (workload visual) remains the same
                t.CalcIntensity();
            }
        }

        
        //------------------------
        // the new Schedule should be empty, as it is handled by the SaveState implicitly. 
        // see graphStateHandler.cs
        GraphStateHandler.SaveState(tasks, new Schedule() , maxIntensityInterval);
        // MaxIntensityInterval is saved, but has no real reason to be used for this step!
        //------------------------
    }

    public List<(Schedule, int)> OA(List<Task> tasks)
    {
        Console.WriteLine("OA reporting for duty!");

        Schedule schedule = new Schedule();

        List<(Schedule, int)> allSchedulesWithCurrentTime = new List<(Schedule, int)>();

        int currentTime = 1;
        int endTime = 100; // Non-dynamic EndTime



        // Main loop, runs across idle time...
        while (currentTime < endTime)
        {
            List<Task> YDSTasks = new List<Task>();
            bool newTaskAdded = false;
            double timeRemaining = 1f;

            List<Task> prevScheduledTasks = schedule.GetTaskList();

            // Debuggy stuff
            Console.WriteLine($"Current time: {currentTime}");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            Console.WriteLine("Writing out tasks before work committed by OA");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            AllTasksDebugOutput(prevScheduledTasks);


            // Commit work on not completed tasks that were scheduled
            // TODO: when no new task added, completed tasks are left over in schedule and thus "worked" on.
            foreach (Task t in prevScheduledTasks)
            {
                if (!t.GetComplete())
                {
                    /*
                        * Ensure tasks that were pushed to later by YDS don't get worked on and have their release pushed further
                        */
                    if (!(t.GetRelease() > currentTime))
                    {
                        // Assignment to update timeRemaining
                        timeRemaining = t.CommitWork(timeRemaining, currentTime);
                    }
                }
            }

            // Debuggy stuff
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            Console.WriteLine("Writing out tasks after work committed by OA");
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            AllTasksDebugOutput(prevScheduledTasks);


            // Add incomplete tasks that were already scheduled to next list to be 
            foreach (Task t in prevScheduledTasks)
            {
                if (!t.GetComplete())
                {
                    YDSTasks.Add(t);
                }
            }

            // For all incomplete tasks with release at current time, add them to tasks to be scheduled
            // TODO: ENSURE THIS DOESN'T RUN EVERY TIME STEP
            foreach (Task t in tasks)
            {
                if ((t.GetRelease() == currentTime) && !t.GetComplete())
                {
                    if (!YDSTasks.Contains(t)) // Ensure already scheduled tasks don't get added again
                    {
                        YDSTasks.Add(t);
                        newTaskAdded = true;
                    }
                }
            }

            // If tasks were added to be scheduled, actually schedule them
            if (newTaskAdded)
            {
                schedule = YDS(YDSTasks, 2);
                newTaskAdded = false;
            }

            currentTime++;

            var tuple = (schedule, currentTime);
            allSchedulesWithCurrentTime.Add(tuple);

            // Debuggy stuff
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        return allSchedulesWithCurrentTime;
    }

    /*
        OA run 1 (does not know about run 2)
        Task 1-1: 2-5
        Task 1-2: 2-6

        OA run 2 (does not know about run 1)
        Task 2-1: 7-8
        Task 2-2: 8-10
*/

    private static void TasksAndScheduledDebugOutput(List<Task> tasks, List<Task> toBeScheduledTasks)
    {
        AllTasksDebugOutput(tasks);

        Console.WriteLine("Writing out max intensity interval tasks....");
        for (int x = 0; x < toBeScheduledTasks.Count; x++)
        {
            Console.WriteLine(toBeScheduledTasks[x]);
        }
    }

    private static void AllTasksDebugOutput(List<Task> tasks)
    {
        Console.WriteLine("Writing out all tasks.....");

        for (int x = 0; x < tasks.Count; x++)
        {
            Console.WriteLine(tasks[x]);
        }
        Console.WriteLine("=============================");
    }

    private List<Task> SelecSortList(List<Task> lt) {
        List<Task> res = lt;
        int n = lt.Count;
        Task tmp = null;
        int min = -1;

        for (int i = 0; i < n; i++) {
            min = i;
            for (int j = i + 1; j < n; j++) {
                if (res[j].GetDeadline() < res[min].GetDeadline()) {
                    min = j;
                }
            }
            tmp = res[i];
            res[i] = res[min];
            res[min] = tmp;
        }

        return res;
    }

}