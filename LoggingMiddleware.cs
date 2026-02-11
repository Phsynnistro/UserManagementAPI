using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log da Requisição
        Console.WriteLine($"[LOG] Request: {context.Request.Method} {context.Request.Path}");

        // Passa para o próximo middleware
        await _next(context);

        // Log da Resposta (ocorre na volta do pipeline)
        Console.WriteLine($"[LOG] Response Status: {context.Response.StatusCode}");
    }
}
