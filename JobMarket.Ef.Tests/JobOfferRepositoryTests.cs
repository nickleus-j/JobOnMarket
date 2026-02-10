using JobMarket.Data.Entity;
using JobMarket.Ef;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace JobMarket.Ef.Tests
{
    public class JobOfferRepositoryTests
    {
        [Fact]
        public async Task OffersForJobsNotAcceptedYetAsync_ReturnsOnlyOffers_ForJobsWhereAcceptedByIsNull()
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "OffersForUnacceptedJobsDb")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                // arrange
                var customer = new Customer { ID = 3, FirstName = "C", LastName = "U" };
                var jobAccepted = new Job { ID = 2, Description = "Accepted job", AcceptedById = 1, AcceptedBy = customer };
                var jobNotAccepted = new Job { ID = 1, Description = "Open job", AcceptedById = null };

                var offerForAcceptedJob = new JobOffer { ID = 20, JobId = jobAccepted.ID, OfferedJob = jobAccepted };
                var offerForOpenJob = new JobOffer { ID = 10, JobId = jobNotAccepted.ID, OfferedJob = jobNotAccepted };

                await context.Customer.AddAsync(customer);
                await context.Job.AddRangeAsync(jobAccepted, jobNotAccepted);
                await context.JobOffer.AddRangeAsync(offerForAcceptedJob, offerForOpenJob);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);

                // act
                var offers = await repo.OffersForJobsNotAcceptedYetAsync();

                // assert
                var list = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<JobOffer>>(offers);
                Assert.True(list.Count()==1);
                Assert.Contains(list, o => o.ID == offerForOpenJob.ID);
            }
        }
        [Fact]
        public async Task MultipleOffersForJobsNotAcceptedYetAsync_ReturnsOnlyOffers_ForJobsWhereAcceptedByIsNull()
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "MultipleOffersForUnacceptedJobsDb")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                // arrange
                var customer = new Customer { ID = 3, FirstName = "C", LastName = "U" };
                var jobAccepted = new Job { ID = 2, Description = "Accepted job", AcceptedById = 1, AcceptedBy = customer };
                var jobNotAccepted = new Job { ID = 1, Description = "Open job", AcceptedById = null };
                var anotherJobNotAccepted = new Job { ID = 3, Description = "Open job again", AcceptedById = null };
                var offerForAcceptedJob = new JobOffer { ID = 20, JobId = jobAccepted.ID, OfferedJob = jobAccepted };
                var offerForOpenJob = new JobOffer { ID = 10, JobId = jobNotAccepted.ID, OfferedJob = jobNotAccepted };
                var offerForOtherOpenJob = new JobOffer { ID = 15, JobId = anotherJobNotAccepted.ID, OfferedJob = anotherJobNotAccepted };

                await context.Customer.AddAsync(customer);
                await context.Job.AddRangeAsync(jobAccepted, jobNotAccepted, anotherJobNotAccepted);
                await context.JobOffer.AddRangeAsync(offerForAcceptedJob, offerForOpenJob, offerForOtherOpenJob);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);

                // act
                var offers = await repo.OffersForJobsNotAcceptedYetAsync();

                // assert
                var list = Assert.IsAssignableFrom<System.Collections.Generic.IEnumerable<JobOffer>>(offers);
                Assert.True(list.Count() > 1);
                Assert.Contains(list, o => o.ID == offerForOpenJob.ID);
            }
        }
        [Fact]
        public async Task AcceptJobOffer_SetsJobAcceptedById_AndPersists()
        {
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "AcceptJobOfferDb")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                // arrange
                var customer = new Customer { ID = 99, FirstName = "Accepter", LastName = "User" };
                var job = new Job { ID = 3, Description = "Job to accept", AcceptedById = null };
                var offer = new JobOffer { ID = 30, JobId = job.ID, OfferedJob = job };

                await context.Customer.AddAsync(customer);
                await context.Job.AddAsync(job);
                await context.JobOffer.AddAsync(offer);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);

                // act
                var returnedJobId = await repo.AcceptJobOffer(offer.ID, customer.ID);

                // assert - returned id and persisted change
                Assert.Equal(job.ID, returnedJobId);

                var persistedJob = await context.Job.FindAsync(job.ID);
                Assert.NotNull(persistedJob);
                Assert.Equal(customer.ID, persistedJob.AcceptedById);
            }
        }
    }
}