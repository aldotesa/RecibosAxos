using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecibosAxosApi.Utils.Request
{
    public class RegistrarReciboRequest
    {
        [Required]
        public decimal Monto { get; set; }
        [Required]
        public string Moneda { get; set; }
        public DateTime? Fecha { get; set; }
        [MaxLength(100,ErrorMessage ="El número de caracteres maximo son 100")]
        public string Comentario { get; set; }
        [Required]
        public Guid IdProvedor { get; set; }
    }
}