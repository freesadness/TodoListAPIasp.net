using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface ITodoTaskRepository
    {
        Task<bool> Exists(int id);
        Task<IEnumerable<TodoTask>> GetAll();
        Task<TodoTask> GetTodoTaskById(int id);
        Task<TodoTask> Put(TodoTask i);
        Task<TodoTask> Post(TodoTask i);
        Task<TodoTask> Delete(int id);

    }
}
