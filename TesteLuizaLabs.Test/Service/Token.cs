using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO.Auth;
using TesteLuizaLabs.Application.Service;

namespace TesteLuizaLabs.Test.Service
{
    public class Token
    {
        /// <summary>
        /// Geramos um token para teste
        /// </summary>
        /// <param name="cache"></param>
        /// <returns></returns>
        public static string GetToken(IMemoryCache cache)
        {

            if (cache.TryGetValue("key", out string resultCache))
            {
                return resultCache;
            }

            var auth = new AuthDTO
            {
                UserName = "Admin",
                Password = "testeLuizaLabs"
            };

            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                    .AddJsonFile("appsettings.json", false)
                    .Build();

            var aplicationServiceAuth = new AplicationServiceAuth(configuration);
            var result = aplicationServiceAuth.Authenticate(auth);

            return result!.Token!;
        }
    }
}
