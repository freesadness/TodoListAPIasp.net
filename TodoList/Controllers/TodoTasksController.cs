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
    public class TodoTasksController : ControllerBase
    {

        private ITodoTaskRepository _repo;
        private readonly IMapper _mapper;

        public TodoTasksController(ITodoTaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/TodoTasks
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<TodoTask>>> GetTodoTask()
        {
            //return await _context.TodoTask.ToListAsync();
            return Ok(await _repo.GetAll());
        }

        // GET: api/TodoTasks/5
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<TodoTask>> GetTodoTask(int id)
        {
            //var todoTask = await _context.TodoTask.FindAsync(id);
            var todoTask = await _repo.GetTodoTaskById(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return todoTask;
        }


        // PUT: api/TodoTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update")]
        public async Task<IActionResult> PutTodoTask(TodoTask todoTask)
        {
            if (!await _repo.Exists(todoTask.TaskId))
            {
                return BadRequest();
            }
            TodoTask t;
            try
            {
                t = await _repo.Put(todoTask);
                //_context.Entry(todoTask).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.Exists(todoTask.TaskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(t);
        }

        // POST: api/TodoTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        public async Task<ActionResult<TodoTask>> PostTodoTask(TodoTask todoTask)
        {
            //_context.TodoTask.Add(todoTask);
            //await _context.SaveChangesAsync();
            todoTask = await _repo.Post(todoTask);
            return CreatedAtAction("GetTodoTask", new { id = todoTask.TaskId }, todoTask);
        }

        // DELETE: api/TodoTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoTask>> DeleteTodoTask(int id)
        {
           
            if (!await _repo.Exists(id))
            {
                return NotFound();
            }
            TodoTask todoTask= await _repo.Delete(id);
            //_context.TodoTask.Remove(todoTask);
            //await _context.SaveChangesAsync();

            return todoTask;
        }

        //private bool TodoTaskExists(int id)
        //{
        //    return _context.TodoTask.Any(e => e.Id == id);
        //}
    }
}
