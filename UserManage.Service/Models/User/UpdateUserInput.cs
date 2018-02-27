using System.ComponentModel.DataAnnotations;

namespace UserManage.Service.Models
{
    public class UpdateUserInput : AddUpdateUserInputBase
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }

}
