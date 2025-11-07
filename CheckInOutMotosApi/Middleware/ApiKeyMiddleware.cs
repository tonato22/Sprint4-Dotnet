using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CheckInOutMotosApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        private const string APIKEYNAME = "x-api-key";

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Tenta ler o header x-api-key
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key não fornecida.");
                return;
            }

            // Lê a chave configurada no appsettings.json
            var apiKey = _config.GetValue<string>("ApiKey");

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("API Key inválida.");
                return;
            }

            // Continua para o próximo middleware
            await _next(context);
        }
    }
}
