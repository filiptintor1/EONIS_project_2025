
using WebShop.Domain.Exceptions;
using WebShopProject.Domain.Exceptions;

namespace WebShop.API.ErrorHandling
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);

            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);
                logger.LogWarning(notFound.Message);

            }
            catch (UserAlreadyExistsException userAlreadyExists)
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict; 
                await context.Response.WriteAsync(userAlreadyExists.Message);
                logger.LogWarning(userAlreadyExists.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred.");
            }
        }
    }
}
