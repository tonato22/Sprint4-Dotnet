# 🚀 CheckInOutMotosApi – Sprint 3

## 📌 Descrição do Projeto
Este projeto é uma **API RESTful em .NET 8 (Web API)** desenvolvida para gerenciar o **controle de entrada e saída de motocicletas em pátios**.  

A solução foi criada com foco em **boas práticas REST**, **documentação com Swagger** e **CRUD completo** para as entidades principais.

---

## 🏗️ Justificativa do Domínio
O domínio escolhido reflete a necessidade de **gerenciamento de motos em pátios** de forma organizada e automatizada.  

As entidades principais foram definidas da seguinte forma:
- **Cliente** → Representa o proprietário da moto.  
- **Moto** → Veículo pertencente ao cliente, que precisa ser identificado no sistema.  
- **Pátio** → Local físico onde as motos são registradas e armazenadas.  
- **Movimentação** → Histórico de check-in e check-out das motos nos pátios.  

Essas entidades foram escolhidas porque representam de forma clara os **atores e processos centrais do sistema**, permitindo registrar desde o cadastro do cliente até o fluxo completo de entrada e saída das motos.

---

## ⚙️ Tecnologias Utilizadas
- **.NET 8 Web API**
- **Entity Framework Core + Migrations**
- **Oracle/SQL Server/SQLite** (dependendo da configuração)
- **Swagger/OpenAPI** para documentação
- **LINQ + Paginação** em todos os endpoints

---

## 📂 Estrutura do Projeto


---

## 🔑 Endpoints Principais

### Clientes
- `GET /api/clientes?page=1&pageSize=10` → Listar clientes com paginação  
- `GET /api/clientes/{id}` → Buscar cliente por Id  
- `POST /api/clientes` → Criar cliente  
- `PUT /api/clientes/{id}` → Atualizar cliente  
- `DELETE /api/clientes/{id}` → Excluir cliente  

### Motos
- `GET /api/motos?page=1&pageSize=10` → Listar motos com paginação  
- `GET /api/motos/{id}` → Buscar moto por Id  
- `POST /api/motos` → Criar moto  
- `PUT /api/motos/{id}` → Atualizar moto  
- `DELETE /api/motos/{id}` → Excluir moto  

### Pátios
- `GET /api/patios?page=1&pageSize=10` → Listar pátios com paginação  
- `GET /api/patios/{id}` → Buscar pátio por Id  
- `POST /api/patios` → Criar pátio  
- `PUT /api/patios/{id}` → Atualizar pátio  
- `DELETE /api/patios/{id}` → Excluir pátio  

### Movimentações
- `GET /api/movimentacoes?page=1&pageSize=10` → Listar movimentações com paginação  
- `POST /api/movimentacoes/checkin` → Registrar check-in  
- `POST /api/movimentacoes/checkout` → Registrar check-out  
- `DELETE /api/movimentacoes/{id}` → Excluir movimentação  

---

## 📖 Documentação (Swagger)
A API possui **Swagger/OpenAPI configurado** com:
- Descrição dos endpoints  
- Exemplos de requisição e resposta  
- Modelos de dados  

Basta rodar o projeto e acessar:  
👉 [https://localhost:5001/swagger](https://localhost:5001/swagger)  

---

## 👨‍💻 Autores
- Diogo Paquete Weyne - RM558380
- Gustavo Tonato Maia - RM555393
- João Victor de Souza - RM555290
