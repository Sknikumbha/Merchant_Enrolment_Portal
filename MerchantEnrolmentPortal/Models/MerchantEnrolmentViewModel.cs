using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Models
{
    public class MerchantEnrolmentViewModel
    {
        public MerchantEnrolmentViewModel()
        {
            MerchantEnrolmentList = new List<MerchantEnrolmentModel>();
            MerchantEnrolmentModel = new MerchantEnrolmentModel();
        }
        public List<MerchantEnrolmentModel> MerchantEnrolmentList { get; set; }

        public MerchantEnrolmentModel MerchantEnrolmentModel { get; set; }
    }
}