using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Repository;
using Todo.Domain.Todo.Checklist;
using Todo.Domain.Todo.Note;

namespace Todo.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<TodoChecklist> _todoCheckLists;
        private readonly IRepository<TodoNote> _todoNotes;

        public HomeController(IRepository<TodoChecklist> todoCheckLists, IRepository<TodoNote> todoNotes)
        {
            _todoCheckLists = todoCheckLists;
            _todoNotes = todoNotes;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult GetAll()
        {
            dynamic data = new ExpandoObject();
            data.checkLists = _todoCheckLists.GetAll();
            data.notes = _todoNotes.GetAll();
            return Json(data);
        }

        
        
        
        public IActionResult AddNote()
        {
            var e = new TodoNote
            {
                Title = "Hello, World!",
                Note = "It's Alive!",
                Created = DateTime.Now,
                LastEdit = DateTime.Now
            };
            _todoNotes.Add(e);
            return Ok();
        }
        
        
        public IActionResult AddChecklist()
        {
            var e = new TodoChecklist
            {
                Title = "Hello, World!",
                Created = DateTime.Now,
                LastEdit = DateTime.Now,
                CheckList = new List<TodoCheckListItem>
                {
                    new TodoCheckListItem
                    {
                        Checked = false,
                        Text = "Fix site"
                    },
                    new TodoCheckListItem
                    {
                        Checked = true,
                        Text = "Be awesome"
                    }
                }
            };

            _todoCheckLists.Add(e);
            return Ok();
        }

        
        public IActionResult Error()
        {
            return View();
        }
    }
}