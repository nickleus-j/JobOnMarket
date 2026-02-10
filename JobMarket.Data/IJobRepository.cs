using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IJobRepository : IRepository<Job>
    {
        Task AcceptJob(int jobId, int customerId);
        Task RemoveJob(int jobId);
    }
}
