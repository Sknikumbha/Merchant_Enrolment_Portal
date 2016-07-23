using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Helper;
using MerchantEnrolmentPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantEnrolmentPortal.Binder
{
    public static class GridBinder
    {
        public static IEnumerable<MerchantEnrolmentModel> BindMerchnatEnrolmentGrid(string merchantName, string country)
        {
            string keyValue = KeysHelper.ApiUrl.GetMerchnatEnrolmentDetails;
            var queryValues = new List<Parameters>
            {
                new Parameters{Key="merchantName", Value=merchantName},
                new Parameters{Key="country", Value=country},
                new Parameters{Key="userId", Value=!string.IsNullOrEmpty(SessionStateFacade.GetSession(SessionName.AKAName)) ? SessionStateFacade.GetSession(SessionName.AKAName) : "Admin"} 
                //Get value of logged in user from session           
            };

            var response = ApiClientHelper.GetHttpClient(keyValue, queryValues);
            if (response == null || response.Content == null) return new List<MerchantEnrolmentModel>();
            var merchantEnrolmentList = (List<MerchantEnrolmentModel>)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(List<MerchantEnrolmentModel>));
            if (merchantEnrolmentList != null && merchantEnrolmentList.Count > 0)
                merchantEnrolmentList = merchantEnrolmentList.OrderByDescending(x => x.MerchantId).ToList();
            SessionStateFacade.SetSessionList(SessionName.EnrollmentList, merchantEnrolmentList);
            return merchantEnrolmentList;

        }

        public static List<MerchantEnrolmentModel> SaveMerchantEnrolmentDetails(MerchantEnrolmentModel merchantEnrolmentModel)
        {
            string keyValue = KeysHelper.ApiUrl.CreateMerchantEnrolmentDetails;
            //var merchantEnrolList = SessionStateFacade.GetSessionList<MerchantEnrolmentModel>(SessionName.EnrollmentList);

            //if (merchantEnrolList.Any(x => ((!string.IsNullOrEmpty(x.MerchantName) && x.MerchantName.Equals(merchantEnrolmentModel.MerchantName)))))
            //    return merchantEnrolList;
            var response = ApiClientHelper.PostHttpClient(keyValue, merchantEnrolmentModel);
            if (response == null || response.Content == null) return new List<MerchantEnrolmentModel>();
            var merchantEnrolmentList = (List<MerchantEnrolmentModel>)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(List<MerchantEnrolmentModel>));
            SessionStateFacade.SetSessionList(SessionName.EnrollmentList, merchantEnrolmentList);
            return merchantEnrolmentList;
        }

        public static List<MerchantEnrolmentModel> RemoveMerchantEnrolmentDetails(int merchantId)
        {
            string keyValue = KeysHelper.ApiUrl.RemoveMerchantEnrolmentDetails;
            var queryValues = new List<Parameters>
            {
                new Parameters{Key="merchantId", Value1=merchantId}
            };

            var response = ApiClientHelper.DeleteHttpClient(keyValue, queryValues);
            if (response == null || response.Content == null) return new List<MerchantEnrolmentModel>();
            var merchantEnrolmentList = (List<MerchantEnrolmentModel>)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result, typeof(List<MerchantEnrolmentModel>));
            SessionStateFacade.SetSessionList(SessionName.EnrollmentList, merchantEnrolmentList);
            return merchantEnrolmentList;
        }
    }
}