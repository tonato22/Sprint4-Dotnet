using Microsoft.EntityFrameworkCore;
using CheckInOutMotosApi.Data;
using CheckInOutMotosApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Conexão com Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CheckInOutMotosApi",
        Version = "v1",
        Description = "API de Gestão de Clientes e Health Checks - com segurança via API Key"
    });

    // 🔐 Configuração da segurança via API Key
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Chave de API via cabeçalho (x-api-key). Exemplo: 12345",
        Name = "x-api-key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "x-api-key",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


// ✅ Versionamento da API
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Criação do app
var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Segurança e middlewares
app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Middleware de API Key (verifica header x-api-key)
app.UseMiddleware<ApiKeyMiddleware>();

// ✅ Mapeia Controllers
app.MapControllers();

// Inicia a aplicação
app.Run();

// Necessário para os testes de integração xUnit
public partial class Program { }
