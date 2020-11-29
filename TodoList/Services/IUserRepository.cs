using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IUserRepository
    {
        Task<bool> UserExists(int id);
        Task<User> GetUserByCredential(User user);
        IEnumerable<User> GetAll();
        Task<User> GetUserById(int id);
        Task<User> Post(User user);
        Task<User> Put(User user);
        Task<User> Delete(int id);
        Task<User> PostUserTask(int userId, TodoTask todo);
    }
}
