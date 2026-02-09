using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JobMarket.Ef
{
    public class JobMarketContext : IdentityDbContext
    {
        public JobMarketContext()
        {
        }

        public JobMarketContext(DbContextOptions<JobMarketContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Contractor> Contractor { get; set; }
        public virtual DbSet<Job> Job { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer { ID = 1, FirstName = "John",LastName="Doe" },
                new Customer { ID = 2, FirstName = "Jane", LastName = "Dow" }
            );
            modelBuilder.Entity<Contractor>().HasData(new Contractor { ID = 1,Name="Acme co",Rating=3 }
            , new Contractor { ID = 2, Name = "Generic Co", Rating = 4 });
        }
    }
}
