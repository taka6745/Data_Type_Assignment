public interface ITaskManager
{
    // Add a new task with the specified task ID and time needed to complete the task
    void AddTask(ITask task);

    // Remove the task with the specified task ID from the project
    void RemoveTask(string taskID);

    // Find the earliest possible commencement time for each task in the project
    string[] FindEarliestCommencementTimes();
    
    // Find a sequence of tasks that satisfies all dependencies
    string[] FindTaskSequence();
}
