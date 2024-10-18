using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext : DbContext
    {

        public AplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<ToDoModel> ToDos { get; set; }
        public DbSet<DeadLineModel> DeadLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoModel>()
            .HasOne(d => d.DeadLine)
            .WithOne(x => x.ToDo)
            .HasForeignKey<ToDoModel>(d => d.DeadLineId)
            .IsRequired(false);
        }
    }
}