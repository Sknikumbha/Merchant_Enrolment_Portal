using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Common.DataTransferObjects
{
    public class CodesetDto
    {
        public int RuleId { get; set; }
        public string Country { get; set; }
        //public CodeListItem CodeListItem { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }




}