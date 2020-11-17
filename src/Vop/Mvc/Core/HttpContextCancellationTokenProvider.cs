using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Vop.Api.Mvc
{
    public class HttpContextCancellationTokenProvider : ICancellationTokenProvider
    {
        public CancellationToken Token => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextCancellationTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
