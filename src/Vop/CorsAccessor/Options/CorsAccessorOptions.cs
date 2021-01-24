using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Vop.Api.CorsAccessor
{
    public class CorsAccessorOptions : CorsPolicy
    {
        public string Name { get; set; }
    }
}