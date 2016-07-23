using MerchantEnrolmentPortal.Filters;
using System.Web;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionFilterAttribute());
        }
    }
}
