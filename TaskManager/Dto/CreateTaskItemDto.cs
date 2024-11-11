using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dto
{
    public class CreateTaskItemDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
