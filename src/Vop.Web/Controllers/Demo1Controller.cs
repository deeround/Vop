using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vop.Api;
using Vop.Api.FluentException;
using Vop.Web.Dtos;
using Vop.Web.Services;

namespace Vop.Web.Controllers
{
    [Route("")]
    [ApiController]
    [Exception(Code = 1002, Message = "demo异常1002")]
    public class Demo1Controller : ControllerBase
    {
        private readonly Demo1Service _demo1Service;

        public Demo1Controller(Demo1Service demo1Service)
        {
            _demo1Service = demo1Service;
        }

        /// <summary>
        /// 测试Get
        /// </summary>
        /// <returns></returns>
        public string GetOne1(Demo1GetDto dto)
        {
            return _demo1Service.GetOne1(dto);
        }

        /// <summary>
        /// 测试Get
        /// </summary>
        /// <returns></returns>
        //[ErrorCode(ErrCode = 10021, ErrMsg = "demo异常10021")]
        public string GetOne2(string id1, string id2)
        {
            throw new Exception("aaa");
        }

        /// <summary>
        /// 测试Update
        /// </summary>
        /// <returns></returns>
        public string UpdateModel1(Demo1GetDto dto)
        {
            return _demo1Service.UpdateModel1(dto);
        }

        public IList<StrObjDict> GetDbOne1()
        {
            return _demo1Service.GetDbOne1();
        }
    }
}
