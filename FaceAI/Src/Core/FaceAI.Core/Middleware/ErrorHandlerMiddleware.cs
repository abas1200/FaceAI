using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FaceAI.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
 


namespace FaceAI.Application.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiVersionReader _apiVersionReader;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IApiVersionReader apiVersionReader
        , ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _apiVersionReader = apiVersionReader;
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string message = "unexpected exception is happened...";
            _logger.LogError(exception, message);
           
            switch (exception)
            {
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    message = "you do not have access to the configuration or application (forbidden area)";
                    break;

                case InvalidCredentialException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                  break;

                case Newtonsoft.Json.JsonSerializationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = exception.Message;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var apiVersion = _apiVersionReader.Read(context.Request);
            if (apiVersion != null && apiVersion.Equals("0.0"))
                await response.WriteAsync(JsonSerializer.Serialize(new { Message = message }));
            else
            {
                var result = JsonSerializer.Serialize(new { IsSuccess = false, FailedMessage = message });
                await response.WriteAsync(result);
            }
        }

    }
}