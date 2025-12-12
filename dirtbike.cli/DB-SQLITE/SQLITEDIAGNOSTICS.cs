using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Sqlite;


namespace T4DBSQLITE
{
    // Entity classes
    public class TestUser
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    // DbContext for SQLite
    public class T4SqliteContext : DbContext
    {
        public DbSet<TestUser> TestUsers { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite database file
            optionsBuilder.UseSqlite("Data Source=MyNewDatabase.db");
        }
    }

    class T4SQLITE
    {
        public void sayMSHelloPing()
        {
            Console.WriteLine("Hello John. SQLite Server Diagnostics. Can we please finish before Christmas?");
        }

        public void StartSQLITEUtilities()
        {
            Console.WriteLine("Right Now we are not doing anything special.");
        }

        // Test EF Core connection and query
        public static void ConnectionTest()
        {
            using (var context = new T4SqliteContext())
            {
                // Ensure database and tables are created
                context.Database.EnsureCreated();

                // Insert sample data if empty
                if (!context.TestUsers.Any())
                {
                    context.TestUsers.AddRange(new List<TestUser>
                    {
                        new TestUser { Username = "john", Email = "john@test.com", CreatedAt = DateTime.Now },
                        new TestUser { Username = "ryan", Email = "ryan@gmail.com", CreatedAt = DateTime.Now }
                    });
                    context.SaveChanges();
                }

                // Query users
                foreach (var user in context.TestUsers)
                {
                    Console.WriteLine($"UserID: {user.UserID}, Username: {user.Username}, Email: {user.Email}, CreatedAt: {user.CreatedAt}");
                }
            }
        }
    }
}
