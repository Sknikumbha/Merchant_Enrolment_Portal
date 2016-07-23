using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Validator
{
    public class NameValidator : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                //string merchandName = value.ToString();
                var merchantEnrolmentList = SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);
                var isDuplicate = merchantEnrolmentList.Any(x => (!string.IsNullOrEmpty(x.MerchantName) && x.MerchantName.Trim().Equals(value.ToString().Trim())));
                if (!isDuplicate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    //int count = merchantEnrolmentList.Where(x => x.MerchantName.Trim().Equals(value.ToString().Trim())).ToList().Count;
                    //if (count <= 1)
                    //    return ValidationResult.Success;
                    //else
                    //    return new ValidationResult("Merchand name already exists");
                    return new ValidationResult("Merchand name already exists");
                }
            }
            else
                return ValidationResult.Success;
            //return base.IsValid(value, validationContext);
        }
    }
}