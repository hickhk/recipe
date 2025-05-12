using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecipeBook.Comunication.Responses;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.ExceptionCustom;
using System.Net;

namespace RecipeBook.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is RecipeBookException)
            {
                HandleException(context);
            }
            else
            {
                TrowUnknowException(context);
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
            }
        }

        private void TrowUnknowException(ExceptionContext context)
        {
            bool isDebug = false;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = isDebug ? context.Result : new ObjectResult(new ResponseErrorJson(ResoourcesMessagesException.UNKNOW_ERROR));
        }
    }
}
