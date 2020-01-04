using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace RecibosAxosApi.Utils
{
    public class FilterException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new ApiException(context.Exception).Response;
        }
    }
}