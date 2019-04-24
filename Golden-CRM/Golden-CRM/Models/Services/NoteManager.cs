using Golden_CRM.Data;
using Golden_CRM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Services
{
    public class NoteManager : INote
    {

        private readonly GoldenDbContext _context;

        public NoteManager(GoldenDbContext context)
        {
            _context = context;
        }

   
        public async Task DeleteAsync(int id)
        {
            Note note = await _context.Notes.FindAsync(id);
            if(note != null)
            {
                _context.Remove(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Note> FindNote(int id)
        {
            Note note = await _context.Notes.FirstOrDefaultAsync(n => n.ID == id);

            return note;
        }

        public async Task<List<Note>> FindNotes(int customerID)
        {
            var notes= _context.Notes.Where(n => n.CustomerID == customerID);

            return await notes.ToListAsync();

        }

        public async Task<List<Note>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task SaveAsync(Note note)
        {

            if(await _context.Notes.FirstOrDefaultAsync(n => n.ID == note.ID) == null)
            {
                _context.Notes.Add(note);
            }

            else
            {
                _context.Notes.Update(note);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNote(Note note)
        {
            Note oldNote = await _context.Notes.FirstOrDefaultAsync(n => n.ID == note.ID);

            if ( note == null)
            {
                _context.Notes.Add(note);
            }

            else
            {
                oldNote.Comment = note.Comment;
                oldNote.Date = note.Date;
                oldNote.UserID = note.UserID;

                _context.Notes.Update(oldNote);
            }

            await _context.SaveChangesAsync();
        }
    }
}
