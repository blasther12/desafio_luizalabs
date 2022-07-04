using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Domain.Core.Interfaces.Repositorys;
using TesteLuizaLabs.Domain.Entity;
using TesteLuizaLabs.Infrastructure.Data;

namespace TesteLuizaLabs.Infrastructure.Repository.Repositorys
{
    public class RepositoryCustomerProduct : RepositoryBase<CustomerProduct>, IRepositoryCustomerProduct
    {
        private readonly TesteLuizaLabsContext _context;
        public RepositoryCustomerProduct(TesteLuizaLabsContext Context)
            : base(Context)
        {
            _context = Context;
        }

        /// <summary>
        /// Busca produtos favorito pelo ID do cliente
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="page"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public virtual async Task<List<CustomerProduct>> GetByCustomerAsync(long customerId, int page, int quantity)
        {
            return await _context.CustomerProducts
                .Where(x => x.CustomerId == customerId)
                .Skip((page - 1) * quantity)
                .Take(quantity)
                .ToListAsync();
        }

        /// <summary>
        /// Valida se já existe um produto cadastrado na lista de favoritos do cliente
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<bool> ContainsKeyAsync(string key)
        {
            var product = await _context.CustomerProducts.FirstOrDefaultAsync(x => x.Key == key);
            return product != null;
        }
    }
}
