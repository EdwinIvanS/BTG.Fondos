using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BTG.Fondos.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace BTG.Fondos.Common
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error no controlado: {Mensaje}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            string mensaje;

            switch (exception)
            {
                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    mensaje = exception.Message;
                    break;

                case BusinessValidationException:
                    statusCode = HttpStatusCode.BadRequest;
                    mensaje = exception.Message;
                    break;

                case InvalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    mensaje = exception.Message;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    mensaje = "Ocurrió un error inesperado en el servidor.";
                    break;
            }

            var response = new
            {
                codigo = (int)statusCode,
                mensaje,
                fecha = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
