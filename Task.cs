public class Task : ITask
{
    public string TaskID { get; set; }
    public int TimeNeeded { get; set; }
    public ITask[] Dependencies { get; set; }
    private int dependencyCount;

    public Task()
    {
        Dependencies = new ITask[1]; // Initiate with size 1
        dependencyCount = 0;
    }

    // Constructor to parse from a line of text
    public Task(string line)
    {
        Dependencies = new ITask[1]; // Initiate with size 1
        dependencyCount = 0;

        var parts = line.Split(',');
        TaskID = parts[0].Trim();
        TimeNeeded = int.Parse(parts[1].Trim());

        for (int i = 2; i < parts.Length; i++)
        {
            string dependencyID = parts[i].Trim();
            AddDependency(new Task { TaskID = dependencyID });
        }
    }

    private int GetIndex(string taskID)
    {
        for (int i = 0; i < dependencyCount; i++)
        {
            if (Dependencies[i].TaskID == taskID)
            {
                return i;
            }
        }
        return -1;
    }

    public bool AddDependency(ITask task)
    {
        if (task == null)
            return false;

        if (FindDependency(task.TaskID) != null)
            return false;

        // Increase size of array if it's full
        if (dependencyCount == Dependencies.Length)
        {
            var newDependencies = new ITask[Dependencies.Length + 1];
            for (int i = 0; i < Dependencies.Length; i++)
            {
                newDependencies[i] = Dependencies[i];
            }
            Dependencies = newDependencies;
        }

        Dependencies[dependencyCount] = task;
        dependencyCount++;
        return true;
    }

    public bool RemoveDependency(ITask task)
    {
        if (task == null)
            return false;

        int index = GetIndex(task.TaskID);
        if (index == -1)
            return false;

        // Shift all tasks after the removed one
        for (int i = index; i < dependencyCount - 1; i++)
        {
            Dependencies[i] = Dependencies[i + 1];
        }

        // Nullify the last task
        Dependencies[dependencyCount - 1] = null;
        dependencyCount--;
        return true;
    }

    public ITask FindDependency(string taskID)
    {
        int index = GetIndex(taskID);
        return index == -1 ? null : Dependencies[index];
    }

    public void ChangeTimeNeeded(int newTimeNeeded)
    {
        TimeNeeded = newTimeNeeded;
    }
}
