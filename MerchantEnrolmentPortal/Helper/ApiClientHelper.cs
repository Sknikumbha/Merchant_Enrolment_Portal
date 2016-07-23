using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using MerchantEnrolmentPortal.Common.Utility;
namespace MerchantEnrolmentPortal.Helper
{
    public static class ApiClientHelper
    {
        public static HttpResponseMessage PostHttpClient<T>(string keyValue, T postObj)
        {
            keyValue = System.Configuration.ConfigurationManager.AppSettings[KeysHelper.Constants.WebApiUrl] + keyValue;

            var merchantEnrolmentClient = new HttpClient { BaseAddress = new Uri(keyValue) };
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Clear();
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(KeysHelper.Constants.ApplicationJson));
            var response = merchantEnrolmentClient.PostAsJsonAsync(keyValue, postObj).Result;

            if (response.IsSuccessStatusCode) return response;
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new ArgumentException("Bad Request" + response.Content);

            return new HttpResponseMessage();
        }

        public static HttpResponseMessage GetHttpClient(string keyValue, IEnumerable<Parameters> queryParams)
        {
            keyValue = System.Configuration.ConfigurationManager.AppSettings[KeysHelper.Constants.WebApiUrl] + keyValue;

            var merchantEnrolmentClient = new HttpClient { BaseAddress = new Uri(keyValue) };
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Clear();
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(KeysHelper.Constants.ApplicationJson));
            var queryString = queryParams.Aggregate(string.Empty, (current, param) => string.IsNullOrEmpty(current)
                ? "?" + param.Key + "=" + param.Value : current + "&" + param.Key + "=" + param.Value);

            var response = merchantEnrolmentClient.GetAsync(queryString).Result;

            if (response.IsSuccessStatusCode) return response;
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new ArgumentException("Bad Request" + response.Content);


            return new HttpResponseMessage();

        }

        public static HttpResponseMessage DeleteHttpClient(string keyValue, IEnumerable<Parameters> queryParams)
        {
            keyValue = System.Configuration.ConfigurationManager.AppSettings[KeysHelper.Constants.WebApiUrl] + keyValue;

            var merchantEnrolmentClient = new HttpClient { BaseAddress = new Uri(keyValue) };
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Clear();
            merchantEnrolmentClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(KeysHelper.Constants.ApplicationJson));
            var queryString = queryParams.Aggregate(string.Empty, (current, param) => string.IsNullOrEmpty(current)
                ? "?" + param.Key + "=" + param.Value1 : current + "&" + param.Key + "=" + param.Value1);

            var response = merchantEnrolmentClient.DeleteAsync(queryString).Result;

            if (response.IsSuccessStatusCode) return response;
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new ArgumentException("Bad Request" + response.Content);

            return new HttpResponseMessage();
        }

    }
}