using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Todo
{
    public abstract class TodoBase
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastEdit { get; set; }

        public new abstract TodoType GetType();

    }

    public enum TodoType
    {
        Note,
        Checklist
    }
}