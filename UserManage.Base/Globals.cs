using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManage.Base
{
    public static class Globals
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public static IConfigurationRoot Configuration { get; set; }
    }
}
