using FluentValidation;
using MerchantEnrolmentPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Validator
{
    public class CustomValidator : AbstractValidator<MerchantEnrolmentModel>
    {
        public CustomValidator()
        {
            //RuleFor(m => m.MerchantName)IsValid();
        }

        private object IsValid()
        {
            throw new NotImplementedException();
        }
    }
}