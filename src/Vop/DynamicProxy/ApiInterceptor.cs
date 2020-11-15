using System.Threading.Tasks;

namespace Vop.Api.DynamicProxy
{
	public abstract class ApiInterceptor : IApiInterceptor
	{
		public abstract void Intercept(IApiMethodInvocation invocation);

		public virtual Task InterceptAsync(IApiMethodInvocation invocation)
		{
			Intercept(invocation);
			return Task.CompletedTask;
		}
	}
}