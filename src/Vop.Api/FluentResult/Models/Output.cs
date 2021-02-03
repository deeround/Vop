using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vop.Api.FluentResult
{
    public class Output
    {
        /// <summary>
        /// 
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ErrCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrStack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Data { get; set; }
    }
    public class OutputWithErrors : Output, IHasValidationErrors
    {
        public IList<ValidationResult> ValidationErrors { get; set; }
    }
}