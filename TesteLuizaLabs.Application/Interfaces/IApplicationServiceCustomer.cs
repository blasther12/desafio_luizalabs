using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;

namespace TesteLuizaLabs.Application.Interfaces
{
    public interface IApplicationServiceCustomer
    {
        Task AddAsync(CustomerDTO obj);

        Task<CustomerDTO?> GetByIdAsync(long id);

        Task<IEnumerable<CustomerDTO>> GetAllAsync(int page, int quantity);

        Task UpdateAsync(CustomerDTO obj);

        Task RemoveAsync(long id);

        ValueTask DisposeAsync();
    }
}
