using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class SimpleAuthMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKey = "super-secret-token"; // Token válido para teste

    public SimpleAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Verifica se o header "Authorization" existe e se o token bate
        if (!context.Request.Headers.TryGetValue("Authorization", out var extractedToken) || extractedToken != ApiKey)
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized: Token invalido ou ausente.");
            return; // Interrompe a requisição aqui
        }

        // Se o token for válido, continua
        await _next(context);
    }
}
