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
        public async Task<IActionResult> Index(int customerID)
        {
            var toDos = await _context.FindToDos(customerID);

            return View(toDos);
        }

        public IActionResult Creater()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, CustomerID, UserID, ContactType, Comment, Assigned Owner, DueDate")] ToDo toDo)
        {

            try
            {
                if (ModelState.IsValid)
                {
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

        public async Task<IActionResult> Delete(int id)
        {
            var toDo = await _context.FindToDo(id);

            if (toDo == null)
            {
                return NotFound();
            }

            return View(toDo);
        }

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