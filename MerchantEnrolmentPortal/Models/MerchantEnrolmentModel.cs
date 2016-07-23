using MerchantEnrolmentPortal.Common.DataTransferObjects;
using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal.Models
{
    public class MerchantEnrolmentModel : IValidatableObject
    {
        public int? MerchantId { get; set; }
        [NameValidator]
        [Required(ErrorMessage = "Merchant Name is requirde")]
        public string MerchantName { get; set; }
        [Required(ErrorMessage = "Country is requirde")]
        public string Country { get; set; }
        //[DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n} €")]
        [Required(ErrorMessage = "Merchant profit is requirde")]
        public decimal MerchantProfit { get; set; }
        [MinValueValidator]
        [Required(ErrorMessage = "Number of outlets is requirde")]
        public int? NumberOfOutlets { get; set; }
        [Required(ErrorMessage = "Added by name is required")]
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "please insert the date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTime tempDate = DateTime.MinValue;
            //DateTime startDate;
            //DateTime endDate;

            if (CreatedOn != null)
            {
                tempDate = CreatedOn;
                var enrolmentRules = SessionStateFacade.GetSessionList<CodesetModel>(SessionName.CodeSetList);
                if (enrolmentRules.Any(x => x.Country.Equals(Country)))
                {
                    var enrolmentRule = enrolmentRules.Where(x => x.Country.Equals(Country)).FirstOrDefault();
                    //startDate = enrolmentRule.CodeListItem.FromToValue.FirstOrDefault().Key;
                    //endDate = enrolmentRule.CodeListItem.FromToValue.FirstOrDefault().Value;
                    if (tempDate.Ticks > enrolmentRule.StartDate.Ticks && tempDate.Ticks < enrolmentRule.EndDate.Ticks)
                        yield return new ValidationResult("Enrolment is not allowed in " + Country + " in this period");
                }
                //switch (Country)
                //{

                //case "France":
                //    {
                //        startDate = Convert.ToDateTime("10/01/2016");
                //        endDate = Convert.ToDateTime("24/02/2016");
                //        if (tempDate.Ticks > startDate.Ticks && tempDate.Ticks < endDate.Ticks)
                //            yield return new ValidationResult("Enrolment is not allowed in" + Country + "in this period");
                //    }
                //    break;
                //case "Spain":
                //    {
                //        startDate = Convert.ToDateTime("15/02/2016");
                //        endDate = Convert.ToDateTime("01/04/2016");
                //        if (tempDate.Ticks > startDate.Ticks && tempDate.Ticks < endDate.Ticks)
                //            yield return new ValidationResult("Enrolment is not allowed in" + Country + "in this period");
                //    }
                //    break;
                //}
            }
        }
    }
}