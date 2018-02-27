using System.Threading.Tasks;
using UserManage.Base.Application;
using UserManage.Entity;

namespace UserManage.Service.Interfaces
{
    public interface ISysLogAppService : IAppService
    {
        void Log(string description, LogType logType=LogType.Operation);
        void Log(string userId, string realName, string ip, string description, LogType logType=LogType.Operation);
        
    }
}
