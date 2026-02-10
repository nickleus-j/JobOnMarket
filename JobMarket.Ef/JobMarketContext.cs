using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Identity;
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
        public virtual DbSet<JobOffer> JobOffer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer { ID = 1, FirstName = "John",LastName="Doe" },
                new Customer { ID = 2, FirstName = "Jane", LastName = "Dow" }
            );
            modelBuilder.Entity<Contractor>().HasData(new Contractor { ID = 1,Name="Acme co",Rating=3 }
            , new Contractor { ID = 2, Name = "Generic Co", Rating = 4 });

            
            string[] roleNames = { "Customer", "Contractor" };
            string[] emails = { "customer@customer.com", "Contractor@Contractor.com" };
            string[] roleIds = { "8e445865-0000-0000-0000-9443d048cdb9", "8e445865-aaaa-aaaa-aaaa-9443d048cdb9" };

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "00445865-0000-0000-0000-9443d048cdb9", Name = "General", NormalizedName = "General", ConcurrencyStamp = "00445865-0000-0000-0000-9443d048cd00" });
            for (int i = 0; i < emails.Length; i++)
            {
                modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = roleIds[i], Name = roleNames[i], NormalizedName = roleNames[i],ConcurrencyStamp= roleIds[i] });
            }
            var hasher = new PasswordHasher<string>(); 
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "8e445865-0000-4543-0000-9443d048cdb9", // Static GUID
                UserName = emails[0],
                NormalizedUserName = emails[0].ToUpper(),
                Email = emails[0],
                NormalizedEmail = emails[0].ToUpper(),
                PasswordHash = "aPassword123!",
                SecurityStamp= "8e445865-0000-aaaa-0000-9443d048cdb9",
                ConcurrencyStamp= "8e445865-0000-aaaa-0000-9443d048cd00"
            });
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "8e445865-1111-4543-1111-9443d048cdb9", // Static GUID
                UserName = emails[1],
                NormalizedUserName = emails[1].ToUpper(),
                Email = emails[1],
                NormalizedEmail = emails[1].ToUpper(),
                PasswordHash = "bPassword123!",
                SecurityStamp = "8e445865-1111-aaaa-1111-9443d048cdb9",
                ConcurrencyStamp = "8e445865-1111-aaaa-1111-9443d048cd00"
            });
            for(int i = 0; i < roleIds.Length; i++)
            {
                modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = roleIds.ElementAt(i), UserId = "8e445865-aaaa-4543-aaaa-9443d048cdb9".Replace("a",i.ToString()) });
            }
        }
    }
}
