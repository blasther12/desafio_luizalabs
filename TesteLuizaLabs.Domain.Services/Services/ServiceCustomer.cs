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
    public class ServiceCustomer : ServiceBase<Customer>, IServiceCustomer
    {
        public readonly IRepositoryCustomer _repositoryCustomer;

        public ServiceCustomer(IRepositoryCustomer RepositoryCustomer)
            : base(RepositoryCustomer)
        {
            _repositoryCustomer = RepositoryCustomer;
        }

        public virtual async Task<bool> CustomerExistsAsync(string email)
        {
            return await _repositoryCustomer.CustomerExistsAsync(email);
        }

    }
}
