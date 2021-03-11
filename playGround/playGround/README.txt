PlayGround v1.1.0
Date: 8/03/21

Recent Changes:
Gave Task a bool field for completion.
Fixed consistency checker in TestLab.
Fixed YDS tests and made OA test.
Made (and preliminary tested) OA implementation.
============================================================================

This is a playgrund to test implementations of YDS and OA algorithms
The following is a short introductory guide on how to use this playground.

NOTE: It is still very much WIP :)
============================================================================
1. File structure.
============================================================================
The project is divided into 5 files.
- Program.cs (Don't touch)
Program is the launchpad of the entire project, there is no need to 
interfere with this. it provides setup for the project.

- Task.cs (Read and understand this. Might expand a lil later)
Task is a data structure definition to help model tasks as they may appear 
in their base componenets. Tasks are used to contain data about a given 
Task, including its release time, deadline and work requirements.

- TestLab.cs (WRITE TESTS HERE)
Testlab is where the tests are written. It also includes a 
template on how to implement a test case. It does NOT use Unit testing 
standards. (Yet)

- Worker.cs (All the Algorithms)
The Worker is where all the algorithms find their place. This is the file
you should use to implement all the algorithms.
TestLab calls the Worker with a specific algorithm and provides the 
un-sorted schedule.

- IntervalData (Data structure)
Contains the data with which intervals are defined and the tasks within the 
interval.

- Schedule.cs (Data Structure)
Contains the data for an output of an offline algorithm. Meaning
the intervals and intensities.

============================================================================
2. Things of Note.
============================================================================

[!] - Currently the tests only work with Offline algorithms. There is no way
to add tasks to the schedule apart from hardcoded into the 
Worker. (Which should be avoided)

[!] - Currently only the YDS algorithm is "Implemented".
USE AT YOUR OWN PERIL!

============================================================================
3. Changes
============================================================================
Initial :: [ 13/02/12 ]
> Added initial code. Might not work.

0.2 :: [ 15/02/21 ]
- Fixed the time interval calculations.
- Added bounded variables for random tasks.
- New and improved comments
+ Added a new class "IntervalData". Moved out from Worker class.

0.3 :: [ 17/02/21 ]
Tasks have gotten id field (auto incrementing).
TestLab has another test (test2), which generates a bunch of random value tasks (number in for loop = number of tasks).
Worker has gotten a method for sorting Task arrays (outputs list), using selection sort.
Implemented basic attempt at finding all tasks within maximum intensity interval.
Implemented basic debugging for writing out all tasks and the maximum intensity interval.]


0.4 :: [ 17/02/21 ]
Tasks calculate their own intensity (correctly, stupid int division).
Fixed assigning tasks to intervals.
Added task intensity to debugging output.

0.5 :: [ 17/02/21 ]
Put debugging output into separate functions.
Added Schedule class, with an object in Worker for keeping track of the final schedule.
Adding tasks and interval being added to schedule.
Added removal of scheduled tasks from list of unscheduled tasks.


0.6 :: [ 22/02/21 ]
Updated unscheduled task releasetimes and deadlines.
Implemented looping until no more unscheduled tasks.
Made another test case (test3).
Split code into 3 functions called Step1, Step2 and Step3.
YDS presumably completed...

0.7 :: [25/02/21]
Started refactoring the code...
Overwrote ToString for Task and IntervalData.
Cleaned up debug output (uses ToString).
Ensured tasks have a concept of their intensity when scheduled (so they know what speed they are to be run at).
IntervalData can contain the tasks within it.
Gave IntervalData setter and getter for tasks, also add single task.
Cleaned up task class.
Started cleaning up Schedule (so it doesn't contain tasks on top of intervals and has a ToString).

Lots of leftover comments (code) and Schedule ToString doesn't work so schedule debug output doesn't work.

0.7.1 :: [25/02/21]
Added ToString methods for Schedule and a more extensive ToString

Made a new SelecSort called SelecSortList based on Lists of tasks rather 
than array conversions

1.0 :: [1/3/21]
Improved overall readability of code and comments across the code.
Improved IntervalData and Schedule string outputs.
Added method in schedule for sorting the intervals (used in ScheduleToString).
TestLab tests renamed to reflect the algorithm they test.
Made YDS return a schedule.


1.0.1 :: [1/03/21]
Task has gotten a CommitWork method 
	(OA relevant, if being worked on 
		but not complete when new task is released)
Debug flag added to YDS (and submethods)
OA implemented (?)
	if idle time between tasks, will keep idle time (not end between 'runs')
    if no task has release time 1, it will start with idle time
Updated TestLab to account for debug flags (hard coded in YDS calls)