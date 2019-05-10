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
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ToDo> FindToDo(int id)
        {
            ToDo toDo = await _context.ToDos.FirstOrDefaultAsync(t => t.ID == id);

            return toDo;
        }

        public async Task<List<ToDo>> FindToDos(int customerID)
        {
            var toDos = _context.ToDos.Where(t => t.CustomerID == customerID);

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
                oldToDo.Comment = toDo.Comment;
                oldToDo.CompletedDate = DateTime.Now;
                oldToDo.UserID = toDo.UserID;

                _context.ToDos.Update(toDo);
            }

            await _context.SaveChangesAsync();
        }
    }
}
