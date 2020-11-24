using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Vop.Api.Mvc
{
    public interface IMvcExceptionProvider
    {
        void OnException(ExceptionContext context);
    }
}