using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WangSql.Abstract.Attributes;

namespace Vop.Web.Models
{
    [Table(TableName = "tb_user")]
    public class UserInfo
    {
        [Column(ColumnName ="id")]
        public string Id { get; set; }
        [Column(ColumnName ="name")]
        public string Name { get; set; }
    }
}
