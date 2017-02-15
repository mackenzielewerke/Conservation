using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAuthentication.Models
{
    public class TeamContext : DbContext
    {
        public DbSet<Circus> Circuses { get; set; }
        public DbSet<Animal> Animals { get; set; }

        public DbSet<Performer> Performers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Circus;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public TeamContext()
        {
         
        }


    }
}
