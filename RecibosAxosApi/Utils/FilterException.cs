using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace RecibosAxosApi.Utils
{
    /// <summary>
    /// Encapsula los errores que se generen en el API y regresan una respuesta en base a la excepción y que sea facil darle lectura al consumirla
    /// </summary>
    public class FilterException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new ApiException(context.Exception).Response;
        }
    }
}