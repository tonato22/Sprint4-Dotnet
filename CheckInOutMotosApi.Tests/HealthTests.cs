/* HealthTests.cs */
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;


namespace CheckInOutMotosApi.Tests
{
    public class HealthTests
    {
        [Fact]
        public async Task HealthCheck_DeveRetornarStatusOK()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", "12345");


            var response = await client.GetAsync("https://localhost:5000/api/v1/health");


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}