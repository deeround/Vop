using System;

namespace Vop.Api.FluentException
{
    public class ExceptionAttribute : Attribute
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

    }
}