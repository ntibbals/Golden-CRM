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
    public class NotesController : Controller
    {

        private readonly INote _context;

        public NotesController(INote context)
        {
            _context = context;
        }

        /// <summary>
        /// This method finds all notes based on customer id
        /// </summary>
        /// <param name="customerID">customer id</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int customerID)
        {
            var notes = await _context.FindNotes(customerID);

            return View(notes);
        }

        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method handles creation of notes in db
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, CustomerID, UserID, Comment, Date")] Note note)
        {

            try
            {
                if(ModelState.IsValid)
                {
                    note.ID = 0;
                    await _context.SaveAsync(note);
                    return LocalRedirect($"~/Customers/Customer/{note.CustomerID}");
                }
                return View(note);
            }
            catch(Exception)
            {
                return Redirect("https://http.cat/500");

            }

        }
        /// <summary>
        /// This method handles edit view for notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var note = await _context.FindNote(id);

            return View(note);
        }

        /// <summary>
        /// This method handles edit funcitonality for notes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, CustomerID, UserID, Comment, Date")] Note note)
        {
            if (id != note.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateNote(note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                //return RedirectToAction(nameof(Index));
                return LocalRedirect($"~/Customers/Customer/{note.CustomerID}");

            }

            return View(note);
        }

        /// <summary>
        /// This method handles delete views for notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.FindNote(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        /// <summary>
        /// This method handles delete funcitonality for notes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Note note = await _context.FindNote(id);
            await _context.DeleteAsync(id);
            return LocalRedirect($"~/Customers/Customer/{note.CustomerID}");
        }

    }
}