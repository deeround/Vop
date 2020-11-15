using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vop.Web.Dtos
{
    public class Demo1GetDto
    {
        public string Id { get; set; }

        [MinLength(1)]
        public string Name { get; set; }
    }
}
