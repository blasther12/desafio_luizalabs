using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.Service
{
    public class Http : IHttp
    {
        private readonly HttpClient _client = new() { Timeout = TimeSpan.FromSeconds(1) };

        /// <summary>
        /// Método reponsavél por buscar dados de webservices
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="url">endpoint da api</param>
        /// <returns></returns>
        public async Task<T?> Get<T>(string url)
        {
            try
            {
                var result = await _client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return System.Text.Json.JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());
                }
                else
                {
                    return default;
                }

            }catch (Exception)
            {
                throw;
            }
        }
    }
}
