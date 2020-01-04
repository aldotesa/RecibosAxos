using RecibosAxosApi.Utils;
using RecibosAxosPersistence.Context;
using RecibosAxosPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecibosAxosApi.Controllers
{
    public class ProvedorController : ApiController
    {
        [HttpGet]
        [Authorize]
        public List<ListUtil> GetProvedores()
        {
            using (var contex = new ApplicationDbContext())
            {
                return contex.Set<Provedor>().Select(x => new ListUtil { name = x.Nombre, value = x.IdProvedor.ToString() }).ToList();
            }
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult RegistrarProvedor(string nombre)
        {
            using (var context = new ApplicationDbContext())
            {
                Provedor provedor = new Provedor
                {
                    Nombre = nombre
                };
                context.Set<Provedor>().Add(provedor);
                if(context.SaveChanges()>0)
                    return Content(HttpStatusCode.Created, new AxosResponse<Guid>(provedor.IdProvedor,"Se registro correctamente"));
                return Content(HttpStatusCode.Conflict, new AxosResponse("Ocurrio un error al intentar guardar, consultar con el administrador"));
            }
        }
    }
}
