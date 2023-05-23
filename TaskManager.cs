using System.IO;
using System.Text;

public class TaskManager : ITaskManager
{
    private ITask[] Tasks;
    private int TaskCount;

    public TaskManager()
    {
        Tasks = new ITask[1]; // Initiate with size 1
        TaskCount = 0;
    }

    private int GetIndex(string taskID)
    {
        for (int i = 0; i < TaskCount; i++)
        {
            if (Tasks[i].TaskID == taskID)
            {
                return i;
            }
        }
        return -1;
    }

    public void AddTask(ITask task)
    {
        if (GetIndex(task.TaskID) != -1) 
            return; // do not add if task with the same ID already exists

        if (TaskCount == Tasks.Length)
        {
            var newTasks = new ITask[Tasks.Length + 1];
            for (int i = 0; i < Tasks.Length; i++)
            {
                newTasks[i] = Tasks[i];
            }
            Tasks = newTasks;
        }

        Tasks[TaskCount] = task;
        TaskCount++;
    }

    public void RemoveTask(string taskID)
    {
        int index = GetIndex(taskID);
        if (index == -1)
            return;

        for (int i = index; i < TaskCount - 1; i++)
        {
            Tasks[i] = Tasks[i + 1];
        }
        Tasks[TaskCount - 1] = null;
        TaskCount--;
    }

    public string[] FindEarliestCommencementTimes()
    {
        // Make sure dependencies are reflected in the tasks
        foreach (ITask task in Tasks)
        {
            foreach (ITask dependency in task.Dependencies)
            {
                dependency.AddDependency(task);
            }
        }

        // DFS
        int[] earliestTimes = new int[TaskCount];
        bool[] visited = new bool[TaskCount];

        for (int i = 0; i < TaskCount; i++)
        {
            if (!visited[i])
            {
                DFS(i, earliestTimes, visited);
            }
        }

        // Save to file
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < TaskCount; i++)
        {
            sb.AppendLine(Tasks[i].TaskID + ", " + earliestTimes[i]);
        }
        File.WriteAllText("EarliestTimes.txt", sb.ToString());

        // Return as string array
        return sb.ToString().Split('\n');
    }

    private void DFS(int taskIndex, int[] earliestTimes, bool[] visited)
    {
        visited[taskIndex] = true;
        foreach (ITask dependency in Tasks[taskIndex].Dependencies)
        {
            int dependencyIndex = GetIndex(dependency.TaskID);
            if (!visited[dependencyIndex])
            {
                DFS(dependencyIndex, earliestTimes, visited);
            }
            earliestTimes[taskIndex] = Math.Max(earliestTimes[taskIndex], earliestTimes[dependencyIndex] + Tasks[dependencyIndex].TimeNeeded);
        }
    }

    public string[] FindTaskSequence()
    {
        // Topological sort using DFS
        Stack<int> stack = new Stack<int>();
        bool[] visited = new bool[TaskCount];

        for (int i = 0; i < TaskCount; i++)
        {
            if (!visited[i])
            {
                TopologicalSort(i, visited, stack);
            }
        }

        // Save to file
        StringBuilder sb = new StringBuilder();
        while (!stack.IsEmpty())
        {
            sb.AppendLine(Tasks[stack.Pop()].TaskID);
        }
        File.WriteAllText("Sequence.txt", sb.ToString());

        // Return as string array
        return sb.ToString().Split('\n');
    }

    private void TopologicalSort(int taskIndex, bool[] visited, Stack<int> stack)
    {
        visited[taskIndex] = true;
        foreach (ITask dependency in Tasks[taskIndex].Dependencies)
        {
            int dependencyIndex = GetIndex(dependency.TaskID);
            if (!visited[dependencyIndex])
            {
                TopologicalSort(dependencyIndex, visited, stack);
            }
        }
        stack.Push(taskIndex);
    }
}
