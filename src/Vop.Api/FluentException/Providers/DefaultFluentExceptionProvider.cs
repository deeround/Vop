using Microsoft.AspNetCore.Mvc;
using System;
using Vop.Api.FluentResult;

namespace Vop.Api.FluentException
{
    public class DefaultFluentExceptionProvider : IFluentExceptionProvider
    {
        private readonly IFluentResultProvider _fluentResultProvider;

        public DefaultFluentExceptionProvider(IFluentResultProvider fluentResultProvider)
        {
            _fluentResultProvider = fluentResultProvider;
        }

        public virtual object OnException(Exception exception)
        {
            return _fluentResultProvider.OnException(exception);
        }
    }
}