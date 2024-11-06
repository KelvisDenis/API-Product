using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Products.Domain.Exceptions;

namespace Products.Application.Middleware
{
   public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
            
                BadRequestException badRequestEx => (badRequestEx.StatusCode, badRequestEx.Message),
                NotFoundException notFoundEx => (notFoundEx.StatusCode, notFoundEx.Message),
                UnauthorizedException unauthorizedEx => (unauthorizedEx.StatusCode, unauthorizedEx.Message),
                ConflictException conflictEx => (conflictEx.StatusCode, conflictEx.Message),
                InternalServerErrorException internalServerErrorEx => (internalServerErrorEx.StatusCode, internalServerErrorEx.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message
            };

            var responseContent = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(responseContent);
        }
    }
}