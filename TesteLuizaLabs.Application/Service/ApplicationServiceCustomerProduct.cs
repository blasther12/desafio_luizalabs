using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Application.Exceptions;
using TesteLuizaLabs.Application.Interfaces;
using TesteLuizaLabs.Domain.Core.Interfaces.Services;
using TesteLuizaLabs.Domain.Entity;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;

namespace TesteLuizaLabs.Application.Service
{
    public class ApplicationServiceCustomerProduct : IApplicationServiceCustomerProduct
    {
        private readonly IServiceCustomerProduct _serviceCustomerProduct;
        private readonly IServiceCustomer _serviceCustomer;
        private readonly IApplicationServiceProduct _serviceProduct;
        private readonly IMapperCustomerProduct _mapperCustomerProduct;
        private readonly IMemoryCache _cache;

        public ApplicationServiceCustomerProduct(IServiceCustomerProduct ServiceCustomerProduct, IServiceCustomer serviceCustomer, IMapperCustomerProduct MapperCustomerProduct, IApplicationServiceProduct serviceProduct, IMemoryCache cache)

        {
            this._serviceCustomerProduct = ServiceCustomerProduct;
            this._serviceCustomer = serviceCustomer;
            this._mapperCustomerProduct = MapperCustomerProduct;
            this._serviceProduct = serviceProduct;
            this._cache = cache;
        }

        /// <summary>
        /// Adiciona um novo produto favorito na base de dados do cliente
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public async Task AddAsync(ProductPostDTO obj)
        {
            //Valida se o id do produto enviado não é nulo ou vazio
            if (string.IsNullOrEmpty(obj.Id))
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.UnknownProduct);

            var customer = await this._serviceCustomer.GetByIdAsync(obj.CustomerId);

            //Valida se existe o cliente na base
            if (customer == null) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.CustomerNotFound);

            var CustomerProduct = customer.CustomerProduct?.FirstOrDefault(x => x.Key == obj.Id);

            //Valida se o produto já existe na lista de favoritos
            if (CustomerProduct is not null) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ExistingFavoriteProduct);

            //Busca o produto via API
            var product = await this._serviceProduct.GetProductAsync(obj.Id);

            //Se o produto não for encontrado retorna erro
            if(product == null) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ProductNotFound);

            var objProduct = this._mapperCustomerProduct.MapperToEntity(new CustomerProductDTO 
            {
                CustomerId = obj.CustomerId,
                Id = product.id
            });

            await this._serviceCustomerProduct.AddAsync(objProduct);
        }

        public async ValueTask DisposeAsync()
        {
            await this._serviceCustomerProduct.DisposeAsync();
        }

        /// <summary>
        /// Busca todos os produtos favorito do cliente
        /// </summary>
        /// <param name="page">Pagina da busca</param>
        /// <param name="quantity">Quantidade de itens por busca</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerProductDTO>> GetAllAsync(long customerId, int page, int quantity)
        {
            string key = $"GetAllAsync-product-{customerId}-{page}-{quantity}";
            //Valida se o item existe no cache de memoria, caso exista retorna dados cacheados
            if (this._cache.TryGetValue(key, out IEnumerable<CustomerProductDTO> result))
            {
                return result;
            }

            List<CustomerProduct> listCustomerProduct = new();

            //Se for informado o ID do cliente é realizado o filtro por cliente, senão buscamos todos
            if (customerId > 0)
            {
                listCustomerProduct = await this._serviceCustomerProduct.GetByCustomerAsync(customerId, page, quantity);
            }
            else
            {
                var enumerableCustomerProduct = await this._serviceCustomerProduct.GetAllAsync(page, quantity);
                listCustomerProduct = enumerableCustomerProduct.ToList();
            }
            //Busca dados complementares diretamente da API
            result = await this._serviceProduct.GetProductListAsync(listCustomerProduct);

            //Se dados existirem armazena em cache
            if (result is not null && result.Any())
            {
                this._cache.Set(key, result, absoluteExpiration: DateTimeOffset.Now.AddMinutes(3));
            }

            return result!;
        }

        /// <summary>
        /// Busca produto favorito por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerProductDTO?> GetByIdAsync(long id)
        {
            var objCustomerProduct = await this._serviceCustomerProduct.GetByIdAsync(id);

            if(objCustomerProduct is null) return null;

            var product = await this._serviceProduct.GetProductAsync(objCustomerProduct.Key!);

            return this._mapperCustomerProduct.MapperToDTO(objCustomerProduct, product!);
        }

        /// <summary>
        /// Remove um produto faorito da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long id)
        {
            var objCustomerProduct = await this._serviceCustomerProduct.GetByIdAsync(id);

            //Valida se o cliente existe na base de dados
            if (objCustomerProduct is null) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.CustomerProductNotFound);

            await this._serviceCustomerProduct.RemoveAsync(objCustomerProduct);

            this._cache.Dispose();
        }

        /// <summary>
        /// Atualiza dados do produto
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CustomerProductDTO obj)
        {
            var objCustomerProduct = this._mapperCustomerProduct.MapperToEntity(obj);
            await this._serviceCustomerProduct.UpdateAsync(objCustomerProduct);
        }
    }
}
