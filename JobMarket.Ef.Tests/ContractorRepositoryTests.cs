using JobMarket.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace JobMarket.Ef.Tests
{
    public class ContractorRepositoryTests
    {

        [Fact]
        public async Task SearchContractor_WithNumericString_ReturnsContractorById()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMark").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                // Seed data
                context.Contractor.AddRange(
                    new Contractor { ID = 3, Name = "Hyn co", Rating = 3 },
                    new Contractor { ID = 4, Name = "Generic Co", Rating = 4 }
                );
                context.SaveChanges();

                var repo = new ContractorRepository(context);

                var result = await repo.SearchContractor("3");

                Assert.Single(result);
                Assert.Equal(3, result.First().ID);
                Assert.Equal("Hyn co", result.First().Name);
            }
        }

        [Fact]
        public async Task SearchContractor_WithNameSubstring_IsCaseInsensitiveAndPartialMatch()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkWithMatch").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                // Seed data
                context.Contractor.AddRange(
                    new Contractor { ID = 11, Name = "Gin co", Rating = 3 },
                    new Contractor { ID = 12, Name = "Generic Co", Rating = 4 },
                    new Contractor { ID = 13, Name = "Another Gin Branch", Rating = 5 }
                );
                context.SaveChanges();

                var repo = new ContractorRepository(context);

                var result = await repo.SearchContractor("Gin");

                // Expect both entries that contain "acme" in the name (case-insensitive)
                Assert.Equal(2, result.Count);
                Assert.Contains(result, c => c.ID == 11 && c.Name == "Gin co");
                Assert.Contains(result, c => c.ID == 13 && c.Name == "Another Gin Branch");
            }
        }
        [Fact]
        public async Task SearchContractorPagination_WithNameSubstring_IsCaseInsensitiveAndPartialMatch()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkWithMatchPage").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                // Seed data
                context.Contractor.AddRange(
                    new Contractor { ID = 11, Name = "Gin co", Rating = 3 },
                    new Contractor { ID = 12, Name = "Generic Co", Rating = 4 },
                    new Contractor { ID = 13, Name = "Another Gin Branch", Rating = 5 },
                    new Contractor { ID = 14, Name = "Some Gin co", Rating = 3 }
                );
                context.SaveChanges();

                var repo = new ContractorRepository(context);

                var result = await repo.SearchContractor("Gin",1,2);

                // Expect both entries that contain "acme" in the name (case-insensitive)
                Assert.Equal(2, result.Count);
                Assert.Contains(result, c => c.ID == 11 && c.Name == "Gin co");
                Assert.Contains(result, c => c.ID == 13 && c.Name == "Another Gin Branch");
            }
        }
        [Fact]
        public async Task SearchContractorPage2_WithNameSubstring_IsCaseInsensitiveAndPartialMatch()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkWithMatchPage2").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                // Seed data
                context.Contractor.AddRange(
                    new Contractor { ID = 11, Name = "Gin co", Rating = 3 },
                    new Contractor { ID = 12, Name = "Generic Co", Rating = 4 },
                    new Contractor { ID = 13, Name = "Another Gin Branch", Rating = 5 },
                    new Contractor { ID = 14, Name = "Some Gin co", Rating = 3 }
                );
                context.SaveChanges();

                var repo = new ContractorRepository(context);

                var result = await repo.SearchContractor("Gin", 2, 2);

                Assert.Single(result);
            }
        }
        [Fact]
        public async Task SearchContractor_NoMatches_ReturnsEmptyList()
        {
            DbContextOptions<JobMarketContext> dbContextOptions = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "jobMarkSearch").Options;

            using (var context = new JobMarketContext(dbContextOptions))
            {
                context.Database.EnsureCreated();

                // Seed data
                context.Contractor.AddRange(
                    new Contractor { ID = 21, Name = "Was co", Rating = 3 },
                    new Contractor { ID = 22, Name = "Gar", Rating = 4 }
                );
                context.SaveChanges();

                var repo = new ContractorRepository(context);

                var result = await repo.SearchContractor("nonexistent");

                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }
        private static JobMarketContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new JobMarketContext(options);
        }

        [Fact]
        public async Task GetContractorByUserIdAsync_ReturnsContractor_WhenUserHasContractor()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                var user = new IdentityUser { Id = "user-1", UserName = "alice" };
                context.Users.Add(user);

                var contractor = new Contractor { ID = 3, Name = "Acme Corp" };
                context.Contractor.Add(contractor);

                var mapping = new ContractorUser { ID = 3, UserId = user.Id, ContractorId = contractor.ID };
                context.ContractorUser.Add(mapping);

                await context.SaveChangesAsync();
            }

            using (var context = CreateInMemoryContext(dbName))
            {
                var repo = new ContractorRepository(context);

                // Act
                var result = await repo.GetContractorByUserIdAsync("alice");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(3, result.ID);
                Assert.Equal("Acme Corp", result.Name);
            }
        }

        [Fact]
        public async Task GetContractorByUserIdAsync_Throws_WhenUserHasNoContractorMapping()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                var user = new IdentityUser { Id = "user-2", UserName = "bob" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = CreateInMemoryContext(dbName))
            {
                var repo = new ContractorRepository(context);

                // Act & Assert
                var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    await repo.GetContractorByUserIdAsync("bob");
                });

                Assert.Contains("No contractor found for user ID", ex.Message);
            }
        }

        [Fact]
        public async Task GetContractorByUserIdAsync_Throws_WhenUserNotFound()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                // no users seeded
                await context.SaveChangesAsync();
            }

            using (var context = CreateInMemoryContext(dbName))
            {
                var repo = new ContractorRepository(context);

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    // underlying SingleAsync on Users should throw because the user does not exist
                    await repo.GetContractorByUserIdAsync("nonexistent");
                });
            }
        }
    }
}