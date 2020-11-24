using System;

namespace Vop.Api.FluentException
{
    public abstract class ApiExceptionBase : Exception
    {
        public virtual int? Code { get; set; } = 4000;
        public virtual string Msg { get; set; } = "系统错误";

        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class.
        public ApiExceptionBase() : base() { }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message.
        //
        // 参数:
        //   message:
        //     The message that describes the error.
        public ApiExceptionBase(string message) : base(message) { }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message and a reference to the inner exception that is the cause of this exception.
        //
        // 参数:
        //   message:
        //     The error message that explains the reason for the exception.
        //
        //   innerException:
        //     The exception that is the cause of the current exception, or a null reference
        //     (Nothing in Visual Basic) if no inner exception is specified.
        public ApiExceptionBase(string message, Exception innerException) : base(message, innerException) { }
    }
}
