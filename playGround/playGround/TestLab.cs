using System;
using System.Collections.Generic;
using System.Text;

namespace playGround
{
    class TestLab
    {
        /*
            The test lab is where we can play around with tests.
            Write individual tests in seperate methods (See testYDS1() for details)
            create a new schedule by making a List<Task> of Tasks and add each task
            individually, you may automate the process using loops if you so desire.

            You can make tasks in 2 different ways currently.

            Task() = A task with bounded randomness (see Task to edit theese values)
            Task(W,R,D) = A task with set workloads, release times and deadlines.

            To run a test, simply call the worker (W) and the algorithm you wish to run.
            Example: W.YDS(Tasklist);

            You may want to run the consistency check prior to running your test as it 
            checks if any task has a deadline that lies behind the release time to avoid
            impossible tasks.
             */

        private Worker W;

        public TestLab(Worker w) {
            W = w; //assign Worker.

            //testYDS1();
            //testYDS2();
            //testYDS3();
            //testYDSX();

            testOA1();
        }

        public void testYDS1() {
            Console.WriteLine("Running testYDS1.........");
            //Template, make list, add elements, check consistency then run.
            List<Task> sampleSchedule = new List<Task>();
            //sampleSchedule.Add(new Task(2, 4, 8)); //workload first, release second, deadline third
            sampleSchedule.Add(new Task(5,1,2));
            sampleSchedule.Add(new Task(6,2,4));
            sampleSchedule.Add(new Task(7,4,7));

            if (ConsistencyCheck(sampleSchedule))
            {
                W.YDS(sampleSchedule, 2);
            }
            else
            {
                Console.WriteLine("CONSISTENCY ERROR!");
            }
        }

        public void testYDS2()
        {
            Console.WriteLine("Running testYDS2.........");
            //Write tests here
            List<Task> s = new List<Task>();

            for (int x = 0; x < 10 ; x++) 
            {
                s.Add(new Task());
            }

            if (ConsistencyCheck(s)) 
            { 
                W.YDS(s, 2); 
            }
            else 
            { 
                Console.WriteLine("CONSISTENCY ERROR!"); 
            }
        }
        public void testYDS3()
        {
            Console.WriteLine("Running testYDS3.........");
            List<Task> sampleSchedule = new List<Task>();
            //sampleSchedule.Add(new Task(2, 4, 8)); //workload first, release second, deadline third
            sampleSchedule.Add(new Task(5, 1, 3));
            sampleSchedule.Add(new Task(6, 4, 7));
            sampleSchedule.Add(new Task(3, 4, 6));
            sampleSchedule.Add(new Task(10, 6, 9));
            sampleSchedule.Add(new Task(5, 3, 5));
            sampleSchedule.Add(new Task(7, 4, 7));
            sampleSchedule.Add(new Task(9, 7, 10));
            sampleSchedule.Add(new Task(2, 6, 11));
            sampleSchedule.Add(new Task(4, 1, 3));
            sampleSchedule.Add(new Task(1, 2, 9));

            if (ConsistencyCheck(sampleSchedule))
            {
                W.YDS(sampleSchedule, 2);
            }
            else
            {
                Console.WriteLine("CONSISTENCY ERROR!");
            }
        }

        public void testYDSX()
        {
            Console.WriteLine("Running testX.........");
            //Write tests here
            List<Task> s = new List<Task>();
            s.Add(new Task());
            if (ConsistencyCheck(s)) { W.YDS(s,2); }
            else { Console.WriteLine("CONSISTENCY ERROR!"); }
        }

        public void testOA1()
        {
            Console.WriteLine("Running testOA1.........");
            List<Task> sampleSchedule = new List<Task>();
            //sampleSchedule.Add(new Task(2, 4, 8)); //workload first, release second, deadline third
            sampleSchedule.Add(new Task(5, 1, 3));
            sampleSchedule.Add(new Task(6, 4, 7));
            sampleSchedule.Add(new Task(3, 4, 6));
            sampleSchedule.Add(new Task(10, 6, 9));
            sampleSchedule.Add(new Task(5, 3, 5));
            sampleSchedule.Add(new Task(7, 4, 7));
            sampleSchedule.Add(new Task(9, 7, 10));
            sampleSchedule.Add(new Task(2, 6, 11));
            sampleSchedule.Add(new Task(4, 1, 3));
            sampleSchedule.Add(new Task(1, 2, 9));

            if (ConsistencyCheck(sampleSchedule))
            {
                W.OA(sampleSchedule);
            }
            else
            {
                Console.WriteLine("CONSISTENCY ERROR!");
            }
        }


        public bool ConsistencyCheck(List<Task> l)
        {
            /*  
                Debug tool to double check whether or not a given schedule is actually viable for sorting.
                Currently only checks for a deadline being earlier than a release time or in the same time unit.
            */
            foreach (Task t in l)
            {
                if (t.GetDeadline() <= t.GetRelease()) { return false; }
                if (t.GetRelease() < t.GetRelMin()) { return false; }
            }
            return true;
        }
    }

}
