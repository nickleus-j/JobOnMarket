using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class CustomerUserRepository : Repository<CustomerUser>, ICustomerUserRepository
    {
        public CustomerUserRepository(DbContext context) : base(context)
        {
        }
        private JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }
        public async Task AssociateCustomerWithUser(string userId, int customerId)
        {
            CustomerUser customerUser = new CustomerUser() { UserId = userId, CustomerId = customerId };
            await AddAsync(customerUser);
        }

        public async Task<CustomerUser> GetCustomerUserByUserIdAsync(string userId)
        {
            CustomerUser customer=await SingleAsync(x => x.UserId == userId);
            return customer;
        }

        public async Task RemoveUserFromCustomersAsync(string userId)
        {
            CustomerUser customer = await SingleAsync(x => x.UserId == userId);
            if (customer != null)
            {
                 Remove(customer);
            }
        }
    }
}
