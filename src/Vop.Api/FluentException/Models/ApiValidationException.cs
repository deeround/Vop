using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vop.Api.FluentException
{
    public class ApiValidationException : ApiException, IHasValidationErrors
    {
        public override int? Code { get; set; } = 4001;
        public IList<ValidationResult> ValidationErrors { get; private set; } = new List<ValidationResult>();

        public ApiValidationException() : base("入参验证失败") { }
        public ApiValidationException(string message) : base(message) { }
        public ApiValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
