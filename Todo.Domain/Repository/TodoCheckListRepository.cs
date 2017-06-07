using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Todo.Checklist;

namespace Todo.Domain.Repository
{
    public class TodoCheckListRepository : IRepository<TodoChecklist>
    {
        private readonly TodoContext _context;

        public TodoCheckListRepository(TodoContext context)
        {
            _context = context;
        }

        public TodoChecklist GetById(int id)
        {
            return _context.TodoChecklists.Include(c => c.CheckList).SingleOrDefault(t => t.Id == id);
        }

        public TodoChecklist Update(TodoChecklist entity)
        {
            var res = _context.TodoChecklists.Update(entity);
            _context.SaveChanges();
            return res.Entity;
        }

        public TodoChecklist Add(TodoChecklist entity)
        {
            var res = _context.TodoChecklists.Add(entity);
            _context.SaveChanges();
            return res.Entity;
        }

        public bool Delete(TodoChecklist entity)
        {
            var res = _context.TodoChecklists.Remove(entity);
            _context.SaveChanges();
            return res.Entity == null;
        }

        public IEnumerable<TodoChecklist> GetAll()
        {
            return _context.TodoChecklists.Include(c => c.CheckList);
        }

        public IEnumerable<TodoChecklist> FindAll(Expression<Func<TodoChecklist, bool>> predicate)
        {
            return _context.TodoChecklists.Include(c => c.CheckList).Where(predicate).ToList();
        }
    }
}