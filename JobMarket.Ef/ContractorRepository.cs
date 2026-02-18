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
        private JobMarketContext marketContext
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
        public async Task<Contractor> GetContractorByUserIdAsync(string userName)
        {
            var identityUser=await marketContext.Users.SingleAsync(u => u.UserName == userName); // Ensure user exists
            var contractorUser = await marketContext.ContractorUser.SingleOrDefaultAsync(cu => cu.UserId == identityUser.Id);
            if (contractorUser != null)
            {
                return await SingleAsync(c => c.ID == contractorUser.ContractorId);
            }
            throw new InvalidOperationException($"No contractor found for user ID: {userName}");
        }
    }
}
