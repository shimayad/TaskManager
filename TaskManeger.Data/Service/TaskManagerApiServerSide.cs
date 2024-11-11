using Microsoft.EntityFrameworkCore;
using TaskManager.Model;
using TaskManeger.Data.Interface;

namespace TaskManeger.Data.Service
{
    public class TaskManagerApiServerSide : ITaskManagerApi
    {
        private readonly TaskManegerDBContext _dbContext;

        public TaskManagerApiServerSide(TaskManegerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _dbContext.Tasks.OrderBy(t => t.DueDate).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskAsync(Guid id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            _dbContext.Add(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
        {
            var connectedtaskItem = await GetTaskAsync(task.Id);
            if (connectedtaskItem == null)
            {
                return null;
            }

            connectedtaskItem.Title = task.Title;
            connectedtaskItem.Description = task.Description;
            connectedtaskItem.IsCompleted = task.IsCompleted;
            connectedtaskItem.DueDate = task.DueDate;

            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _dbContext.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
