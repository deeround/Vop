using System;

namespace Vop.Api.FluentException
{
    public class ApiValidationException : ApiException
    {
        public override int? Code { get; set; } = 4001;
        public ApiValidationException() : base() { }
        public ApiValidationException(string message) : base(message) { }
        public ApiValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
