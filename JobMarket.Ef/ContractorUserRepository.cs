using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class ContractorUserRepository : Repository<ContractorUser>, IContractorUserRepository
    {
        public ContractorUserRepository(DbContext context) : base(context)
        {
        }
        private JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }
        public async Task AssociateContractorWithUser(string userId, int contractorId)
        {
            ContractorUser contractorUser = new ContractorUser() { UserId = userId, ContractorId = contractorId };
            await AddAsync(contractorUser);
        }

        public async Task<ContractorUser> GetContractorUserByUserIdAsync(string userId)
        {
            ContractorUser contractorUser = await SingleAsync(c=>c.UserId == userId);
            return contractorUser;
        }

        public async Task RemoveUserFromContractorsAsync(string userId)
        {
            ContractorUser contractorUser = await SingleAsync(c => c.UserId == userId);
            if (contractorUser != null)
            {
                Remove(contractorUser);
            }
        }
    }
}
