public interface ITask
{
    string TaskID { get; }

    int TimeNeeded { get; }

    string[] Dependencies { get; }

    int DependencyCount { get; }

    void AddDependency(string dependencyID);

    void RemoveDependency(string dependencyID);

    void ChangeTimeNeeded(int newTimeNeeded);

}
