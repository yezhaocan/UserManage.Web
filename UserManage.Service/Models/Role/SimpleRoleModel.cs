using UserManage.Entity.System;

namespace UserManage.Service.Models
{
    public class SimpleRoleModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static SimpleRoleModel Create(Sys_Role role)
        {
            SimpleRoleModel roleModel = new SimpleRoleModel();

            roleModel.Id = role.Id;
            roleModel.Name = role.Name;

            return roleModel;
        }
    }

}
