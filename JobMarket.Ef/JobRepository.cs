using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace JobMarket.Ef
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(DbContext context) : base(context)
        {
        }

        private JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }
        public async Task AcceptJob(int jobId, int customerId)
        {
            var job = await SingleAsync(j => j.ID == jobId);
            job.AcceptedById = customerId;
            await marketContext.SaveChangesAsync();
        }
        public async Task RemoveJob(int jobId)
        {
            var job = await SingleAsync(j => j.ID == jobId);
            Remove(job);
            await marketContext.SaveChangesAsync();
        }
    }
}
