using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Domain.Core.Interfaces.Services
{
    public interface IServiceCustomer : IServiceBase<Customer>
    {
        Task<bool> CustomerExistsAsync(string email);
    }
}
