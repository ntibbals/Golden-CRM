using Golden_CRM.Data;
using Golden_CRM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Services
{
    public class ToDoManager : IToDo
    {
        private readonly GoldenDbContext _context;

        public ToDoManager(GoldenDbContext context)
        {
            _context = context;
        }
        public async Task DeleteAsync(int id)
        {
            ToDo toDo = await _context.ToDos.FindAsync(id);
            if (toDo != null)
            {
                _context.Remove(toDo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> FindName(int customerId)
        {
            var name = await _context.Customers.FirstOrDefaultAsync(c => c.ID == customerId);
            var fullName = name.FirstName + " " + name.LastName;
            return fullName;
        }

        public async Task<ToDo> FindToDo(int id)
        {
            ToDo toDo = await _context.ToDos.FirstOrDefaultAsync(t => t.ID == id);

            return toDo;
        }

        public async Task<List<ToDo>> FindToDos(string userId)
        {
            var toDos = _context.ToDos.Where(t => t.UserID == userId);

            return await toDos.ToListAsync();
        }

        public async Task<List<ToDo>> GetToDos()
        {
            return await _context.ToDos.ToListAsync();
        }

        public async Task SaveAsync(ToDo toDo)
        {
            toDo.CompletedDate = DateTime.Now;

            if(await _context.ToDos.FirstOrDefaultAsync(t => t.ID == toDo.ID) == null)
            {
                _context.ToDos.Add(toDo);
            }
            else
            {
                _context.ToDos.Update(toDo);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateToDo(ToDo toDo)
        {
            ToDo oldToDo = await _context.ToDos.FirstOrDefaultAsync(t => t.ID == toDo.ID);

            if(oldToDo == null)
            {
                toDo.CompletedDate = DateTime.Now;
                _context.ToDos.Add(toDo);
            }

            else
            {
                oldToDo.ContactType = toDo.ContactType;
                oldToDo.AssignedOwner = toDo.AssignedOwner;
                oldToDo.DueDate = toDo.DueDate;
                oldToDo.Comment = toDo.Comment;
                oldToDo.CompletedDate = DateTime.Now;
                oldToDo.UserID = toDo.UserID;

                _context.ToDos.Update(oldToDo);
            }

            await _context.SaveChangesAsync();
        }
    }
}
