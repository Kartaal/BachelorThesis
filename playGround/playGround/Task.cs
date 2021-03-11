using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace playGround
{
    public class Task
    {
        /* 
         A data structure denoting a task. 
         Containing three values denoting: Work, release times and deadlines.
        */

        //Constant variables for bounded randomness.
        private const int wrkMin = 1;
        private const int wrkMax = 11;
        private const int relMin = 1;
        private const int relMax = 10;
        private const int dedMax = 11;

        private int id = idCount++;
        private int  releaseT, deadlineT;
        private double taskIntensity = 0.0;
        private float workT;
        private bool complete;

        //Static variable to denote ID numbers across Tasks. DO NOT TOUCH
        private static int idCount = 0;

        // Generates a task with random work,release and deadlines. Using bounded randomness.
        public Task() {
            var rnd = new Random();
            workT = rnd.Next(wrkMin, wrkMax);
            releaseT = rnd.Next(relMin, relMax);
            deadlineT = rnd.Next((releaseT+1), dedMax);
        }

        /* Creates a task with predefined variables. 
           WARNING: It is possible to enter invalid tasks. USE WITH CAUTION.
           Work is float, but parameter is integer, this is fine. 
         */
        public Task(int w, int r, int d)
        {
            workT = w;
            releaseT = r;
            deadlineT = d;
        }

        public int GetRelMin() { return relMin; }
        public int GetId() { return id; }
        public float GetWork() { return workT; }
        public int GetRelease() { return releaseT; }
        public int GetDeadline() { return deadlineT; }
        public double GetIntensity() { return taskIntensity; }
        public bool GetComplete() { return complete; }

        public void SetComplete(bool newComplete) { complete = newComplete; }

        public void SetDeadline(int newDeadline)
        {
            deadlineT = newDeadline;
        }
        public void SetRelease(int newRelease)
        {
            releaseT = newRelease;
        }

        public void SetIntensity(double newIntensity) { taskIntensity = newIntensity; }

        /* 
         * Takes the remaining amount of time that can be used to work (between 0 and 1)
         * If amount of work done in remaining time is less than work load, 
         *        update work load and set remaining time to 0
         *        Increment release time to indicate work has been done but not completed 
         *                (ensures task gets added to tasks to be scheduled)
         * If amount of work done in remaining time is more than work load,
         *        update remaining time and set work load to 0 and complete to true
         * Always return remaining time after committing work, 
         *      this is needed for working on other tasks
        */
        public float CommitWork(float timeRemaining)
        {
            // Can't complete this task
            if(timeRemaining * taskIntensity < workT)
            {
                workT -= timeRemaining * (float) taskIntensity;
                releaseT++;
                timeRemaining = 0f;
            }
            else // More work can be done after this task
            {
                timeRemaining -= workT / (float) taskIntensity;
                workT = 0f;
                complete = true;
            }
            return timeRemaining;
        }

        public override string ToString()
        {
            return "ID: " + id + " | " + 
                   "WORKLOAD: " + workT + " | " +
                   "RELEASE: " + releaseT + " | " +
                   "DEADLINE: " + deadlineT + " | " +
                   "INTENSITY: " + taskIntensity;
        }

    }
}

/*
 a job denoted by I
has a release time R:I when it arrives
a work requirement W:I
and a Deadline D:I 
if I runs in S constant time
it will be completed by

W:I / S
     */
