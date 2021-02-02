using System;
using System.Collections.Generic;
using System.Linq;
using Vop.Api;
using Vop.Api.DependencyInjection;
using Vop.Api.FluentException;
using Vop.Web.Dtos;
using WangSql;

namespace Vop.Web.Services
{
    public class Demo1Service : ITransientDependency
    {
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

        [Table(AutoCreate = true)]
        public class tb_user
        {
            [Column]
            public string id { get; set; }
            [Column]
            public string name { get; set; }
        }
        public IList<StrObjDict> GetDbOne1()
        {
            var sqlMapper = new SqlMapper();
            sqlMapper.Migrate().Run();
            sqlMapper.Entity<tb_user>().Insert(new tb_user() { id = Guid.NewGuid().ToString(), name = "vop" + DateTime.Now.ToString("yyyyMMddHHmmss") });
            return sqlMapper.Query<StrObjDict>("select * from tb_user", null).ToList();
        }
    }
}
