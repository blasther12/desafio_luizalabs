using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Core.Interfaces.Repositorys;
using TesteLuizaLabs.Domain.Core.Interfaces.Services;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Domain.Services.Services
{
    public class ServiceCustomerProduct : ServiceBase<CustomerProduct>, IServiceCustomerProduct
    {
        public readonly IRepositoryCustomerProduct _repositoryCustomerProduct;

        public ServiceCustomerProduct(IRepositoryCustomerProduct RepositoryCustomerProduct)
            : base(RepositoryCustomerProduct)
        {
            _repositoryCustomerProduct = RepositoryCustomerProduct;
        }

        public virtual async Task<List<CustomerProduct>> GetByCustomerAsync(long customerId, int page, int quantity)
        {
            return await _repositoryCustomerProduct.GetByCustomerAsync(customerId, page, quantity);
        }

        public virtual async Task<bool> ContainsKeyAsync(string key)
        {
            return await _repositoryCustomerProduct.ContainsKeyAsync(key);
        }
    }
}
