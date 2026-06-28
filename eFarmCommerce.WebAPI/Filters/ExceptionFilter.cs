using eFarmCommerce.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eFarmCommerce.WebAPI.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ClientException clientException)
        {
            context.ModelState.AddModelError("Error", clientException.Message);

            context.Result = new BadRequestObjectResult(new
            {
                statusCode = clientException.StatusCode,
                message = clientException.Message
            });

            context.ExceptionHandled = true;
            return;
        }

        context.Result = new ObjectResult(new
        {
            statusCode = 500,
            message = "Greška na serveru.",
            detail = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}