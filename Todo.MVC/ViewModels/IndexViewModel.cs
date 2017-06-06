using System.Collections.Generic;
using Todo.Domain.Todo.Checklist;
using Todo.Domain.Todo.Note;

namespace Todo.MVC.ViewModels
{
    public class IndexViewModel
    {
        public List<TodoNote> Notes { get; set; }
        public List<TodoChecklist>Checklists { get; set; }
    }
}