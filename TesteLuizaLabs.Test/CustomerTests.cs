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

namespace TesteLuizaLabs.Test 
{
    public class CustomerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private HttpClient _client;
        private readonly IMemoryCache _cache;
        private readonly string _token;

        public CustomerTests(TestingWebAppFactory<Program> factory)
        {
            ////Instanciamos o cache e um novo client e recuperamos o token 
            _cache = factory.Services.GetService<IMemoryCache>()!;
            _client = factory.CreateClient();
            _token = Token.GetToken(_cache);
        }

        /// <summary>
        /// Testa buscar um registro sem autenticação
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test1_UnauthenticatedAsync()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(new CustomerDTO { Name = "teste nome", Email = "email_teste" });

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/customer", data);



            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        /// <summary>
        /// Testa o cadastro com e-mail invalido
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test2_EmailInvalid()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(new CustomerDTO { Name = "teste nome", Email = "email_teste" });

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/customer", data);
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.Contains("Formato de E-mail invalido!", result);
        }

        /// <summary>
        /// Testa adicionar um novo cliente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test3_AddCustomer()
        {
            var customer = new CustomerDTO
            {
                Name = "teste nome",
                Email = "email_teste@gmail.com"
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/customer", data);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Testa adicionar um cliente duplicado
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test4_AddDuplicateCustomer()
        {
            var customer = new CustomerDTO
            {
                Name = "teste nome",
                Email = "email_teste2@outlook.com"
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/customer", data);

            var teste = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        /// Testa buscar um cliente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test5_GetCustomer()
        {
            long customerId = 1;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _client.GetAsync($"api/customer/{customerId}");

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Testa buscar varios clientes
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test6_GetCustomers()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await _client.GetAsync("api/customer");

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Testa a atualização de um cliente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test7_PutCustomers()
        {
            var customer = new CustomerDTO
            {
                Id = 1,
                Name = "teste nome",
                Email = "email_teste@outlook.com"
            };

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var json = System.Text.Json.JsonSerializer.Serialize(customer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("api/customer", data);

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }

        /// <summary>
        /// Testa a remoção de um cliente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test8_DeleteCustomers()
        {
            long customerId = 2;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await _client.DeleteAsync($"api/customer/{customerId}");

            var teste = await response.Content.ReadAsStringAsync();

            Assert.NotNull(response);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}