class Program
{
    public static void Main()
    {
        ITaskManager taskManager = new TaskManager();
        string path = string.Empty;
        // get  cwd and "/Users/takoda/Projects/Ass3/Ass3/bin/Debug/net6.0/Ass3.pdb"  remove after bin
        string CWDpath = Directory.GetCurrentDirectory().Replace("/bin/Debug/net6.0", "/");
        // add a forward slash if not on end of cwdpath
        if (CWDpath[CWDpath.Length - 1] != '/')
        {
            CWDpath += "/";
        }
        bool tasksLoaded = false;
        while (true)
        {
            
            while (!tasksLoaded) {
                Console.WriteLine("Load tasks from a file \n");
                
                Console.Write("Enter the path to a text file: ");
                path = CWDpath + Console.ReadLine();
                if (File.Exists(path))                    {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string line in lines)
                    {
                        try
                        {
                            taskManager.AddTask(new Task(line));
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            
                             
                        } 
                        catch {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nError loading tasks. Please try again.\n");
                            Console.ResetColor();
                            break;
                        }
                        
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nTasks loaded successfully. \n");
                    Console.ResetColor();
                    tasksLoaded = true;
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nFile does not exist. Please try again.\n");
                    Console.ResetColor();
                    
                }
                
            }
            Console.WriteLine("\n1. Save tasks to a file");
            Console.WriteLine("2. Add a task");
            Console.WriteLine("3. Remove a task");
            Console.WriteLine("4. Change time needed for a task");
            Console.WriteLine("5. Find and save task sequence");
            Console.WriteLine("6. Find and save earliest commencement times");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            string? option = Console.ReadLine();

            switch (option)
            {

                case "1":
                    File.WriteAllLines(path, taskManager.GetTaskDetails());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tasks saved successfully to the same file.");
                    Console.ResetColor();
                    break;

                case "2":
                    Console.Write("Enter task details (Format - ID, Time, DependencyID1, DependencyID2, ...): ");
                    string? taskDetails = Console.ReadLine().ToUpper();
                    if (string.IsNullOrWhiteSpace(taskDetails))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No input. Please try again.");
                        Console.ResetColor();
                        break;
                    }
                    
                    var taskData = taskDetails.Split(',');
                    if (taskData.Length < 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please try again.");
                        Console.ResetColor();
                        break;
                    }
                    bool fail = false;
                    var taskId = taskData[0].Trim();
                     

                    if (taskManager.DoesTaskExist(taskId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Task already exists, Try again");
                        Console.ResetColor();
                        break;
                    }
                    if (!CheckFormat(taskData[0]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid ID input. ID should be a single letter followed by a number.");
                        Console.ResetColor();
                        break;
                    }
                    if (!int.TryParse(taskData[1].Trim(), out int timeNeeded) || timeNeeded <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid time input. Time should be a number greater than zero.");
                        Console.ResetColor();
                        break;

                    }

                    var taskDependencies = new string[taskData.Length - 2];
                    
                    for (int i = 2; i < taskData.Length; i++)
                    {
                        var dependencyId = taskData[i].Trim();
                        if (taskManager.DoesTaskExist(dependencyId))
                        {
                            taskDependencies[i - 2] = dependencyId;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"No task with ID {dependencyId} exists. Please try again.");
                            Console.ResetColor();
                            fail = true;
                        }
                    }
                    if (!fail)
                    {
                        var newTask = new Task(taskId, timeNeeded, taskDependencies);
                        taskManager.AddTask(newTask);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Task added successfully.");
                        Console.ResetColor();
                        break;
                    }

                    break;
                    

                case "3":
                    Console.Write("Enter task ID to remove: ");
                    string? taskID = Console.ReadLine().ToUpper();
                    
                    if (taskManager.DoesTaskExist(taskID))
                    {
                        taskManager.RemoveTask(taskID);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Task removed successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"No task with ID {taskID} exists.");
                        Console.ResetColor();
                    }
                    break;

                case "4":
                    Console.Write("Enter task ID to change time: ");
                    string taskIDToChange = Console.ReadLine();
                    Console.Write("Enter new time needed: ");

                    if (!int.TryParse(Console.ReadLine(), out int newTime) || newTime <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid time input. Time should be a number greater than zero.");
                        Console.ResetColor();
                        break;
                    }

                    if (taskManager.DoesTaskExist(taskIDToChange))
                    {
                        taskManager.ChangeTaskTime(taskIDToChange, newTime);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Time changed successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"No task with ID {taskIDToChange} exists.");
                        Console.ResetColor();
                    }
                    break;

                case "5":
                    string sequenceFile = CWDpath + "Sequence.txt";

                    string[] sequence = taskManager.FindTaskSequence();
                    if (File.Exists(sequenceFile))
                    {
                        File.Delete(sequenceFile);
                    }
                    File.WriteAllLines(sequenceFile, sequence);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task sequence saved to Sequence.txt successfully.");
                    Console.ResetColor();
                    break;

                case "6":
                    string earliestFile = CWDpath + "EarliestTimes.txt";

                    string[] earliestTimes = taskManager.FindEarliestCommencementTimes();
                    if (File.Exists(earliestFile))
                    {
                        File.Delete(earliestFile);
                    }
                    File.WriteAllLines(earliestFile, earliestTimes);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Commencement times saved to EarliestTimes.txt successfully.");
                    Console.ResetColor();
                    break;
                case "7":
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ResetColor();
                    break;
            }
        }
    }
    private static bool CheckFormat(string task) // check first letter is T and everything following is a number
    {
        if (task[0] == 'T')
        {
            if (task.Length <= 1)
            {
                return false;
            }
            for (int i = 1; i < task.Length; i++)
            {
                if (!char.IsDigit(task[i]))
                {
                    return  false;
                }
            }
            return true;
        }
        return false;

    }
}
