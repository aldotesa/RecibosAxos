using RecibosAxosPersistence.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecibosAxosApi.Utils.ViewModels
{
    public class ReciboViewModel : IRecibo
    {
        public decimal Monto { get ; set ; }
        public string Moneda { get ; set ; }
        public string Comentario { get ; set ; }
        public Guid IdProvedor { get ; set ; }
        public DateTime Fecha { get ; set ; }
        public string NombreProvedor { get; set; }
        public string NombreRegistro { get; set; }
    }
}