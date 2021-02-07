using System;
using System.Collections.Generic;
using System.Linq;
using Vop.Api;
using Vop.Api.DependencyInjection;
using Vop.Api.FluentException;
using Vop.Web.Dtos;
using Vop.Web.Models;
using WangSql;
using WangSql.Abstract.Attributes;

namespace Vop.Web.Services
{
    public class Demo1Service : ITransientDependency
    {
        private readonly ISqlMapper _sqlMapper;

        public Demo1Service(ISqlMapper sqlMapper)
        {
            _sqlMapper = sqlMapper;
        }

        /// <summary>
        /// 测试Get
        /// </summary>
        /// <returns></returns>
        public string GetOne1(Demo1GetDto dto)
        {
            throw new ApiException("哈哈哈");
        }

        /// <summary>
        /// 测试Update
        /// </summary>
        /// <returns></returns>
        public string UpdateModel1(Demo1GetDto dto)
        {
            return "1";
        }

        /// <summary>
        /// 测试数据库
        /// </summary>
        /// <returns></returns>
        public IList<StrObjDict> GetDbOne1()
        {
            _sqlMapper.From<UserInfo>().Insert(new UserInfo() { Id = Guid.NewGuid().ToString(), Name = "vop" + DateTime.Now.ToString("yyyyMMddHHmmss") }).SaveChanges();
            return _sqlMapper.Query<StrObjDict>("select * from tb_user", null).ToList();
        }
    }
}
