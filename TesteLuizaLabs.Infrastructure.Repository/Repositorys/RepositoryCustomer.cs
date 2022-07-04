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
    public class RepositoryCustomer : RepositoryBase<Customer>, IRepositoryCustomer
    {
        private readonly TesteLuizaLabsContext _context;
        public RepositoryCustomer(TesteLuizaLabsContext Context)
            : base(Context)
        {
            _context = Context;
        }

        /// <summary>
        /// Valida se já existe um cliente cadastrado com o e-mail na base de dados
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual async Task<bool> CustomerExistsAsync(string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email!.ToLower().Equals(email.ToLower()));
            return customer != null;
        }
    }
}
