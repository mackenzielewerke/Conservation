using AngularCircus.web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCircus.web.Models
{
    public class AngularCircusContext : IdentityDbContext

    {
        public DbSet<Circus> Circuses { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Act> Acts { get; set; }

        public AngularCircusContext()
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
            builder.Entity<Circus>()
                .HasKey(c => new { c.Id });
            builder.Entity<Act>()
                .HasOne(p => p.Circus)
                .WithMany(b => b.Acts)
                .HasForeignKey(c => new { c.CircusId });
        }
    }
}

