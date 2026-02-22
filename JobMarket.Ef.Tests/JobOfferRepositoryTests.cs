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
        [Fact]
        public async Task OffersForJobsNotAcceptedYetAsync_returns_only_offers_for_unaccepted_jobs()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "AcceptJobOfferDb_OffersForJobsNotAccepted")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                // Ensure DB is created
                context.Database.EnsureCreated();

                var acceptedCustomer = new Customer { ID = 100, FirstName = "Accepted", LastName = "Customer" };
                var acceptedJob = new Job { ID = 1, Description = "Accepted Job", AcceptedBy = acceptedCustomer, AcceptedById = acceptedCustomer.ID };
                var unacceptedJob = new Job { ID = 2, Description = "Unaccepted Job", AcceptedBy = null, AcceptedById = null };

                var offerForAccepted = new JobOffer { ID = 1, JobId = acceptedJob.ID, OfferedJob = acceptedJob };
                var offerForUnaccepted = new JobOffer { ID = 2, JobId = unacceptedJob.ID, OfferedJob = unacceptedJob };

                context.Customer.Add(acceptedCustomer);
                context.Job.AddRange(acceptedJob, unacceptedJob);
                context.JobOffer.AddRange(offerForAccepted, offerForUnaccepted);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);

                // Act
                var offers = (await repo.OffersForJobsNotAcceptedYetAsync()).ToList();

                // Assert
                Assert.Single(offers);
                Assert.Equal(unacceptedJob.ID, offers.Single().JobId);
            }
        }

        [Fact]
        public async Task AcceptJobOffer_sets_AcceptedById_and_returns_jobId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "AcceptJobOfferDb_AcceptedById_and_returns_jobId")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                var job = new Job { ID = 10, Description = "OpenJob", AcceptedBy = null, AcceptedById = null };
                var offer = new JobOffer { ID = 20, JobId = job.ID, OfferedJob = job };

                context.Job.Add(job);
                context.JobOffer.Add(offer);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);
                var customerId = 500;

                // Act
                var returnedJobId = await repo.AcceptJobOffer(offer.ID, customerId);

                // Assert
                Assert.Equal(job.ID, returnedJobId);

                // Reload job to be sure changes were saved
                var reloadedJob = await context.Job.SingleAsync(j => j.ID == job.ID);
                Assert.Equal(customerId, reloadedJob.AcceptedById);
            }
        }

        [Fact]
        public async Task OffersForJob_returns_all_offers_for_given_job_with_paging_parameters()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<JobMarketContext>()
                .UseInMemoryDatabase(databaseName: "AcceptJobOfferDb_AcceptedById_and_paging_parameters")
                .Options;

            using (var context = new JobMarketContext(options))
            {
                context.Database.EnsureCreated();

                var job1 = new Job { ID = 101, Description = "Job101" };
                var job2 = new Job { ID = 102, Description = "Job102" };

                var offersForJob1 = new List<JobOffer>
            {
                new JobOffer { ID = 301, JobId = job1.ID, OfferedJob = job1 },
                new JobOffer { ID = 302, JobId = job1.ID, OfferedJob = job1 },
                new JobOffer { ID = 303, JobId = job1.ID, OfferedJob = job1 }
            };
                var offersForJob2 = new List<JobOffer>
            {
                new JobOffer { ID = 401, JobId = job2.ID, OfferedJob = job2 }
            };

                context.Job.AddRange(job1, job2);
                context.JobOffer.AddRange(offersForJob1);
                context.JobOffer.AddRange(offersForJob2);
                await context.SaveChangesAsync();

                var repo = new JobOfferRepository(context);

                // Use a page and pageSize large enough to retrieve all offers for job1
                int page = 1;
                int pageSize = 10;

                // Act
                var result = (await repo.OffersForJob(job1.ID, page, pageSize)).ToList();

                // Assert
                Assert.Equal(offersForJob1.Count, result.Count);
                Assert.All(result, o => Assert.Equal(job1.ID, o.JobId));
            }
        }
    }
}
