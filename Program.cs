using UserManagementAPI.Controllers; 
// Adicione os usings necessários se as classes estiverem em namespaces diferentes

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao container.
builder.Services.AddControllers();

var app = builder.Build();

// --- CONFIGURAÇÃO DO PIPELINE DE MIDDLEWARE (Passo 5) ---

// 1. Error Handling (Primeiro, para capturar erros de tudo que vier depois)
app.UseMiddleware<ErrorHandlingMiddleware>();

// 2. Authentication (Segundo, para proteger a API)
app.UseMiddleware<SimpleAuthMiddleware>();

// 3. Logging (Terceiro, conforme solicitado)
// Nota: Nesta posição, ele só logará requisições que passaram pela autenticação.
app.UseMiddleware<LoggingMiddleware>();

// --- FIM DA CONFIGURAÇÃO DE MIDDLEWARE ---

app.MapControllers();

app.Run();
