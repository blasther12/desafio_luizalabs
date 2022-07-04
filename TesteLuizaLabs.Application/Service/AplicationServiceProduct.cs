using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Application.Interfaces;
using TesteLuizaLabs.Domain.Entity;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;

namespace TesteLuizaLabs.Application.Service
{
    public class AplicationServiceProduct : IApplicationServiceProduct
    {
        private readonly IHttp _client;
        private readonly IMapperCustomerProduct _mapperCustomerProduct;

        private readonly string _url = "http://challenge-api.luizalabs.com/";
        public AplicationServiceProduct(IHttp client, IMapperCustomerProduct mapperCustomerProduct)
        {
            this._client = client;
            this._mapperCustomerProduct = mapperCustomerProduct;
        }

        /// <summary>
        /// Busca um produto diretamente da api de produtos da luizalabs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Domain.Model.Product?> GetProductAsync(string id)
        {
            var product = await this._client.Get<Domain.Model.Product>($"{_url}api/product/{id}/");

            return product;
        }

        /// <summary>
        /// Busca uma lista de produtos diretamente da api de produtos da luizalabs
        /// </summary>
        /// <param name="customerProducts"></param>
        /// <returns></returns>
        public async Task<List<CustomerProductDTO>> GetProductListAsync(IEnumerable<CustomerProduct> customerProducts)
        {
            List<CustomerProductDTO> customerProductsList = new();

            foreach(var customerProduct in customerProducts)
            {
                var product = await this._client.Get<Domain.Model.Product>($"{_url}api/product/{customerProduct.Key}/");

                if (product is null)
                    continue;
                //Caso exista adiciona a lista de retorno
                customerProductsList.Add(this._mapperCustomerProduct.MapperToDTO(customerProduct, product));
            }

            return customerProductsList;
        }
    }
}
