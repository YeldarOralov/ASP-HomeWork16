using HW16.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW16.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = new User
            {
                Login = "admin",
                Password = "123456"
            };
            modelBuilder.Entity<User>().HasData(user);
            modelBuilder.Entity<Portfolio>(entity => entity.HasOne(x => x.User).WithMany(x => x.Portfolios).HasForeignKey("UserId"));
            modelBuilder.Entity<Portfolio>().HasData(
                new Portfolio
                {
                    UserId=user.Id,
                    Name = "Project 1",
                    Description = "Test project",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Microsoft_.NET_logo.png"
                },
                new Portfolio
                {
                    UserId = user.Id,
                    Name = "Project 2",
                    Description = "Test project",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Microsoft_.NET_logo.png"
                });
        }
    }
}
