﻿using AutoMapper;
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
    public class Demo1GetModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
    public class Demo1GetMap : Profile
    {
        public Demo1GetMap()
        {
            CreateMap<Demo1GetDto, Demo1GetModel>();
        }
    }
}
