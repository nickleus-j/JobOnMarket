using System;
using System.Collections.Generic;
using System.Text;

namespace JobMarket.Data
{
    public interface IDataWorkUnit
    {
        void Dispose();
        Task<int> CompleteAsync();
        IContractorRepository ContractorRepository { get; }
        ICustomerRepository CustomerRepository { get; }
    }
}
