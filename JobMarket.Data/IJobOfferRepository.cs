using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IJobOfferRepository : IRepository<JobOffer>
    {
        Task<IEnumerable<JobOffer>> OffersForJobsNotAcceptedYetAsync();
        Task<int> AcceptJobOffer(int offerId, int customerId);
        Task<IEnumerable<JobOffer>> OffersForJob(int jobId,int page,int pageSize);
    }
}
