using System;

namespace Vop.Api.FluentResult
{
    /// <summary>
    /// 规范化结果提供器
    /// </summary>
    public interface IFluentResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        object OnException(Exception exception);

        /// <summary>
        /// 成功返回值
        /// </summary>
        object OnSuccessed(object data);
    }
}