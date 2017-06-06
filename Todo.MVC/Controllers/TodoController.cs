using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Repository;
using Todo.Domain.Todo.Checklist;
using Todo.Domain.Todo.Note;
using Todo.MVC.ViewModels;

namespace Todo.MVC.Controllers
{
    public class TodoController : Controller
    {
        private readonly IRepository<TodoChecklist> _todoCheckLists;
        private readonly IRepository<TodoNote> _todoNotes;

        public TodoController(IRepository<TodoChecklist> todoCheckLists, IRepository<TodoNote> todoNotes)
        {
            _todoCheckLists = todoCheckLists;
            _todoNotes = todoNotes;
        }

        public IActionResult Index()
        {
            var vm = new IndexViewModel
            {
                Checklists = _todoCheckLists.GetAll().ToList(),
                Notes = _todoNotes.GetAll().ToList()
            };
            return View(vm);
        }

        public IActionResult GetAll()
        {
            return Json(new
            {
                checkLists = _todoCheckLists.GetAll(),
                notes = _todoNotes.GetAll()
            });
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