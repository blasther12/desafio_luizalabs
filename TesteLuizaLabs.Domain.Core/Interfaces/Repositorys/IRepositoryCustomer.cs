using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Domain.Core.Interfaces.Repositorys
{
    public interface IRepositoryCustomer : IRepositoryBase<Customer>
    {
        Task<bool> CustomerExistsAsync(string email);
    }
}
