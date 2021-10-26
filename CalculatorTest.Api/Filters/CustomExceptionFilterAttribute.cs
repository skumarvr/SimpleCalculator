using CalculatorTest.Api.Utils;
using CalculatorTest.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;
using System.Net.Http;

namespace CalculatorTest.Api.Controllers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            ApiError apiError;
            if (context.Exception is CalculatorException)
            {
                var ex = context.Exception as CalculatorException;
                context.Exception = null;
                apiError = new ApiError(ex.Message);
                apiError.StackTrace = ex.StackTrace;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                var msg = "An unhandled error occurred.";                
                apiError = new ApiError(msg);
                apiError.StackTrace = context.Exception.StackTrace;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Result = new JsonResult(apiError);
            base.OnException(context);
        }
    }
}
