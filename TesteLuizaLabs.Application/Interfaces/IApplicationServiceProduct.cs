using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Application.Interfaces
{
    public interface IApplicationServiceProduct
    {
        Task<Domain.Model.Product?> GetProductAsync(string id);
        Task<List<CustomerProductDTO>> GetProductListAsync(IEnumerable<CustomerProduct> customerProducts);
    }
}
