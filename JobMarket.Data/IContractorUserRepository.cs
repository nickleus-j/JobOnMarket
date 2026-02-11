using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IContractorUserRepository : IRepository<ContractorUser>
    {
        Task AssociateContractorWithUser(string userId, int contractorId);
        Task<ContractorUser> GetContractorUserByUserIdAsync(string userId);
        Task RemoveUserFromContractorsAsync(string userId);
    }
}
