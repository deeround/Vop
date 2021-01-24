using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Vop.Api.Authentication
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly IOptions<JwtSettingsOptions> _options;

        public JwtTokenHandler(IOptions<JwtSettingsOptions> options)
        {
            _options = options;
        }

        public virtual TokenInfo CreateToken(Dictionary<string, object> payload)
        {
            var options = _options.Value;
            var claims = new List<Claim>();
            if (payload != null)
            {
                foreach (var item in payload)
                {
                    claims.Add(new Claim(item.Key, item.Value?.ToString()));
                }
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.IssuerSigningKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(options.ValidIssuer, options.ValidAudience, claims, null, DateTime.Now.AddSeconds((double)options.ExpiredTime), credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new TokenInfo()
            {
                AccessToken = token,
                TokenType = "Bearer",
                ExpiresIn = options.ExpiredTime
            };
        }
    }
}
