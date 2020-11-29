using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class TDContext : DbContext
    {
        public TDContext (DbContextOptions<TDContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication1.Models.User> User { get; set; }

        public DbSet<WebApplication1.Models.TodoTask> TodoTask { get; set; }
    }
}
