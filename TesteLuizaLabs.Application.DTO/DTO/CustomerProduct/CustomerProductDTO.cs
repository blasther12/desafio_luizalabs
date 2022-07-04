using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Application.DTO.DTO
{
    public class CustomerProductDTO
    {
        public string? Id { get; set; }
        public long CustomerId { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
    }
}
