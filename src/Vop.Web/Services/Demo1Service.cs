using Vop.Api.DependencyInjection;
using Vop.Api.FluentException;
using Vop.Web.Dtos;

namespace Vop.Web.Services
{
    [Interceptor(typeof(LogDispatchProxy))]
    public class Demo1Service : ITransientDependency
    {
        /// <summary>
        /// 测试Get
        /// </summary>
        /// <returns></returns>
        public string GetOne1(Demo1GetDto dto)
        {
            throw new ApiException("哈哈哈");
            return "1";
        }

        /// <summary>
        /// 测试Update
        /// </summary>
        /// <returns></returns>
        public string UpdateModel1(Demo1GetDto dto)
        {
            return "1";
        }
    }
}
