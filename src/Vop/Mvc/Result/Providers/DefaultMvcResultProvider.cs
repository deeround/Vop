using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;

namespace Vop.Api.Mvc
{
    public class DefaultMvcResultProvider : IMvcResultProvider
    {
        private readonly IFluentResultProvider _fluentResultProvider;

        public DefaultMvcResultProvider(IFluentResultProvider fluentResultProvider)
        {
            _fluentResultProvider = fluentResultProvider;
        }

        public virtual void OnSuccessed(ActionExecutedContext context)
        {
            object data;
            if (context.Result is ContentResult contentResult) data = contentResult.Content;
            else if (context.Result is ObjectResult objectResult) data = objectResult.Value;
            else data = null;
            context.Result = _fluentResultProvider.OnSuccessed(data);
        }
    }
}