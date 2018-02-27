using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Base
{
    public interface IAceSession
    {
    }

    public class AceSession<T> : IAceSession
    {
        public T UserId { get; set; }
    }
}
