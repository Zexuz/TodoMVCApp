using System.Collections.Generic;

namespace Todo.Domain.Todo.Checklist
{
    public class TodoChecklist:TodoBase
    {
        public List<TodoCheckListItem> CheckList { get; set; }
        
        public override TodoType GetType()
        {
            return TodoType.Checklist;
        }
    }
    
}