using JobMarket.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    public class EfWorkUnit : IDataWorkUnit
    {
        private readonly JobMarketContext _context;
        public IContractorRepository ContractorRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public EfWorkUnit(JobMarketContext context)
        {
            _context = context;
            ContractorRepository = new ContractorRepository(_context);
            CustomerRepository = new CustomerRepository(_context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
