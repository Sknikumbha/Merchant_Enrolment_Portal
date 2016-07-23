using MerchantEnrolmentPortal.Common.DataTransferObjects;
using MerchantEnrolmentPortal.Common.Utility;
using MerchantEnrolmentPortal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal.Filters
{
    public class SessionFilterAttribute:FilterAttribute,IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext )
        {
            SessionStateFacade.SetSession(SessionName.AKAName, "Admin");
            SessionStateFacade.SetSessionList(SessionName.CodeSetList, ValidationRulesRepository.Get());
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

    }
}