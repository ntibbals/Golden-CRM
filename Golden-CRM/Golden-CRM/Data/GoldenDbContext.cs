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

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
