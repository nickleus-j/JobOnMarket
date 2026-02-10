using System.Linq;
using System.Threading.Tasks;
using JobMarket.Ef;
using JobMarket.Data.Entity;
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
    }
}