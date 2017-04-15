using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Conservation.web.Models;

namespace Conservation.web.Data
{
    public class ConservationContext : IdentityDbContext
    {
        public DbSet<Conservations> Conservations { get; set; }
        public DbSet<Habitat> Habitats { get; set; }
        public DbSet<Animal> Animals { get; set; }

        public ConservationContext()
            : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=./Conservation.db");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .ForSqliteToTable("Users");
            builder.Entity<IdentityRole>()
                .ForSqliteToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>()
                .ForSqliteToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<string>>()
                .ForSqliteToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>()
                .ForSqliteToTable("UserLogins");
            builder.Entity<IdentityUserRole<string>>()
                .ForSqliteToTable("UserRoles");
            builder.Entity<IdentityUserToken<string>>()
                .ForSqliteToTable("UserTokens");
        }
    }
}
