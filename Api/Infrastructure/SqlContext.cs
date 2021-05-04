using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Infrastructure
{
    public class SqlContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        {
           // Database.EnsureCreated();    создаем базу данных при первом обращении
        }

    }
}
