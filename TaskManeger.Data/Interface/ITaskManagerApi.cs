
using TaskManager.Model;

namespace TaskManeger.Data.Interface
{
    public interface ITaskManagerApi
    {
        Task<List<TaskItem>> GetTasksAsync();

        Task<TaskItem?> GetTaskAsync(Guid id);

        Task<TaskItem> CreateTaskAsync(TaskItem task);

        Task<TaskItem?> UpdateTaskAsync(TaskItem task);

        Task DeleteTaskAsync(TaskItem task);
    }
}
