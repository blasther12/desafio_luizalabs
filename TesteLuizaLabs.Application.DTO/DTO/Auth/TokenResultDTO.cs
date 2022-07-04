using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Application.DTO.DTO.Auth
{
    public class TokenResultDTO
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public DateTime? Expires { get; set; }
    }
}
