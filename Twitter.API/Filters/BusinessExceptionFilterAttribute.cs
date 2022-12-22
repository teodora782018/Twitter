﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Twitter.API.ActionFilters
{
    public class BusinessExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly Type _exceptionType;
        private readonly HttpStatusCode _statusCode;

        public BusinessExceptionFilterAttribute(Type exceptionType, HttpStatusCode httpStatusCode)
        {
            _exceptionType = exceptionType ?? throw new ArgumentNullException(nameof(exceptionType));
            _statusCode = httpStatusCode;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception.GetType() == _exceptionType)
            {
                context.HttpContext.Response.StatusCode = (int)_statusCode;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(exception.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}
