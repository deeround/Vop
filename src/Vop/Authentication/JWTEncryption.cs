using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Vop.Api.Authentication
{
    /// <summary>
    /// JWT 加解密
    /// </summary>
    public class JWTEncryption
    {
        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="issuerSigningKey"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static string Encrypt(string issuerSigningKey, Dictionary<string, object> payload)
        {
            return Encrypt(issuerSigningKey, JsonSerializer.Serialize(payload));
        }

        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="issuerSigningKey"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        public static string Encrypt(string issuerSigningKey, string payload)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JsonWebTokenHandler();
            return tokenHandler.CreateToken(payload, credentials);
        }
    }
}