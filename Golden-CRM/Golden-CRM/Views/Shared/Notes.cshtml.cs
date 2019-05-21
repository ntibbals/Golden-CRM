using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Golden_CRM.Pages.Components.Notes
{
    public class NotesModel : PageModel
    {
        private readonly INote _note;

        [BindProperty]
        public List<Note> Note { get; set; }

        [FromRoute]
        public int? ID { get; set; }

        public NotesModel(INote note)
        {
            _note = note;
        }

        public async Task OnGetAsync()
        {
            Note = await _note.FindNotes(ID.GetValueOrDefault());
        }

        public async Task<IActionResult> OnPost()
        {

            var notes = await _note.FindNotes(ID.GetValueOrDefault());
            if(notes == null)
            {
                Note note = new Note();
                await _note.SaveAsync(note);
            }
            //else
            //{
            //    await _note.SaveChangesAsync();

            //}

            return RedirectToPage("/Customers/Customer", new { id = ID });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _note.DeleteAsync(ID.Value);

            return RedirectToPage("/Index");
        }
    }
}