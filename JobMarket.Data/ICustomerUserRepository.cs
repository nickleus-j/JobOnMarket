using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface ICustomerUserRepository : IRepository<CustomerUser>
    {
        Task AssociateCustomerWithUser(string userId, int customerId);
        Task<CustomerUser> GetCustomerUserByUserIdAsync(string userId);
        Task RemoveUserFromCustomersAsync(string userId);
    }
}
