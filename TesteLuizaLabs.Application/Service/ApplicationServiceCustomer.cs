using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Application.Exceptions;
using TesteLuizaLabs.Application.Interfaces;
using TesteLuizaLabs.Domain.Core.Interfaces.Services;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;
using TesteLuizaLabs.Infrastruture.CrossCutting.Validations;

namespace TesteLuizaLabs.Application.Service
{
    public class ApplicationServiceCustomer : IApplicationServiceCustomer
    {
        private readonly IServiceCustomer _serviceCustomer;
        private readonly IMapperCustomer _mapperCustomer;

        public ApplicationServiceCustomer(IServiceCustomer ServiceCustomer, IMapperCustomer MapperCustomer)

        {
            this._serviceCustomer = ServiceCustomer;
            this._mapperCustomer = MapperCustomer;
        }

        /// <summary>
        /// Adiciona um cliente a base de dados
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task AddAsync(CustomerDTO obj)
        {
            //Valida se as informações não são nulas ou vazias
            if (string.IsNullOrEmpty(obj.Name) || string.IsNullOrEmpty(obj.Email)) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.IncompleteData);

            //Valida o formato do e-mail
            if(!obj.Email.isValidEmail())
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.InvalidEmail);

            //Valida se já esxiste cliente com este e-mail cadastrado, caso exista retorna erro.
            var exists = await this._serviceCustomer.CustomerExistsAsync(obj.Email);

            if (!exists)
            {
                var objCustomer = this._mapperCustomer.MapperToEntity(obj);
                await this._serviceCustomer.AddAsync(objCustomer);
            }
            else
            {
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ExistingCustomer);
            }
            
        }

        public async ValueTask DisposeAsync()
        {
            await this._serviceCustomer.DisposeAsync();
        }

        /// <summary>
        /// Buscar todos os clientes
        /// </summary>
        /// <param name="page">Pagina da busca</param>
        /// <param name="quantity">Quantidade de itens por busca</param>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerDTO>> GetAllAsync(int page, int quantity)
        {
            var objCustomer = await this._serviceCustomer.GetAllAsync(page, quantity);
            return this._mapperCustomer.MapperListCustomer(objCustomer);
        }

        /// <summary>
        /// Busca Cliente por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerDTO?> GetByIdAsync(long id)
        {
            var objCustomer = await this._serviceCustomer.GetByIdAsync(id);

            if (objCustomer is null) return null;

            return this._mapperCustomer.MapperToDTO(objCustomer);
        }

        /// <summary>
        /// Remove um cliente da base de dados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(long id)
        {
            var objCustomer = await this._serviceCustomer.GetByIdAsync(id);

            //Valida se o cliente existe
            if (objCustomer is null) 
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.CustomerNotFound);

            await this._serviceCustomer.RemoveAsync(objCustomer);
        }

        /// <summary>
        /// Atualiza informações do usuario
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task UpdateAsync(CustomerDTO obj)
        {
            //Valida se as informações não são nulas ou vazias
            if (string.IsNullOrEmpty(obj.Name) || string.IsNullOrEmpty(obj.Email))
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.IncompleteData);

            //Valida o formato do e-mail
            if (!obj.Email.isValidEmail())
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.InvalidEmail);

            //Valida se já esxiste cliente com este e-mail cadastrado, caso exista retorna erro.
            var exists = await this._serviceCustomer.CustomerExistsAsync(obj.Email);

            if (!exists)
            {
                var objCustomer = this._mapperCustomer.MapperToEntity(obj);
                await this._serviceCustomer.UpdateAsync(objCustomer);
            }
            else
            {
                throw new TesteLuizaLabsExceptions(TesteLuizaLabsExceptions.Type.ExistingCustomer);
            }
        }
    }
}
