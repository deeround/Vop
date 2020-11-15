using System;

namespace Vop.Api.FluentException
{
    public class ApiUnknowException : ApiException
    {
        public override int? Code { get; set; } = -1;
        public ApiUnknowException() : base() { }
        public ApiUnknowException(string message) : base(message) { }
        public ApiUnknowException(string message, Exception innerException) : base(message, innerException) { }
    }
}
