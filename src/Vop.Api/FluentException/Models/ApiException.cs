using System;

namespace Vop.Api.FluentException
{
    public class ApiException : BaseException
    {
        public override int? Code { get; set; } = 4000;
        public Exception StackException { get; set; }
        public ApiException() : base("系统错误") { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception innerException) : base(message, innerException) { StackException = innerException; }
        public ApiException(int? code, string message) : base(message) { this.Code = code; }
        public ApiException(int? code, string message, Exception innerException) : base(message, innerException) { this.Code = code; StackException = innerException; }
    }














    public abstract class BaseException : Exception
    {
        public virtual int? Code { get; set; } = 4000;

        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class.
        public BaseException() : base("系统错误") { }
        //
        // 摘要:
        //     Initializes a new instance of the System.Exception class with a specified error
        //     message.
        //
        // 参数:
        //   message:
        //     The message that describes the error.
        public BaseException(string message) : base(message) { }
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
        public BaseException(string message, Exception innerException) : base(message, innerException) { }
    }

}
