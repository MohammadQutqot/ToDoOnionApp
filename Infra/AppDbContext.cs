using core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TaskReminder> TaskReminders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskTag> TaskTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.Reminder)
                .WithOne(r => r.TaskItem)
                .HasForeignKey<TaskReminder>(r => r.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTag>()
                .HasKey(tt => new { tt.TaskItemId, tt.TagId });

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt => tt.TaskItem)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TaskTag>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.TaskTags)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}