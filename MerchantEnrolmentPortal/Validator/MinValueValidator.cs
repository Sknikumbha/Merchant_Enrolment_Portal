using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Validator
{
    public class MinValueValidator : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int val = (int)value;
                if (val < 10)
                {
                    return new ValidationResult("Minimum number of outlets should be 10");
                }
            }
            return ValidationResult.Success;
        }
    }
}