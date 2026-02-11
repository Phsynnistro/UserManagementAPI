using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Tenta processar a requisição normalmente
            await _next(context);
        }
        catch (Exception ex)
        {
            // Se der erro, captura aqui
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Formato JSON simples pedido na atividade
        var result = JsonSerializer.Serialize(new { error = "Internal server error." });
        
        // Dica: Em um cenário real, você poderia logar o 'exception.Message' aqui
        return context.Response.WriteAsync(result);
    }
}
