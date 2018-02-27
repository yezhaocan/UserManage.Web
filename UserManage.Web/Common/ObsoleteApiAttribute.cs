using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Web.Common
{
    public class ObsoleteApiAttribute : Attribute, IFilterMetadata
    {
        public ObsoleteApiAttribute()
        {
        }
        public ObsoleteApiAttribute(string message)
        {
            this.Message = message;
        }

        public string Message { get; private set; }
    }
}
