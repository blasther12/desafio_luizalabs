using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Domain.Entity
{
    [Table("customerproduct")]
    public class CustomerProduct : BaseEntity
    {
        public CustomerProduct()
        {
            Date = DateTime.Now;
        }

        public long CustomerId { get; set; }
        public string? Key { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer? Customer { get; set; }
    }
}
