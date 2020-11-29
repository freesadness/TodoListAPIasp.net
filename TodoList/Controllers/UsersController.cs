using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet("getAll")]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            //return await _context.User.ToListAsync();

           return  Ok(_repo.GetAll());
        }

        // GET: api/Users/5
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
           
            if (!await _repo.UserExists(id))
            {
                return NotFound();
            }
            //var tasks =  _context.TodoTask.Where(x => x.UserId == user.UserId).ToList();
            //user.Tasks = tasks;
            return Ok(await _repo.GetUserById(id));
        }



        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("updateUser")]
        public async Task<IActionResult> PutUser(User user)
        {
            if (!await _repo.UserExists(user.UserId))
            {
                return NotFound();
            }
            //_context.Entry(user).State = EntityState.Modified;
            User u;
            try
            {
                u= await _repo.Put(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.UserExists(user.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(u);
        }

        // GET: api/Users/5
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserCredential creden)
        {
            User user = _mapper.Map<User>(creden);
            user = await _repo.GetUserByCredential(user);
            if (user == null)
            {
                return NotFound();
            }
            //var tasks =  _context.TodoTask.Where(x => x.UserId == user.UserId).ToList();
            //user.Tasks = tasks;
            return Ok(user);
        }

        // GET: api/Users/5
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserCredential creden)
        {
            User user = _mapper.Map<User>(creden);
            user = await _repo.Post(user);
            if (user == null)
            {
                return NotFound();
            }
            //var tasks =  _context.TodoTask.Where(x => x.UserId == user.UserId).ToList();
            //user.Tasks = tasks;
            return Ok(user);
        }
        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("createTaskForUser")]
        public async Task<IActionResult> PutUserTask(int userId, TodoTaskDto todoTask)
        {
            if (!await _repo.UserExists(userId))
            {
                return NotFound();
            }
            //_context.Entry(user).State = EntityState.Modified;
            User u;
            try
            {
                var todo = _mapper.Map<TodoTask>(todoTask);
                todo.UserId = userId;
                u = await _repo.PostUserTask(userId, todo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.UserExists(userId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(u);
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("createUser")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            //_context.User.Add(user);
            //await _context.SaveChangesAsync();
            await _repo.Post(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            //var user = await _context.User.FindAsync(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            //_context.User.Remove(user);
            //await _context.SaveChangesAsync();
            if (!await _repo.UserExists(id))
            {
                return NotFound();
            }
            User user = await _repo.Delete(id);
            return user;
        }

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.UserId == id);
        //}
    }
}
