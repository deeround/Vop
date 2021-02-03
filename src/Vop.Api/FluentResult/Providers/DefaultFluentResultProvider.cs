using Microsoft.Extensions.Options;
using System;
using Vop.Api.FluentException;

namespace Vop.Api.FluentResult
{
    public class DefaultFluentResultProvider : IFluentResultProvider
    {
        private readonly IOptions<CoreOptions> _coreOptions;

        public DefaultFluentResultProvider(IOptions<CoreOptions> coreOptions)
        {
            _coreOptions = coreOptions;
        }

        /// <summary>
        /// 异常返回值
        /// </summary>
        public virtual object OnException(Exception exception)
        {
            ApiException exp = ResolveException(exception);

            Output output = null;
            if (exp is IHasValidationErrors errors)
            {
                output = new OutputWithErrors()
                {
                    Code = 0,
                    Msg = "操作失败",
                    ErrCode = exp.Code,
                    ErrMsg = exp.Message,
                    ValidationErrors = errors.ValidationErrors
                };
            }
            else
            {
                output = new Output()
                {
                    Code = 0,
                    Msg = "操作失败",
                    ErrCode = exp.Code,
                    ErrMsg = exp.Message,
                };
            }
            if (output != null && _coreOptions.Value != null && _coreOptions.Value.ErrStack)
            {
                if (exp.StackException != null)
                {
                    output.ErrStack = exp.StackException.StackTrace;
                }
                else
                {
                    output.ErrStack = exp.StackTrace;
                }
            }
            return output;
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        public virtual object OnSuccessed(object data)
        {
            Output output = new Output()
            {
                Code = 1,
                Msg = "操作成功",
                Data = data
            };
            return output;
        }

        private ApiException ResolveException(Exception exception)
        {
            if (exception is ApiException) return exception as ApiException;
            else if (exception.InnerException != null && exception.InnerException is ApiException) return exception.InnerException as ApiException;
            else if (exception.InnerException != null && exception.InnerException.InnerException != null && exception.InnerException.InnerException is ApiException) return exception.InnerException.InnerException as ApiException;
            else return new ApiException(exception.Message, exception);
        }

    }
}