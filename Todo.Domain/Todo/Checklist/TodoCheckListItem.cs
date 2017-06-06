using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Todo.Checklist
{
    public class TodoCheckListItem
    {
        [Key]
        public int Id { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }
    }
}