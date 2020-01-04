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
    [Authorize]
    public class ProvedorController : ApiController
    {
        /// <summary>
        /// Regresa los provedores con los entity que persisten en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<ListUtil> GetProvedores()
        {
            using (var contex = new ApplicationDbContext())
            {
                return contex.Set<Provedor>().Select(x => new ListUtil { name = x.Nombre, value = x.IdProvedor.ToString() }).ToList();
            }
        }
        /// <summary>
        /// Registra un provedor con su nombre, se utiliza para generar provedores de manera rapida en la vista de registrar recibo
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult RegistrarProvedor(string nombre)
        {
            using (var context = new ApplicationDbContext())
            {
                Provedor provedor = new Provedor
                {
                    Nombre = nombre
                };
                context.Set<Provedor>().Add(provedor);
                if (context.SaveChanges() > 0)
                    return Content(HttpStatusCode.Created, new AxosResponse<Guid>(provedor.IdProvedor, "Se registro correctamente"));
                return Content(HttpStatusCode.Conflict, new AxosResponse("Ocurrio un error al intentar guardar, consultar con el administrador"));
            }
        }
    }
}
