using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    public interface INote
    {
        Task<Note> FindNote(int id);
        Task<List<Note>> FindNotes(int customerID);
        Task<List<Note>> GetNotes();
        Task UpdateNote(Note note);
        Task SaveAsync(Note note);
        Task DeleteAsync(int id);
    }
}
