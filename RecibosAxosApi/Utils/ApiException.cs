using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RecibosAxosApi.Utils
{
    public class ApiException: HttpResponseException
    {
        public ApiException(Exception ex) : base(new HttpResponseMessage(HttpStatusCode.InternalServerError)
        {
            Content = new StringContent(JsonConvert.SerializeObject(new AxosResponse(ex.Message)))
        })
        {
            HResult = ex.HResult;
        }
    }
}