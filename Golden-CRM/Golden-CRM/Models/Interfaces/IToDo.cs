using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    public interface IToDo
    {
        Task<Note> FindToDo(int id);
        Task<List<Note>> FindToDos(int customerID);
        Task<List<Note>> GetToDos();
        Task UpdateToDo(ToDo toDo);
        Task SaveAsync(ToDo toDo);
        Task DeleteAsync(int id);
    }
}
