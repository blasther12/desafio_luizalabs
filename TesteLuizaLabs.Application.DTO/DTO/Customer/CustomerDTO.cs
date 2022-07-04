using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Application.DTO.DTO
{
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
