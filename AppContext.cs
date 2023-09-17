using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    internal class AppContext:DbContext
    {
        public AppContext() {
        Database.EnsureCreated();
        }
        public DbSet<Hero> Heroes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-U7N6H5S8;Initial Catalog=HeroesDB;Integrated Security=True;Connect Timeout=30;Trust Server Certificate=True;");
        }
    }
}
