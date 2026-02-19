using JobMarket.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Ef
{
    /// <summary>
    /// Entity Framework Unit of work that will contain the data context and data repositories.
    /// </summary>
    public class EfUnitOfWork : IDataUnitOfWork
    {
        private readonly JobMarketContext _context;
        public IContractorRepository ContractorRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public IJobRepository JobRepository { get; private set; }
        public IJobOfferRepository JobOfferRepository { get; private set; }
        public ICustomerUserRepository CustomerUserRepository { get; private set; }
        public IContractorUserRepository ContractorUserRepository { get; private set; }

        public EfUnitOfWork(JobMarketContext context)
        {
            _context = context;
            ContractorRepository = new ContractorRepository(_context);
            CustomerRepository = new CustomerRepository(_context);
            JobRepository = new JobRepository(_context);
            JobOfferRepository=new JobOfferRepository(_context);
            CustomerUserRepository = new CustomerUserRepository(_context);
            ContractorUserRepository = new ContractorUserRepository(_context);
        }
        /// <summary>
        /// Save Changes
        /// </summary>
        /// <returns></returns>
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
