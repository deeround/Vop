using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Vop.Api.Mvc
{
    public interface IMvcResultProvider
    {
        void OnSuccessed(ActionExecutedContext context);
    }
}