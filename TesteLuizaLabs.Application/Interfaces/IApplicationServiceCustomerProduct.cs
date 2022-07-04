using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;

namespace TesteLuizaLabs.Application.Interfaces
{
    public interface IApplicationServiceCustomerProduct
    {
        Task AddAsync(ProductPostDTO obj);

        Task<CustomerProductDTO?> GetByIdAsync(long id);

        Task<IEnumerable<CustomerProductDTO>> GetAllAsync(long customerId, int page, int quantity);

        Task UpdateAsync(CustomerProductDTO obj);

        Task RemoveAsync(long id);

        ValueTask DisposeAsync();
    }
}
