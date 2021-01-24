using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Vop.Api.FluentException;

namespace Vop.Api.Mvc
{
    public class DefaultMvcExceptionProvider : IMvcExceptionProvider
    {
        public virtual void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is ApiException) throw exception as ApiException;
            else if (exception.InnerException != null && exception.InnerException is ApiException) throw exception.InnerException as ApiException;
            else if (exception.InnerException != null && exception.InnerException.InnerException != null && exception.InnerException.InnerException is ApiException) throw exception.InnerException.InnerException as ApiException;
            else
            {
                //查询action上是否标记的错误码
                var action = (context.ActionDescriptor as ControllerActionDescriptor);
                var errorCodeAttribute = action.MethodInfo.GetCustomAttributes(typeof(ErrorCodeAttribute), false).FirstOrDefault() as ErrorCodeAttribute;
                if (errorCodeAttribute != null)
                {
                    throw new ApiException(errorCodeAttribute.ErrCode, errorCodeAttribute.ErrMsg, exception);
                }
                //查询controller上是否标记的错误码
                errorCodeAttribute = action.ControllerTypeInfo.GetCustomAttributes(typeof(ErrorCodeAttribute), false).FirstOrDefault() as ErrorCodeAttribute;
                if (errorCodeAttribute != null)
                {
                    throw new ApiException(errorCodeAttribute.ErrCode, errorCodeAttribute.ErrMsg, exception);
                }

                //如果都没有
                throw new ApiUnknowException(exception.Message, exception);
            }

            throw exception;
        }
    }
}