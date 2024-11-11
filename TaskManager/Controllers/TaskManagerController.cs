using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Dto;
using TaskManager.Model;
using TaskManeger.Data.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskManagerApi api;

        public TaskManagerController(ITaskManagerApi api)
        {
            this.api = api;
        }

        [HttpGet]
        [Route("api/tasks")]
        public async Task<IActionResult> GetTasksAsync()
        {
            var taskItems = (await api.GetTasksAsync()).Select(task => new TaskItemDto()
            {
                Id = task.Id,
                Title = task.Title,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate,
                Description = task.Description
            });

            return Ok(taskItems);
        }

        [HttpGet]
        [Route("api/tasks/{id}")]
        public async Task<IActionResult> GetTasksAsync(Guid id)
        {
            var taskItem = await api.GetTaskAsync(id);
            if(taskItem == null)
            {
                return NotFound();
            }

            var taskItemDto = new TaskItemDto()
            {
                Id = taskItem.Id,
                Title = taskItem.Title,
                IsCompleted = taskItem.IsCompleted,
                DueDate = taskItem.DueDate,
                Description = taskItem.Description
            };
            return Ok(taskItemDto);
        }

        [HttpPost]
        [Route("api/tasks")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] CreateTaskItemDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var task = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    IsCompleted = taskDto.IsCompleted,
                    DueDate = taskDto.DueDate
                };

                var taskItem = await api.CreateTaskAsync(task);
                return CreatedAtAction(nameof(GetTasksAsync), new { id = taskItem.Id }, taskItem);
            }
            catch(Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("api/tasks")]
        public async Task<IActionResult> UpdateTaskAsync([FromBody] TaskItemDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var taskItem = new TaskItem
                {
                    Id = taskDto.Id,
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    IsCompleted = taskDto.IsCompleted,
                    DueDate = taskDto.DueDate
                };

                taskItem = await api.UpdateTaskAsync(taskItem);

                if(taskItem == null)
                {
                    return NotFound();
                }

                return CreatedAtAction(nameof(GetTasksAsync), new { id = taskItem?.Id }, taskItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("api/tasks/{id}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            try
            {
                var taskItem = await api.GetTaskAsync(id);
                if (taskItem == null)
                {
                    return NotFound();
                }
                await api.DeleteTaskAsync(taskItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ModelState);
            }
        }
    }
}
