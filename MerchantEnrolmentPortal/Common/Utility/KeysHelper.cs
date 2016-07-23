using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Common.Utility
{
    public static class KeysHelper
    {
        public static class Constants
        {
            public const string WebApiUrl = "WebAPIURL";
            public const string ApplicationJson = "application/json";
        }

        public static class ApiUrl
        {
            public const string GetMerchnatEnrolmentDetails = "GetMerchantEnrolment";
            public const string CreateMerchantEnrolmentDetails = "SaveMerchantEnrolment";
            public const string RemoveMerchantEnrolmentDetails = "DeleteMerchantEnrolment";
        }
    }
}