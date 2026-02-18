using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IContractorRepository: IRepository<Contractor>
    {
        Task<IList<Contractor>> SearchContractor(string searchTerm);
        Task<IList<Contractor>> SearchContractor(string searchTerm, int page, int pageSize = 10);
        Task<Contractor> GetContractorByUserIdAsync(string userName);
    }
}
