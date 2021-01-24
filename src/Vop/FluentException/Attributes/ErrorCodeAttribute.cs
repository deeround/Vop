using System;

namespace Vop.Api.FluentException
{
    public class ErrorCodeAttribute : Attribute
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg { get; set; }

    }
}