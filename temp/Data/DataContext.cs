﻿using Microsoft.EntityFrameworkCore;

namespace StudAPI.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
            Database.EnsureCreated();
        }

        public DbSet<Human> People { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Group> Groups { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_config["ConnectionString"]);
        }
    }
}

