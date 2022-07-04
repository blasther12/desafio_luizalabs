using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Core.Interfaces.Repositorys;
using TesteLuizaLabs.Infrastructure.Data;

namespace TesteLuizaLabs.Infrastructure.Repository.Repositorys
{
    public abstract class RepositoryBase<TEntity> : IAsyncDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly TesteLuizaLabsContext _context;

        public RepositoryBase(TesteLuizaLabsContext Context)
        {
            this._context = Context;
        }

        /// <summary>
        /// Adiciona um novo registro no Banco de dados
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity obj)
        {
            try
            {
                await this._context.Set<TEntity>().AddAsync(obj);
                await this._context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Busca um registro pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            return await this._context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int page, int quantity)
        {
            return await this._context.Set<TEntity>()
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
        }

        /// <summary>
        /// Atualiza um registro na base de dados
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity obj)
        {

            try
            {
                this._context.Entry(obj).State = EntityState.Modified;
                await this._context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Remove um item da base
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(TEntity obj)
        {
            try
            {
                this._context.Set<TEntity>().Remove(obj);
                await this._context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }


        }

        /// <summary>
        /// Limpa o contexto
        /// </summary>
        /// <returns></returns>
        public virtual async ValueTask DisposeAsync()
        {
            await this._context.DisposeAsync();
        }


    }
}
