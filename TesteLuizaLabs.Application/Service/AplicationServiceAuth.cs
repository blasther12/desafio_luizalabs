using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO.Auth;
using TesteLuizaLabs.Application.Interfaces;

namespace TesteLuizaLabs.Application.Service
{
    public class AplicationServiceAuth : IApplicationServiceAuth
    {
        public IConfiguration _configuration;

        public AplicationServiceAuth(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Método que realiza a autenticação para a utilização da API
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public TokenResultDTO Authenticate(AuthDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //Recupera dados do appsettings.json
            var key = Encoding.ASCII.GetBytes(this._configuration["jwt:key"]);
            var usernameBase = this._configuration["user:username"];
            var passwordBase = this._configuration["user:password"];

            //Valida usuario e senha
            if (user.UserName.Equals(usernameBase) && user.Password.Equals(passwordBase))
            {
                //Cria token de acesso
                var tokenDescriptor = new SecurityTokenDescriptor { Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("roles", "adm")
                }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Expires = DateTime.UtcNow.AddHours(1)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                string tokenValue = tokenHandler.WriteToken(token);
                DateTime expires = tokenDescriptor.Expires.Value;

                return new TokenResultDTO { Success = true, Token = tokenValue, Expires = expires };
            }
            else
            {
                return new TokenResultDTO { Success = false };
            }
        }
    }
}
