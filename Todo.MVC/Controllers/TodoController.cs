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
        
        [HttpPost]
        public IActionResult AddCheckbox(int checkListId)
        {
            var oldCheckList = _todoCheckLists.GetById(checkListId);
            if (oldCheckList == null) return NotFound();

            var checkListItem = new TodoCheckListItem();
            oldCheckList.CheckList.Add(checkListItem);
            oldCheckList.LastEdit = DateTime.Now;
            _todoCheckLists.Update(oldCheckList);

            return Ok(Json(checkListItem));
        }
        [HttpDelete]
        public IActionResult DeleteCheckbox(int checkListId, int checkListItemId)
        {
            var oldCheckList = _todoCheckLists.GetById(checkListId);
            if (oldCheckList == null) return NotFound();

            var checkListItem = oldCheckList.CheckList.SingleOrDefault(item => item.Id == checkListItemId);
            if (checkListItem == null) return NotFound();

            oldCheckList.CheckList.Remove(checkListItem);
            oldCheckList.LastEdit = DateTime.Now;

            _todoCheckLists.Update(oldCheckList);
            return Ok(Json(checkListItem));
        }
        

        [HttpPost]
        public IActionResult UpdateCheckbox([FromBody] TodoChecklist checklist)
        {
            var oldCheckList = _todoCheckLists.GetById(checklist.Id);
            if (oldCheckList == null) return NotFound();

            foreach (var oldItem in oldCheckList.CheckList)
            {
                foreach (var item in checklist.CheckList)
                {
                    if (oldItem.Id != item.Id) continue;
                    oldItem.Text = item.Text;
                    oldItem.Checked = item.Checked;
                }
            }
            oldCheckList.LastEdit = DateTime.Now;
            oldCheckList.Title = checklist.Title;

            _todoCheckLists.Update(oldCheckList);
            return Ok();
        }
        
        [HttpPost]
        public IActionResult UpdateNote([FromBody] TodoNote todoNote)
        {
            var oldNote = _todoNotes.GetById(todoNote.Id);
            if (oldNote == null) return NotFound();

            oldNote.LastEdit = DateTime.Now;
            oldNote.Title = todoNote.Title;
            oldNote.Note= todoNote.Note;

            _todoNotes.Update(oldNote);
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateNote()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var todoNote = new TodoNote();
            _todoNotes.Add(todoNote);
            return Ok(Json(todoNote));
        }


        public IActionResult CreateChecklist()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var todoChecklist = new TodoChecklist();
            _todoCheckLists.Add(todoChecklist);
            return Ok(Json(todoChecklist));
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}