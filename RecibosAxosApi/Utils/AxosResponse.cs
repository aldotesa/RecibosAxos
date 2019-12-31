using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecibosAxosApi.Utils
{
    /// <summary>
    /// Esta clase se utiliza para estandarizar las respuestas y siempre contengan una descripción y un codigo de respuesta
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AxosResponse
    {
        public AxosResponse(string mensaje)
        {
            Mensaje = mensaje;
        }

        public string Mensaje { get; set; }
    }

    public class AxosResponse<TResult> : AxosResponse
    {
        public AxosResponse(TResult objectResponse, string mensaje) : base(mensaje)
        {
            ObjectResponse = objectResponse;
        }

        public TResult ObjectResponse { get; set; }
    }
}