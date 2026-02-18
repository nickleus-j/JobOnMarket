using JobMarket.Data;
using JobMarket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class CustomerRepository:Repository<Customer>,ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        private JobMarketContext marketContext
        {
            get { return Context as JobMarketContext; }
        }

        public async Task<IList<Customer>> SearchCustomerAsync(string searchTerm)
        {
            int Id;
            bool isNumber=int.TryParse(searchTerm, out Id);
            if (isNumber)
            {
                return await FindAsync(c=>c.ID==Id);
            }
            string loweredTerm = $"%{searchTerm.ToLower()}%";
            return await FindAsync(c => !String.IsNullOrEmpty(c.LastName) && EF.Functions.Like(c.LastName.ToLower(), loweredTerm));
        }
        public async Task<IList<Customer>> SearchCustomerAsync(string searchTerm,int page,int pageSize=10)
        {
            string loweredTerm = $"%{searchTerm.ToLower()}%";
            return await FindAsync(c => !String.IsNullOrEmpty(c.LastName) && EF.Functions.Like(c.LastName.ToLower(), loweredTerm),page,pageSize);
        }
    }
}
