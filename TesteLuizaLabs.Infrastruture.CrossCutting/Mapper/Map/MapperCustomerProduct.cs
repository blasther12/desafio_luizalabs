using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Domain.Entity;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.Map
{
    public class MapperCustomerProduct : IMapperCustomerProduct
    {
        #region properties

        List<CustomerProductDTO> customerProductDTOs = new();

        #endregion


        #region methods

        public CustomerProduct MapperToEntity(CustomerProductDTO customerProductDTO)
        {
            CustomerProduct customerProduct = new CustomerProduct
            {
                Key = customerProductDTO.Id,
                CustomerId = customerProductDTO.CustomerId
            };

            return customerProduct;

        }


        public IEnumerable<CustomerProductDTO> MapperListCustomerProduct(IEnumerable<CustomerProduct> customerProducts)
        {
            foreach (var item in customerProducts)
            {


                CustomerProductDTO customerProductDTO = new CustomerProductDTO
                {
                    Id = item.Key,
                    CustomerId = item.CustomerId
                };

                customerProductDTOs.Add(customerProductDTO);

            }

            return customerProductDTOs;
        }

        public CustomerProductDTO MapperToDTO(CustomerProduct customerProduct, Domain.Model.Product product)
        {

            CustomerProductDTO customerProductDTO = new CustomerProductDTO
            {
                Id = customerProduct.Key,
                CustomerId= customerProduct.CustomerId,
                Image = product.image,
                Price = product.price,
                Title = product.title
            };

            return customerProductDTO;

        }

        #endregion
    }
}
