using JobMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<IList<Customer>> SearchCustomerAsync(string searchTerm);
        Task<IList<Customer>> SearchCustomerAsync(string searchTerm, int page, int pageSize = 10);
    }
}
