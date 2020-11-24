using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vop.Web.Dtos
{
    public class Demo1GetDto
    {
        [Required]
        public string Id { get; set; }

        [MinLength(2)]
        public string Name { get; set; }
    }
}
