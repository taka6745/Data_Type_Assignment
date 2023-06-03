public class TaskManager : ITaskManager
{
    private ITask[] tasks;
    private int taskCount;

    public TaskManager()
    {
        tasks = new ITask[0];
        taskCount = 0;
    }

    public void AddTask(ITask task)
    {
        for (int i = 0; i < taskCount; i++)
        {
            if (tasks[i].TaskID == task.TaskID)
                return;
        }

        ITask[] newTasks = new ITask[taskCount + 1];
        for (int i = 0; i < taskCount; i++)
        {
            newTasks[i] = tasks[i];
        }
        newTasks[taskCount] = task;
        tasks = newTasks;
        taskCount++;
    }
    

    public void RemoveTask(string taskID)
    {
        ITask ? taskToRemove = null;
        for (int i = 0; i < taskCount; i++)
        {
            if (tasks[i].TaskID == taskID)
            {
                taskToRemove = tasks[i];
                break;
            }
        }

        if (taskToRemove != null)
        {
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i].RemoveDependency(taskID);
            }

            ITask[] newTasks = new ITask[taskCount - 1];
            int index = 0;
            for (int i = 0; i < taskCount; i++)
            {
                if (tasks[i].TaskID != taskID)
                {
                    newTasks[index] = tasks[i];
                    index++;
                }
            }
            tasks = newTasks;
            taskCount--;
        }
    }

    public void ChangeTaskTime(string taskID, int newTime)
    {
        for (int i = 0; i < taskCount; i++)
        {
            if (tasks[i].TaskID == taskID)
            {
                tasks[i].ChangeTimeNeeded(newTime);
                break;
            }
        }
    }


    public string[] GetTaskDetails()
{
    string[] details = new string[taskCount];
    for (int i = 0; i < taskCount; i++)
    {
        string detail = $"{tasks[i].TaskID}, {tasks[i].TimeNeeded}";
        if(tasks[i].Dependencies.Length > 0)
        {
            string dependencyIds = String.Join(", ", tasks[i].Dependencies);
            detail += $", {dependencyIds}";
        }
        details[i] = detail;
    }
    return details;
}

    public bool DoesTaskExist(string taskID)
    {
        foreach (var task in tasks)
        {
            if (task.TaskID == taskID)
            {
                return true;
            }
        }

        return false;
    }




    // FindEarliestCommencementTimes() and FindTaskSequence() will be left blank 
    // for now as they require DFS and Topological Search implementations.
    // Find the earliest possible commencement time for each task in the project
    public string[] FindEarliestCommencementTimes()
    {
        int[] commencementTimes = new int[taskCount];
        bool[] visited = new bool[taskCount];

        for (int i = 0; i < taskCount; i++)
        {
            if (!visited[i])
                DFS(i, visited, commencementTimes);
        }

        string[] result = new string[taskCount];
        for (int i = 0; i < taskCount; i++)
        {
            result[i] = $"{tasks[i].TaskID}, {commencementTimes[i]}";
        }
        return result;
    }

    private void DFS(int taskIndex, bool[] visited, int[] commencementTimes)
    {
        visited[taskIndex] = true;
        ITask task = tasks[taskIndex];

        foreach (string dependencyId in task.Dependencies)
        {
            if (dependencyId != null)
            {
                int index = IndexOf(dependencyId);
                if (!visited[index])
                    DFS(index, visited, commencementTimes);

                commencementTimes[taskIndex] = Math.Max(commencementTimes[taskIndex], commencementTimes[index] + tasks[index].TimeNeeded);
            }
        }
    }

    private int IndexOf(string taskID)
    {
        for (int i = 0; i < taskCount; i++)
        {
            if (tasks[i].TaskID == taskID)
                return i;
        }
        return -1; // not found
    }

    // Find a sequence of tasks that satisfies all dependencies
    private int[] TopologicalSort()
    {
        int[] stack = new int[taskCount];
        int top = -1;
        bool[] visited = new bool[taskCount];

        for (int i = 0; i < taskCount; i++)
        {
            if (!visited[i])
                top = TopologicalSortUtil(i, visited, stack, top);
        }

        // reverse the array
        int start = 0, end = top;
        while (start < end)
        {
            int temp = stack[start];
            stack[start] = stack[end];
            stack[end] = temp;
            start++;
            end--;
        }

        return stack;
    }

    private int TopologicalSortUtil(int taskIndex, bool[] visited, int[] stack, int top)
{
    visited[taskIndex] = true;

    foreach (string dependencyID in tasks[taskIndex].Dependencies)
    {
        if (dependencyID != null)
        {
            int index = IndexOf(dependencyID);
            if (!visited[index])
                top = TopologicalSortUtil(index, visited, stack, top);
        }
    }

    // Shift all elements one position to the right to make room at the beginning
    for (int i = top; i >= 0; i--)
    {
        stack[i + 1] = stack[i];
    }

    // Insert the task at the beginning of the stack
    stack[0] = taskIndex;
    return top + 1;
}


    public string[] FindTaskSequence()
{
    bool[] visited = new bool[taskCount];
    int[] stack = new int[taskCount];
    int top = -1;

    for (int i = 0; i < taskCount; i++)
    {
        if (!visited[i])
            top = TopologicalSortUtil(i, visited, stack, top);
    }

    string[] result = new string[taskCount];
    for (int i = 0; i <= top; i++)
    {
        // Read the stack in reverse order
        result[i] = tasks[stack[top - i]].TaskID;
    }

    return result;
}




}
