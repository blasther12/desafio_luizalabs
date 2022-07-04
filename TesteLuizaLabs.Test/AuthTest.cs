using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TesteLuizaLabs.Application.DTO.DTO.Auth;
using TesteLuizaLabs.Test.Service;
using Xunit;

namespace TesteLuizaLabs.Test
{

    public class AuthTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private HttpClient _client;

        public AuthTest(TestingWebAppFactory<Program> factory)
        {
            //Cliamos uma instancia do HttpClient
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Realizamos o teste de login para utilização do serviço
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test1_Login()
        {
            var auth = new AuthDTO
            {
                UserName = "Admin",
                Password = "testeLuizaLabs"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(auth);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/auth", data);

            response.EnsureSuccessStatusCode();
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
        }
    }
}
