using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class DefaultMvcExceptionProvider : IMvcExceptionProvider
    {
        public virtual void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            throw exception;
        }
    }
}