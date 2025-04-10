using FluentValidation;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BTG.Vacinacao.CrossCutting.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                if (ex is ValidationException validationEx)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    var errors = validationEx.Errors
                        .GroupBy(e => e.PropertyName)
                        .Select(g => new
                        {
                            field = g.Key,
                            messages = g.Select(e => e.ErrorMessage).ToList()
                        });

                    var validationResponse = new
                    {
                        status = context.Response.StatusCode,
                        errors
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
                    return;
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new
                {
                    status = context.Response.StatusCode,
                    message = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
