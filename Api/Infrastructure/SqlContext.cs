using Microsoft.EntityFrameworkCore;
using SQL_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace SQL_API.Infrastructure
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
