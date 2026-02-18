using JobMarket.Data.Entity;
using JobMarket.Ef;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JobMarket.Ef.Tests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task SearchCustomerAsync_ById_ReturnsMatchingCustomer()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMark").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                context.Customer.Add(new Customer { ID = 23, FirstName = "Lan", LastName = "Doe" });
                context.Customer.Add(new Customer { ID = 24, FirstName = "Kia", LastName = "Smith" });
                await context.SaveChangesAsync();

                var repo = new CustomerRepository(context);

                var result = await repo.SearchCustomerAsync("23");

                Assert.Single(result);
                Assert.Equal(23, result.First().ID);
                Assert.Equal("Doe", result.First().LastName);
            }
        }

        [Fact]
        public async Task SearchCustomerAsync_ByLastName_PartialCaseInsensitive_ReturnsMatches()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMark").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                context.Customer.Add(new Customer { ID = 33, FirstName = "John", LastName = "Moe" });
                context.Customer.Add(new Customer { ID = 34, FirstName = "Alex", LastName = "moe-sample" });
                context.Customer.Add(new Customer { ID = 35, FirstName = "Empty", LastName = "Han" }); // should be ignored
                await context.SaveChangesAsync();

                var repo = new CustomerRepository(context);

                // partial, case-insensitive search
                var result = await repo.SearchCustomerAsync("MoE");

                Assert.Equal(2, result.Count);
                Assert.Contains(result, c => c.ID == 33);
                Assert.Contains(result, c => c.ID == 34);
                Assert.DoesNotContain(result, c => c.ID == 35);
            }
        }

        [Fact]
        public async Task SearchCustomerAsync_NoMatches_ReturnsEmptyList()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMark").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                context.Customer.Add(new Customer { ID = 3, FirstName = "John", LastName = "Doe" });
                await context.SaveChangesAsync();

                var repo = new CustomerRepository(context);

                var result = await repo.SearchCustomerAsync("nonexistent");

                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }
        [Fact]
        public async Task SearchCustomerAsync_Pagination_PartialCaseInsensitive_ReturnsMatches()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkPage").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                context.Customer.Add(new Customer { ID = 33, FirstName = "John", LastName = "Moe" });
                context.Customer.Add(new Customer { ID = 34, FirstName = "Alex", LastName = "moe-sample" });
                context.Customer.Add(new Customer { ID = 35, FirstName = "Empty", LastName = "Han" }); // should be ignored
                context.Customer.Add(new Customer { ID = 32, FirstName = "Joe", LastName = "Samoewa" });
                await context.SaveChangesAsync();

                var repo = new CustomerRepository(context);

                // partial, case-insensitive search
                var result = await repo.SearchCustomerAsync("MoE",1,2);

                Assert.Equal(2, result.Count);
                Assert.Contains(result, c => c.ID == 33);
                Assert.DoesNotContain(result, c => c.ID == 35);
            }
        }
        [Fact]
        public async Task SearchCustomerAsync_Page2_PartialCaseInsensitive_ReturnsMatches()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkPage2").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                context.Customer.Add(new Customer { ID = 33, FirstName = "John", LastName = "Moe" });
                context.Customer.Add(new Customer { ID = 34, FirstName = "Alex", LastName = "moe-sample" });
                context.Customer.Add(new Customer { ID = 35, FirstName = "Empty", LastName = "Han" }); // should be ignored
                context.Customer.Add(new Customer { ID = 32, FirstName = "Joe", LastName = "Samoewa" });
                await context.SaveChangesAsync();

                var repo = new CustomerRepository(context);

                // partial, case-insensitive search
                var result = await repo.SearchCustomerAsync("MoE", 2, 2);

                Assert.Single(result);
            }
        }
    }
}