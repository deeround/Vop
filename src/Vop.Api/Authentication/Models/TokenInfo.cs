using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vop.Api.Authentication
{
    public class TokenInfo
    {
        /// <summary>
        /// TOKEN
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效期(秒)
        /// </summary>
        public long? ExpiresIn { get; set; }
        /// <summary>
        /// Bearer
        /// </summary>
        public string TokenType { get; set; }
    }
}
