# 🏍️ CheckInOutMotosApi

## 📘 Descrição do Projeto

A **CheckInOutMotosApi** é uma API desenvolvida em **.NET 8** integrada ao banco **Oracle**, criada como parte da **Sprint 3 - Software Architecture** do curso de **Análise e Desenvolvimento de Sistemas (FIAP)**.

O sistema gerencia **clientes e o check-in/out de motos no pátio da Mottu**, utilizando boas práticas de arquitetura, versionamento de API, autenticação com **API Key** e integração com **ML.NET** para previsão de uso.

---

## 🧱 Estrutura da Solução

```
CheckInOutMotosApi/          → Projeto principal (API)
CheckInOutMotosApi.Tests/    → Projeto de testes automatizados (xUnit)
Sprint-3-main.sln            → Solution principal
```

---

## ⚙️ Tecnologias Utilizadas

- **.NET 8.0**
- **Entity Framework Core** (Oracle)
- **Swagger / OpenAPI**
- **API Versioning**
- **JWT / API Key Middleware**
- **ML.NET** (endpoint de previsão)
- **xUnit + WebApplicationFactory** (testes automatizados)
- **Oracle Database** (FIAP Cloud)

---

## 🧠 Pré-Requisitos

Antes de rodar, instale:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/)
- Oracle Client configurado (para conexão com `oracle.fiap.com.br`)

---

## 🧬 Configuração do Banco

No arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "User Id=rm558380;Password=fiap24;Data Source=oracle.fiap.com.br:1521/ORCL"
  },
  "AllowedHosts": "*"
}
```

> ⚠️ Altere as credenciais de acordo com seu RM e senha FIAP.

---

## 🚀 Como Executar o Projeto

### 🔹 Usando o Visual Studio
1. Abra a solução `Sprint-3-main.sln`
2. Selecione o projeto **CheckInOutMotosApi** como **Startup Project**
3. Pressione **F5** para iniciar a API
4. O navegador abrirá em:
   ```
   https://localhost:5000/swagger
   ```

---

## 🔍 Endpoints Principais

### 🔸 Clientes
| Método | Endpoint | Descrição |
|---------|-----------|-----------|
| GET | `/api/Clientes` | Lista todos os clientes |
| GET | `/api/Clientes/{id}` | Retorna cliente por ID |
| POST | `/api/Clientes` | Cadastra novo cliente |
| PUT | `/api/Clientes/{id}` | Atualiza cliente |
| DELETE | `/api/Clientes/{id}` | Remove cliente |

### 🔸 Health Check
| Método | Endpoint | Descrição |
|---------|-----------|-----------|
| GET | `/api/v1/Health` | Verifica se a API está ativa |
| Header obrigatório | `x-api-key: 12345` |

### 🔸 ML.NET Endpoint
| Método | Endpoint | Descrição |
|---------|-----------|-----------|
| POST | `/api/v1/Prediction` | Faz previsão de uso de motos com ML.NET |

---

## 🤫 Executando os Testes (xUnit)

### 1️⃣ Certifique-se de que a API **não está rodando**
> Os testes utilizam um servidor em memória via `WebApplicationFactory`.

### 2️⃣ No terminal PowerShell:

```bash
cd "C:\Users\User\source\repos"(conforme aonde foi salvo)
dotnet test .\CheckInOutMotosApi.Tests\CheckInOutMotosApi.Tests.csproj
```

### 3️⃣ Resultado esperado:
- Teste `HealthCheck_DeveRetornarStatusOK` → ✅ **OK**
- Teste `Prediction_DeveRetornarResultadoValido` → ✅ **OK**

---

## 🔐 Middleware de Segurança

A API usa um middleware personalizado que exige o header:

```
x-api-key: 12345
```

Caso o header não seja enviado, a resposta será:
```json
{
  "message": "API Key não fornecida."
}
```

---

## 📊 Versionamento

A API utiliza **versionamento via URL**:
```
/api/v1/Clientes
/api/v1/Health
/api/v1/Prediction
```

---

## 🧮 Testes Automatizados (xUnit)

Os testes ficam no projeto `CheckInOutMotosApi.Tests` e utilizam `WebApplicationFactory`:

```csharp
public class HealthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public HealthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task HealthCheck_DeveRetornarStatusOK()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("x-api-key", "12345");

        var response = await client.GetAsync("/api/v1/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
```

---

## 📚 Contribuidores

👨‍💻 **Diogo Weyne** – RM558380  
👨‍💻 **Gustavo Tonato Maia**   - RM555393
👨‍💻 **João Victor de Souza** -RM555290

---

## 🏁 Resultado Esperado

✅ API documentada no Swagger  
✅ Endpoints versionados  
✅ Middleware de autenticação com API Key  
✅ Endpoint ML.NET funcional  
✅ Testes xUnit executando com sucesso  

---


