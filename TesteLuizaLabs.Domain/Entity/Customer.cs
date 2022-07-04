using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Domain.Entity
{
    [Table("customers")]
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Date = DateTime.Now;
        }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<CustomerProduct>? CustomerProduct { get; set; }
    }
}
