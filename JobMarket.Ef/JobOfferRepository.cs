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
    }
}
