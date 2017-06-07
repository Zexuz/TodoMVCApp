using Microsoft.EntityFrameworkCore;
using Todo.Domain.Todo.Checklist;
using Todo.Domain.Todo.Note;

namespace Todo.Domain.Repository
{
    public sealed class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> ctx) : base(ctx)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoNote>();
            modelBuilder.Entity<TodoChecklist>().HasMany(a => a.CheckList);
           
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TodoNote> TodoNotes{ get; set; }
        public DbSet<TodoChecklist> TodoChecklists{ get; set; }
    }
}