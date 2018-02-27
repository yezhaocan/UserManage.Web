using UserManage.Base.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManage.Base.Application
{
    public abstract class ValidationModel
    {
        public virtual void Validate()
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext vc = new ValidationContext(this, null, null);
            bool isValid = Validator.TryValidateObject
                    (this, vc, validationResults, true);
            if (isValid == false)
            {
                throw new Exceptions.InvalidDataException(validationResults[0].ErrorMessage);
            }
        }
    }
}
