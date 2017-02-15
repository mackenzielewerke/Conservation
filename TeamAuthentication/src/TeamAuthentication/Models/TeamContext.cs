using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamAuthentication.Data;

namespace TeamAuthentication.Models
{
    public class TeamContext : IdentityDbContext<ApplicationUser>
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

        public TeamContext() : base()
        {
         
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
            .ToTable("Users");
            builder.Entity<IdentityRole>()
            .ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>()
            .ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<string>>()
            .ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>()
           .ToTable("UserLogins");
            builder.Entity<IdentityUserRole<string>>()
           .ToTable("UserRoles");
            builder.Entity<IdentityUserToken<string>>()
           .ToTable("UserTokens");
        }

    }
}
