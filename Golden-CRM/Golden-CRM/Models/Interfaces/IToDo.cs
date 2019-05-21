using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    public interface IToDo
    {
        Task<ToDo> FindToDo(int id);
        Task<List<ToDo>> FindToDos(string userId);
        Task<List<ToDo>> GetToDos();
        Task UpdateToDo(ToDo toDo);
        Task SaveAsync(ToDo toDo);
        Task DeleteAsync(int id);
        Task<string> FindName(int customerId);
    }
}
