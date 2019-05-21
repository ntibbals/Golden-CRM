using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Golden_CRM.Controllers
{
    public class ToDoController : Controller
    {

        private readonly IToDo _context;

        public ToDoController(IToDo context)
        {
            _context = context;
        }
        /// <summary>
        /// This method handles view for ToDos/tasks based on user ID
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string userId)
        {
            var toDos = await _context.FindToDos(userId);

            return View(toDos);
        }

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method handles adding todos/tasks into db
        /// </summary>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, CustomerID, UserID, ContactType, Comment, Assigned Owner, DueDate")] ToDo toDo)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    toDo.CustomerName = await _context.FindName(toDo.CustomerID);
                    toDo.ID = 0;
                    await _context.SaveAsync(toDo);
                    return LocalRedirect($"~/Customers/Customer/{toDo.CustomerID}");
                }
                return View(toDo);
            }
            catch (Exception)
            {
                return Redirect("https://http.cat/500");

            }

        }

        /// <summary>
        /// This method locates specific ToDo to edit
        /// </summary>
        /// <param name="id">id of todo/task to edit</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _context.FindToDo(id);

            if(note == null)
            {
                return NotFound();
            }
            return View(note);

        }
        /// <summary>
        /// This method handles edit functionality for ToDo's/notes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="toDo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, CustomerID, UserID, AssignedOwner, ContactType, Comment, DueDate, CompletedDate")] ToDo toDo)
        {
            if (id != toDo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateToDo(toDo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                //return RedirectToAction(nameof(Index));
                return LocalRedirect($"~/Customers/Customer/{toDo.CustomerID}");

            }

            return View(toDo);
        }

        /// <summary>
        /// This method handles delete routing for todo's/tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var toDo = await _context.FindToDo(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        /// <summary>
        /// This method controls delete functionality for todos/tasks
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ToDo toDo = await _context.FindToDo(id);
            await _context.DeleteAsync(id);
            return LocalRedirect($"~/Customers/Customer/{toDo.CustomerID}");
        }
    }
}