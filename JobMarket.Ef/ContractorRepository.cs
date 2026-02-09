using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class ContractorRepository : Repository<Contractor>, IContractorRepository
    {
        public ContractorRepository(DbContext context) : base(context)
        {
        }
        public JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }

        public async Task<IList<Contractor>> SearchContractor(string searchTerm)
        {
            int Id;
            bool isNumber = int.TryParse(searchTerm, out Id);
            if (isNumber)
            {
                return await FindAsync(c => c.ID == Id);
            }
            string loweredTerm = $"%{searchTerm.ToLower()}%";
            return await FindAsync(c => !String.IsNullOrEmpty(c.Name) && EF.Functions.Like(c.Name.ToLower(), loweredTerm));
        }
    }
}
