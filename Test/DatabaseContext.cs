using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class DatabaseContext : DbContext
    {
        private readonly string _dbFolder = "db";
        private readonly string _dbName = "integration.sdb";
        private readonly string _dbPath;

        public DbSet<User> Users { get; set; }

        public DatabaseContext()
        {
            _dbPath = Path.Combine(Environment.CurrentDirectory, _dbFolder, _dbName);
            CreateDbDirectoryIfNotExists(_dbPath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            new DbSeeder(modelBuilder).Seed();
        }

        private void CreateDbDirectoryIfNotExists(string dbPath)
        {
            FileInfo dbInfo = new FileInfo(dbPath);
            if (dbInfo.Directory?.Exists == false)
            {
                Directory.CreateDirectory(dbInfo.Directory.FullName);
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
