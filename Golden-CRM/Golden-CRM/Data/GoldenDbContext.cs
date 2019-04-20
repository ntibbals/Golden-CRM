using Golden_CRM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Data
{
    public class GoldenDbContext : DbContext
    {

        public GoldenDbContext(DbContextOptions<GoldenDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    ID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "jd@doe.com",
                    PhoneNumber = "555-555-5555"
                });

            modelBuilder.Entity<Note>().HasData(
                 new Note
                {
                    ID = 1,
                    CustomerID = 1,
                    Comment = "This is comment one for John Doe"
                },
                 new Note
                {
                    ID = 2,
                    CustomerID = 1,
                    Comment = "This is comment two for John Doe"
                });
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
