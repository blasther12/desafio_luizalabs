using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Domain.Entity;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces
{
    public interface IMapperCustomerProduct
    {
        #region Mappers

        CustomerProduct MapperToEntity(CustomerProductDTO customerProductDTO);

        IEnumerable<CustomerProductDTO> MapperListCustomerProduct(IEnumerable<CustomerProduct> customerProduct);

        CustomerProductDTO MapperToDTO(CustomerProduct customerProduct, Domain.Model.Product product);

        #endregion
    }
}
