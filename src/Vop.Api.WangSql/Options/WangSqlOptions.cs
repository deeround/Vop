using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vop.Api.WangSql
{
    public class WangSqlOptions
    {
        public string ConnectionString { get; set; }
        public string ConnectionType { get; set; }
        public string Name { get; set; }
        public string ParameterPrefix { get; set; }
        public bool UseParameterPrefixInParameter { get; set; }
        public bool UseParameterPrefixInSql { get; set; }
        public bool UseQuotationInSql { get; set; }
        public bool Debug { get; set; }
    }
}
