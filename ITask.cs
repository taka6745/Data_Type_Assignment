public interface ITask
{
    string TaskID { get; }

    int TimeNeeded { get; }

    ITask[] ? Dependencies { get; }

    int DependencyCount { get; }

    // Add a dependency between the task and its dependency task
    void AddDependency(ITask dependency);

    // Remove a dependency between the task and its dependency task
    void RemoveDependency(ITask dependency);

    // Change the time needed to complete the task with the specified task ID
    void ChangeTimeNeeded(string taskID, int newTimeNeeded);

}