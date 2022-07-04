using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Core.Interfaces.Repositorys;
using TesteLuizaLabs.Domain.Core.Interfaces.Services;

namespace TesteLuizaLabs.Domain.Services.Services
{
    public abstract class ServiceBase<TEntity> : IAsyncDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> Repository)
        {
            _repository = Repository;
        }
        public virtual async Task AddAsync(TEntity obj)
        {
            await _repository.AddAsync(obj);
        }
        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int page, int quantity)
        {
            return await _repository.GetAllAsync(page, quantity);
        }
        public virtual async Task UpdateAsync(TEntity obj)
        {
            await _repository.UpdateAsync(obj);
        }
        public virtual async Task RemoveAsync(TEntity obj)
        {
            await _repository.RemoveAsync(obj);
        }

        public virtual async ValueTask DisposeAsync()
        {
            await _repository.DisposeAsync();
        }
    }
}
