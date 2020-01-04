using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace RecibosAxosApi.Utils
{
    public class FiltroSwagger : IDocumentFilter
    {
        /// <summary>
        /// Se utiliza para exponer solo las api que se desean
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths =
                swaggerDoc.paths.Where(e => e.Key.Equals("/api/Recibo/GetAllRecibos") || e.Key.Equals("/api/Recibo/GetReciboByUser") || e.Key.Equals("/api/Recibo/RegistrarRecibo") || e.Key.Equals("/api/Recibo/EliminarRecibo") || e.Key.Equals("/api/Recibo/EditarRecibo") || e.Key.Equals("/api/Recibo/GetRecibo") || e.Key.Equals("/api/Account/Register") || e.Key.Contains("api/Provedor/")
                                            ).ToDictionary(
                    x => x.Key, x => x.Value);
        }
    }
}