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

    /// <summary>
    /// Excepción que se utiliza para responder de una menera clara a las peticiónes que tengan error del servidor o cliente
    /// </summary>
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