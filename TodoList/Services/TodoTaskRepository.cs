using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private TDContext  _context;
        public TodoTaskRepository(TDContext context)
        {
            _context = context;
        }

        public async Task<TodoTask> Delete(int id)
        {
            TodoTask t = await GetTodoTaskById(id);
            _context.TodoTask.Remove(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.TodoTask.AnyAsync<TodoTask>(c => c.TaskId == id);
        }
        public async Task<IEnumerable<TodoTask>> GetAll()
        {
            return await _context.TodoTask.OrderBy(c => c.UserId).ToListAsync();
        }

        public async Task<TodoTask> GetTodoTaskById(int id)
        {
            return await _context.TodoTask.FirstOrDefaultAsync(x => x.TaskId == id);
        }

        public async Task<TodoTask> Post(TodoTask i)
        {
            _context.TodoTask.Add(i);
            await _context.SaveChangesAsync();
            return i;
        }

        public async Task<TodoTask> Put(TodoTask i)
        {
            _context.Entry(i).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return i;
        }

    }
}
