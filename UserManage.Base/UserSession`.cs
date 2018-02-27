using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Base
{
    public interface IUserSession
    {
    }

    public class UserSession<T> : IUserSession
    {
        public T UserId { get; set; }
    }
}
