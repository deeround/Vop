using System;

namespace Vop.Api.DynamicApiController
{
    /// <summary>
    /// 接口参数位置设置
    /// </summary>
    public class ApiSeatAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="seat"></param>
        public ApiSeatAttribute(ApiSeats seat = ApiSeats.ActionEnd)
        {
            Seat = seat;
        }

        /// <summary>
        /// 参数位置
        /// </summary>
        public ApiSeats Seat { get; set; }
    }
}