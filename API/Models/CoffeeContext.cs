using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace API.Models
{
    public class CoffeeContext : DbContext
    {
        public string DbPath { get; private set; }

        public CoffeeContext(DbContextOptions<CoffeeContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}coffee.db";
        }

        public DbSet<Coffee> Coffees { get; set; }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //     => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coffee>().ToTable("Coffee");
        }
    }
}