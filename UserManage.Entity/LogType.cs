using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Entity
{
    public enum LogType
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        Operation=1,
        /// <summary>
        /// 错误日志
        /// </summary>
        Error=2,
        /// <summary>
        /// 错误日志
        /// </summary>
        System = 3,
    }
}
