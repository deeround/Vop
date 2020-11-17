using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vop.Web.Dtos;
using Vop.Web.Services;

namespace Vop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("getOne1")]
        public string GetOne1(Demo1GetDto dto)
        {
            return _demo1Service.GetOne1(dto);
        }

        /// <summary>
        /// 测试Update
        /// </summary>
        /// <returns></returns>
        [HttpPost("updateModel1")]
        public string UpdateModel1(Demo1GetDto dto)
        {
            return _demo1Service.UpdateModel1(dto);
        }
    }
}
