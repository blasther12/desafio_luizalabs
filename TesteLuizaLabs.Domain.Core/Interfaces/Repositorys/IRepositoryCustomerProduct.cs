using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Domain.Core.Interfaces.Repositorys
{
    public interface IRepositoryCustomerProduct : IRepositoryBase<CustomerProduct>
    {
        Task<List<CustomerProduct>> GetByCustomerAsync(long customerId, int page, int quantity);
        Task<bool> ContainsKeyAsync(string key);
    }
}
