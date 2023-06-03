public interface ITaskManager
{
    void AddTask(ITask task);
    void RemoveTask(string taskID);
    string[] FindEarliestCommencementTimes();
    string[] FindTaskSequence();
    void ChangeTaskTime(string taskID, int newTime); 
    string[] GetTaskDetails();
    bool DoesTaskExist(string taskID);
}
