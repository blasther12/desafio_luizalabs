using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Domain.Core.Interfaces.Services
{
    /// <summary>
    /// Realiza a ponte entre a camada de aplicação e o acesso aos repositorios do banco de dados da camada de Dominio
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IServiceBase<TEntity> where TEntity : class
    {

        Task AddAsync(TEntity obj);

        Task<TEntity?> GetByIdAsync(long id);

        Task<IEnumerable<TEntity>> GetAllAsync(int page, int quantity);

        Task UpdateAsync(TEntity obj);

        Task RemoveAsync(TEntity obj);

        ValueTask DisposeAsync();
    }
}
