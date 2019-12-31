using RecibosAxosApi.Utils;
using RecibosAxosApi.Utils.Request;
using RecibosAxosPersistence.Context;
using RecibosAxosPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RecibosAxosApi.Controllers
{
    public class ReciboController : ApiController
    {
        [HttpGet]
        [Authorize(Roles="Root")]
        public IHttpActionResult GetRecibos()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var username = HttpContext.Current.User.Identity.Name;
                List<Recibo> recibos = context.Set<Recibo>().ToList();
                return Content(HttpStatusCode.OK,new AxosResponse<List<Recibo>>(recibos,"Listado de recibos general"));
            }
        }
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetReciboByUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                List<Recibo> recibos = context.Set<Recibo>().Where(x=>x.Usuario.UserName.Equals(userName)).ToList();
                return Content(HttpStatusCode.OK, new AxosResponse<List<Recibo>>(recibos, $"Listado de recibos del usuario {userName}"));
            }
        }

        //[HttpPost]
        //[Authorize]
        //public IHttpActionResult RegistrarRecibo(RegistrarReciboRequest request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return ModelState.Values.ToList().ForEach(x=>x.Errors.ToList(x=>x.))
        //    }

        //}


    }
}
