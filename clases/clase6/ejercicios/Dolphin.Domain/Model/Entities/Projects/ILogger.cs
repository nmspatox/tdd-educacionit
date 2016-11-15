namespace Dolphin.Domain.Model.Entities.Projects {
    public interface ILogger {
        void LogAddTask(ProjectTask task);
        void LogRemoveTask(ProjectTask task);
    }
}