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
    public class MapperCustomer : IMapperCustomer
    {
        #region properties

        List<CustomerDTO> customerDTOs = new();

        #endregion


        #region methods

        public Customer MapperToEntity(CustomerDTO customerDTO)
        {
            Customer customer = new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
            };

            return customer;

        }


        public IEnumerable<CustomerDTO> MapperListCustomer(IEnumerable<Customer> customers)
        {
            foreach (var item in customers)
            {


                CustomerDTO customerDTO = new CustomerDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email
                };

                customerDTOs.Add(customerDTO);

            }

            return customerDTOs;
        }

        public CustomerDTO MapperToDTO(Customer customer)
        {

            CustomerDTO customerDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email
            };

            return customerDTO;

        }

        #endregion
    }
}
