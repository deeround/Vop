using System.Threading.Tasks;

namespace Vop.Api.DynamicProxy
{
	public interface IApiInterceptor
    {
        void Intercept(IApiMethodInvocation invocation);

        Task InterceptAsync(IApiMethodInvocation invocation);
	}
}
