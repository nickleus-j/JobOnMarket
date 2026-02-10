using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class JobOfferRepository : Repository<JobOffer>, IJobOfferRepository
    {
        public JobOfferRepository(DbContext context) : base(context)
        {
        }

        private JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }
        public async Task<IEnumerable<JobOffer>> OffersForJobsNotAcceptedYetAsync()
        {
            var offers = await marketContext.JobOffer.Include(i => i.OfferedJob).Where(o=>o.OfferedJob.AcceptedBy==null).ToListAsync();
            return offers;
        }
        public async Task<int> AcceptJobOffer(int offerId, int customerId)
        {
            var offer = await SingleAsync(j => j.ID==offerId);
            int jobId = offer.JobId;
            var job = await marketContext.Job.SingleOrDefaultAsync(j => j.ID == jobId);
            job.AcceptedById = customerId;
            await marketContext.SaveChangesAsync();
            return jobId;
        }
    }
}
