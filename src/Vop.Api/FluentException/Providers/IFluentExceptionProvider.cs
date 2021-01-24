using Microsoft.AspNetCore.Mvc;
using System;

namespace Vop.Api.FluentException
{
    public interface IFluentExceptionProvider
    {
        object OnException(Exception exception);
    }
}