using AngularZoo.web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularZoo.web.Models
{
    public class AngularConservationContext : IdentityDbContext

    {
        public DbSet<Conservation> Conservations { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Group> Groups { get; set; }

        public AngularConservationContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=AngularConservation;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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

