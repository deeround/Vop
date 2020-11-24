using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vop.Api
{
    public interface IHasValidationErrors
    {
        IList<ValidationResult> ValidationErrors { get; }
    }
}