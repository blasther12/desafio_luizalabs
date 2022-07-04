using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO.Auth;

namespace TesteLuizaLabs.Application.Interfaces
{
    public interface IApplicationServiceAuth
    {
        TokenResultDTO Authenticate(AuthDTO user);
    }
}
