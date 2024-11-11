using Microsoft.EntityFrameworkCore;
using TaskManager.Model;

namespace TaskManeger.Data
{
    public class TaskManegerDBContext : DbContext
    {
        public TaskManegerDBContext(DbContextOptions<TaskManegerDBContext> options) : base(options)
        {

        }

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=TaskManagerDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}
