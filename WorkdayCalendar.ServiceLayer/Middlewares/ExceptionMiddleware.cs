using Microsoft.AspNetCore.Http;
using WorkdayCalendar.DomainLayer.Exceptions;

namespace WorkdayCalendar.ServiceLayer.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;            
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {   

            // Handle specific domain exceptions
            if (exception is NotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;                
            }
            else if (exception is InvalidInputParameterException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;            }
            

            return context.Response.WriteAsync($"{exception.Message} {context.Response.StatusCode}"); ;
        }
    }
}
