using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext : IdentityDbContext<AppUser>
    {

        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<ToDoModel> ToDos { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>().HasKey(p => new{p.UserId, p.ToDoId});

            builder.Entity<Portfolio>(x => x
            .HasOne(x => x.ToDo)
            .WithMany(t => t.Portfolios)
            .HasForeignKey(x => x.ToDoId));

            builder.Entity<Portfolio>(x=> x
            .HasOne(x => x.User)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(x => x.UserId));

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}