using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Todo.Domain.Todo.Note;

namespace Todo.Domain.Repository
{
    public class TodoNoteRepository : IRepository<TodoNote>
    {
        private readonly TodoContext _context;

        public TodoNoteRepository(TodoContext context)
        {
            _context = context;
        }

        public TodoNote GetById(int id)
        {
            return _context.TodoNotes.SingleOrDefault(t => t.Id == id);
        }

        public TodoNote Update(TodoNote entity)
        {
            var res = _context.TodoNotes.Update(entity);
            _context.SaveChanges();
            return res.Entity;
        }

        public TodoNote Add(TodoNote entity)
        {
            var res = _context.TodoNotes.Add(entity);
            _context.SaveChanges();
            return res.Entity;
        }

        public bool Delete(TodoNote entity)
        {
            var res = _context.TodoNotes.Remove(entity);
            _context.SaveChanges();
            return res.Entity == null;
        }

        public IEnumerable<TodoNote> GetAll()
        {
            return _context.TodoNotes;
        }

        public IEnumerable<TodoNote> FindAll(Expression<Func<TodoNote, bool>> predicate)
        {
            return _context.TodoNotes.Where(predicate).ToList();
        }
    }
}