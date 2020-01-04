using RecibosAxosPersistence.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecibosAxosApi.Utils.Request
{
    /// <summary>
    /// Clase que se utiliza para registrar un recibo
    /// </summary>
    public class RegistrarReciboRequest : IRecibo
    {
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        [MaxLength(100, ErrorMessage = "El número de caracteres maximo son 100")]
        public string Comentario { get; set; }
        [Required]
        public Guid IdProvedor { get; set; }
    }
    public class EditarReciboRequest : IRecibo
    {
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public string Moneda { get; set; }
        [MaxLength(100, ErrorMessage = "El número de caracteres maximo son 100")]
        public string Comentario { get; set; }
        [Required]
        public Guid IdProvedor { get; set; }
        public DateTime Fecha { get; set; }
        [Required]
        public int IdRecibo { get; set; }
    }
}