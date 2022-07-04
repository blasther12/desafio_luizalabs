using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO;
using TesteLuizaLabs.Test.Service;
using Xunit;
using Xunit.Priority;

namespace TesteLuizaLabs.Test
{
    public class CustomerProductTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private HttpClient _client;
        private readonly IMemoryCache _cache;
        private readonly string _token;

        public CustomerProductTests(TestingWebAppFactory<Program> factory)
        {
            //Instanciamos o cache e um novo client e recuperamos o token 
            _cache = factory.Services.GetService<IMemoryCache>()!;
            _client = factory.CreateClient();
            _token = Token.GetToken(_cache);
        }

        /// <summary>
        /// Teste para adicionar um produto vazio
        /// </summary>
        /// <returns></returns>
        [Fact, Priority(3)]
        public async Task Test1_AddProductEmpty()
        {
            var customer = new ProductPostDTO
            {
                Id = "",
                CustomerId = 1
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Customerproduct", data);

            var teste = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Teste para adicionar um produto de um cliente que não existe
        /// </summary>
        /// <returns></returns>
        [Fact, Priority(3)]
        public async Task Test2_AddProductCustomerEmpty()
        {
            var customer = new ProductPostDTO
            {
                Id = "1bf0f365-fbdd-4e21-9786-da459d78dd1f",
                CustomerId = 1
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/Customerproduct", data);

            var teste = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Teste da busca de um produto
        /// </summary>
        /// <returns></returns>
        [Fact, Priority(1)]
        public async Task Test3_GetCustomerProduct()
        {
            long customerProductId = 1;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _client.GetAsync($"api/Customerproduct/{customerProductId}");

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Teste da busca de varios produtos
        /// </summary>
        /// <returns></returns>
        [Fact, Priority(2)]
        public async Task Test4_GetCustomersProduct()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _client.GetAsync("api/Customerproduct");

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
