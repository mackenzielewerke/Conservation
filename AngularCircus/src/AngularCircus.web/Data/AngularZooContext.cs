using AngularCircus.web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCircus.web.Models
{
    public class AngularZooContext : IdentityDbContext

    {
        public DbSet<Zoo> Zoos { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Exhibit> Exhibits { get; set; }

        public AngularZooContext()
            : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=AngularCircus;Integrated Security=True");
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

