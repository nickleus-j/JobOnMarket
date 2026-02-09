using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IContractorRepository: IRepository<Contractor>
    {
        Task<IList<Contractor>> SearchContractor(string searchTerm);
    }
}
