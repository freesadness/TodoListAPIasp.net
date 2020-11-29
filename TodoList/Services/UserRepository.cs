
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly TDContext _context;
        public UserRepository(TDContext context)
        {
            _context = context;
        }
        public async Task<bool> UserExists(int id)
        {
            return await _context.User.AnyAsync<User>(c => c.UserId == id);
        }
        public async Task<User> Delete(int id)
        {
            User u = await GetUserById(id);
            _context.TodoTask.RemoveRange(u.Tasks);
            _context.User.Remove(u);
            await _context.SaveChangesAsync();
            return u;
        }

        public  async Task<User> GetUserById(int userId)
        {
            User user = await _context.User.FirstOrDefaultAsync(x => x.UserId == userId);
            user.Tasks = _context.TodoTask.Where(x => x.UserId == user.UserId).ToList();
            return user;
        }

        public  IEnumerable<User> GetAll()
        {
            var result = _context.User.OrderBy(c => c.UserId).ToList(); ;
            foreach (var user in result)
            {
                user.Tasks = _context.TodoTask.Where(x => x.UserId == user.UserId).ToList();
            }
            return  result;
        }

        public async Task<User> Post(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<User> Put(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> PostUserTask(int userId, TodoTask todo)
        {
            User user= await GetUserById(userId);
            todo.IsCompleted = false;
            user.Tasks.Add( todo);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByCredential(User user)
        {
            return await _context.User.FirstOrDefaultAsync(x => x.UserName == user.UserName&&user.Password == x.Password);
        }

    }
}
