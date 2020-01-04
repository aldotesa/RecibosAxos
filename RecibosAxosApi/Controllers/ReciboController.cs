using Microsoft.AspNet.Identity;
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
using System.Data.Entity;
using RecibosAxosApi.Utils.ViewModels;
using Swashbuckle.Swagger.Annotations;
using RecibosAxosApi.Utils.Devextreme;
using DevExtreme.AspNet.Data;

namespace RecibosAxosApi.Controllers
{
    [FilterException]
    public class ReciboController : ApiController
    {
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse<List<ReciboViewModel>>))]
        [HttpGet]
        [Authorize(Roles = "Root")]
        public IHttpActionResult GetAllRecibos()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<ReciboViewModel> recibos =
                    context.Set<Recibo>()
                    .Include(x => x.Provedor)
                    .Include(x => x.Usuario)
                    .Select(x =>
                    new ReciboViewModel { Comentario = x.Comentario, Fecha = x.Fecha, Moneda = x.Moneda, Monto = x.Monto, NombreProvedor = x.Provedor.Nombre, NombreRegistro = x.Usuario.UserName, IdProvedor = x.IdProvedor }
                    ).ToList();
                return Content(HttpStatusCode.OK, new AxosResponse<List<ReciboViewModel>>(recibos, "Listado de recibos general"));
            }
        }
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse<List<ReciboViewModel>>))]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetReciboByUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var userName = HttpContext.Current.User.Identity.Name;
                List<ReciboViewModel> recibos =
                    context.Set<Recibo>()
                    .Where(x => x.Usuario.UserName.Equals(userName))
                    .Include(x => x.Provedor)
                    .Include(x => x.Usuario)
                    .Select(x =>
                    new ReciboViewModel { Comentario = x.Comentario, Fecha = x.Fecha, Moneda = x.Moneda, Monto = x.Monto, NombreProvedor = x.Provedor.Nombre, NombreRegistro = x.Usuario.UserName, IdProvedor = x.IdProvedor }
                    )
                    .ToList();
                return Content(HttpStatusCode.OK, new AxosResponse<List<ReciboViewModel>>(recibos, $"Listado de recibos del usuario {userName}"));
            }
        }
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(AxosResponse<int>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult RegistrarRecibo(RegistrarReciboRequest request)
        {
            if (!ModelState.IsValid)
            {
                //Mensaje separado por puntos, validación del lado del servidor
                return Content(HttpStatusCode.BadRequest, new AxosResponse(string.Join("", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));

            }
            using (var context = new ApplicationDbContext())
            {
                Recibo recibo = new Recibo(request);
                recibo.IdUsuario = HttpContext.Current.User.Identity.GetUserId();
                context.Set<Recibo>().Add(recibo);
                if (context.SaveChanges() > 0)
                    return Content(HttpStatusCode.Created, new AxosResponse<int>(recibo.IdRecibo, "Se registro correctamente el recibo"));
                return Content(HttpStatusCode.Conflict, new AxosResponse("Ocurrió un error al guardar el recibo contacte con la administración"));
            }
        }
        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(AxosResponse))]
        [HttpDelete]
        [Authorize]
        public IHttpActionResult EliminarRecibo(int idRecibo)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Recibo recibo = context.Set<Recibo>().Find(idRecibo);
                if (recibo != null)
                {
                    context.Set<Recibo>().Remove(recibo);
                    if (context.SaveChanges() > 0)
                        return Content(HttpStatusCode.NoContent, new AxosResponse("Se eliminó correctamente el recibo"));
                    return Content(HttpStatusCode.Conflict, new AxosResponse("Ocurrió un error inesperado favor de comunicarse con el departamento de soporte técnico"));
                }
                return Content(HttpStatusCode.NotFound, new AxosResponse("No se encontro el registro solicitado"));
            }
        }

        [SwaggerResponse(HttpStatusCode.NoContent, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.Conflict, Type = typeof(AxosResponse))]
        [HttpPut]
        [Authorize]
        public IHttpActionResult EditarRecibo(EditarReciboRequest request)
        {
            if (!ModelState.IsValid)
            {
                //Mensaje separado por puntos, validación del lado del servidor
                return Content(HttpStatusCode.BadRequest, new AxosResponse(string.Join("", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage))));

            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Recibo recibo = context.Set<Recibo>().Include(x => x.Provedor).FirstOrDefault(x => x.IdRecibo == request.IdRecibo);
                if (recibo != null)
                {
                    recibo.Fecha = request.Fecha;
                    recibo.Moneda = request.Moneda;
                    recibo.Monto = request.Monto;
                    recibo.IdProvedor = request.IdProvedor;
                    recibo.Comentario = request.Comentario;
                    context.SaveChanges();
                    return Content(HttpStatusCode.NoContent, new AxosResponse("Se editó Correctamente"));
                    //return Content(HttpStatusCode.Conflict, new AxosResponse("Ocurrió un error consulte con el administrador del sistema"));
                }
                return Content(HttpStatusCode.NotFound, new AxosResponse("No se encontro ningun registro favor de verificar la información"));
            }
        }


        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(AxosResponse<Recibo>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(AxosResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(AxosResponse))]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetRecibo(int idRecibo)
        {
            using (var context = new ApplicationDbContext())
            {
                var recibo = context.Set<Recibo>().Include(x => x.Provedor).Include(x => x.Usuario).FirstOrDefault(x => x.IdRecibo == idRecibo);
                if (recibo != null)
                    return Content(HttpStatusCode.OK, new AxosResponse<Recibo>(recibo, $"Recibo {recibo.IdRecibo}"));
                return Content(HttpStatusCode.NotFound, new AxosResponse("No se encuentra el recibo que se solicito"));
            }
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetRecibosByUserGrid(DataSourceLoadOptions loadOptions)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            return Request.CreateResponse(DataSourceLoader.Load(new ApplicationDbContext().Set<Recibo>().OrderBy(x => x.IdRecibo).Where(x => x.Usuario.UserName.Equals(userName)).Select(obj => new
            {
                obj.IdRecibo,
                obj.Moneda,
                obj.Monto,
                obj.Fecha,
                obj.Provedor.Nombre,
                obj.Usuario.UserName,
                obj.Comentario
            }), loadOptions));
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetRecibosGlobal(DataSourceLoadOptions loadOptions)
        {
            return Request.CreateResponse(DataSourceLoader.Load(new ApplicationDbContext().Set<Recibo>().OrderBy(x => x.IdRecibo).Select(obj => new
            {
                obj.IdRecibo,
                obj.Moneda,
                obj.Monto,
                obj.Fecha,
                obj.Provedor.Nombre,
                obj.Usuario.UserName,
                obj.Comentario
            }), loadOptions));
        }

    }
}
