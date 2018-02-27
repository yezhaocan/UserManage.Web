using System.Reflection;

namespace UserManage.Service
{
    public static class CurrentAssembly
    {
        public static Assembly Value
        {
            get
            {
                Assembly assembly = typeof(CurrentAssembly).GetTypeInfo().Assembly;
                return assembly;
            }
        }
    }
}
