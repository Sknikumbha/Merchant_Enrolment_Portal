using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MerchantEnrolmentPortal.Filters
{
    public class OnExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //log exception to db/enterprise loogin/flat file
            throw new NotImplementedException();
        }
    }
}