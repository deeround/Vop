using System.Collections.Generic;

namespace Vop.Api.Authentication
{
    public interface IJwtTokenHandler
    {
        TokenInfo CreateToken(Dictionary<string, object> payload);
    }
}