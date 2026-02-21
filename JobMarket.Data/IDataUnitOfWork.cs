using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IDataUnitOfWork
    {
        void Dispose();
        Task<int> CompleteAsync();
        IContractorRepository ContractorRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IJobRepository JobRepository { get; }
        IJobOfferRepository JobOfferRepository { get; }
        ICustomerUserRepository CustomerUserRepository { get;  }
        IContractorUserRepository ContractorUserRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
    }
}
