using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IJobOfferRepository : IRepository<JobOffer>
    {
        Task<IEnumerable<JobOffer>> OffersForJobsNotAcceptedYetAsync();
    }
}
