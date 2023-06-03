# CAB301 ALGORITHMS AND COMPLEXITY - Assignment 3

**Due Date:** 2 June 2023  
**Weighting:** 40%  
**Group or Individual:** Individual

## Introduction
This assignment focuses on applying your knowledge of data structures and algorithms to develop a real-world software project management system. You will be required to identify significant Abstract Data Types (ADTs) in the application, design and implement these ADTs, and use them to build the project management system. Additionally, you will need to apply algorithms to solve computational problems arising from the ADT implementations and application building.

## Task Summary
1. Information and Software Requirements
   - Develop a project management system to manage a list of tasks in a project.
   - Tasks may have dependencies on other tasks.
   - Task information, including task ID, time needed to complete the task, and task dependencies, is stored in a text file.
2. Microsoft Console Application
   - Create a command-line menu for the project management system.
   - Allow the user to:
     - Enter the name of a text file and read task information from it.
     - Add a new task with its dependencies to the project.
     - Remove a task from the project.
     - Change the completion time of a task.
     - Save the updated task information back to the input text file.
     - Find a sequence of tasks that satisfies all dependencies and save it to a text file.
     - Find the earliest possible commencement time for each task and save the solution to a text file.
3. Assignment Requirements
   - Use C# as the programming language.
   - Create a console application using Microsoft Visual Studio 2022 (Community Edition).

## Submission Guidelines
- Create a single zip file named `your-student-number.zip` containing your entire console application project.
- Do not submit any other documents.
- The submitted archive must be in standard .zip format. Other formats like .7z, .rar, .gz, etc., will not be accepted.
- Submit your assignment through the CAB301 Canvas website. Email submissions are not accepted.
- You can submit your assignment multiple times before the deadline.

## Tests
| Type     | Test Case                                                            | Expected Outcome                                                                                    | Pass |
|----------|----------------------------------------------------------------------|----------------------------------------------------------------------------------------------------|------|
| Positive | Enter a valid text file name                                         | The system should correctly load the tasks from the text file                                      | 1    |
| Negative | Enter an invalid or non-existent text file name                      | The system should throw an error message or ask for the file name again                           | 1    |
| Negative | Add a new task with no time                                          | The system should throw an error message                                                          | 1    |
| Negative | Invalid TaskID, valid time                                           | Error Message                                                                                      | 1    |
| Negative | Invalid Time, Valid taskID                                           | Error Message                                                                                      | 1    |
| Positive | Add a new task with no dependencies                                  | The system should successfully add the task and the task should appear in the list of tasks        | 1    |
| Negative | Add a task with a valid time and invalid Dependency format            | Error Message                                                                                      | 1    |
| Positive | Add a new task with dependencies that exist in the system             | The system should successfully add the task and correctly associate it with its dependencies       | 1    |
| Negative | Add a new task with dependencies that do not exist in the system      | The system should not allow the addition of the task and throw an error or warning message         | 1    |
| Positive | Remove a task that exists in the system                               | The task should be successfully removed from the system, and no other tasks should depend on it    | 1    |
| Negative | Remove a task that does not exist in the system                        | The system should throw an error message stating that the task does not exist                       | 1    |
| Positive | Change the time of a task that exists in the system                    | The system should successfully update the task's time                                              | 1    |
| Negative | Change the time of a task that does not exist in the system             | The system should throw an error message stating that the task does not exist                       | 1    |
| Positive | Save after making valid changes                                       | The system should successfully update the text file with the new task details                      | 1    |
| Positive | Run the sequence-finding operation on a set of tasks with valid dependencies           | The system should return a valid sequence that satisfies all dependencies                        | 1    |
| Negative | Run the sequence-finding operation on a set of tasks with circular dependencies       | The system should return an error or a warning about the impossible sequence                     | 0    |
| Positive | Run the commencement time-finding operation on a set of tasks with valid dependencies  | The system should return the earliest commencement times for each task correctly                  | 1    |
| Negative | Run the commencement time-finding operation on a set of tasks with circular dependencies  | The system should return an error or a warning about the impossible commencement times            | 0    |
