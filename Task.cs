public class Task : ITask
{
    public string TaskID { get; private set; }

    public int TimeNeeded { get; private set; }

    public string[] Dependencies { get; private set; }

    public int DependencyCount { get; private set; }


    public Task(string taskInput)
    {
        string[] parts = taskInput.Split(',');
        TaskID = parts[0].Trim();
        TimeNeeded = int.Parse(parts[1].Trim());
        DependencyCount = parts.Length - 2; // subtract the first two elements (TaskID and TimeNeeded)
        Dependencies = new string[DependencyCount];

        for (int i = 0; i < DependencyCount; i++)
        {
            Dependencies[i] = parts[i + 2].Trim(); // +2 to skip TaskID and TimeNeeded
        }
    }
    public Task(string taskID, int timeNeeded)
    {
        TaskID = taskID;
        TimeNeeded = timeNeeded;
        Dependencies = new string[0];
        DependencyCount = 0;
    }

    public Task(string taskID, int timeNeeded, string[] dependencies)
    {
        TaskID = taskID;
        TimeNeeded = timeNeeded;
        Dependencies = dependencies;
        DependencyCount = dependencies.Length;
    }

    public void AddDependency(string dependencyID)
    {
        
            string[] newDependencies = new string[DependencyCount + 1];
            for (int i = 0; i < DependencyCount; i++)
            {
                newDependencies[i] = Dependencies[i];
            }
            newDependencies[DependencyCount] = dependencyID;
            Dependencies = newDependencies;
            DependencyCount++;
        
    }

    public void RemoveDependency(string dependencyID)
    {   
        if (DependencyCount >= 1)
        {
            Console.WriteLine("Removing dependency");
            Console.WriteLine($"DependencyCount: {DependencyCount}");
            Console.WriteLine($"Task: {TaskID}");
            string[] newDependencies = new string[(DependencyCount - 1)];
            int index = 0;
            for (int i = 0; i < DependencyCount - 1; i++)
            {
                if (!(Dependencies[i] == dependencyID))
                {
                    newDependencies[index] = Dependencies[i];
                    index++;
                }
            }
            Dependencies = newDependencies;
            DependencyCount--;
        }
    }

    public void ChangeTimeNeeded(int newTimeNeeded)
    {
        TimeNeeded = newTimeNeeded;
    }
    
}
