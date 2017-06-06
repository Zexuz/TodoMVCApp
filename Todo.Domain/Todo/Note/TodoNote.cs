namespace Todo.Domain.Todo.Note
{
    public class TodoNote:TodoBase
    {
        public string Note { get; set; }
        
        public override TodoType GetType()
        {
            return TodoType.Note;
        }
    }
}