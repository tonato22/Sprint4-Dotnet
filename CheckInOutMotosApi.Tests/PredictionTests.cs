using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;


namespace CheckInOutMotosApi.Tests
{
    public class PredictionTests
    {
        [Fact]
        public async Task Prediction_DeveRetornarResultadoValido()
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", "12345");


            var sampleData = new { tamanho = 120, quartos = 3, banheiros = 2, idade = 10, distanciaCentro = 5 };


            var response = await client.PostAsJsonAsync("https://localhost:5000/api/v1/prediction/predict", sampleData);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            var content = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(content));
        }
    }
}