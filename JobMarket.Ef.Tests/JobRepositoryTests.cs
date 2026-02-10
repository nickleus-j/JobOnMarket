using JobMarket.Data.Entity;
using JobMarket.Ef;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace JobMarket.Ef.Tests
{
    public class JobRepositoryTests
    {
        [Fact]
        public async Task AcceptJob_SetsAcceptedById_AndPersists()
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "AcceptJobDb")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                var job = new Job { ID = 101, Description = "Test job", AcceptedById = null };
                await context.Job.AddAsync(job);
                await context.SaveChangesAsync();

                var repo = new JobRepository(context);

                await repo.AcceptJob(101, 1);

                var persisted = await context.Job.FindAsync(101);
                Assert.NotNull(persisted);
                Assert.Equal(1, persisted.AcceptedById);
            }
        }

        [Fact]
        public async Task RemoveJob_DeletesJob_FromDatabase()
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "RemoveJobDb")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                var job = new Job { ID = 202, Description = "To be removed" };
                await context.Job.AddAsync(job);
                await context.SaveChangesAsync();

                var repo = new JobRepository(context);
                await repo.RemoveJob(202);

                var removed = await context.Job.FindAsync(202);
                Assert.Null(removed);
            }
        }
    }
}