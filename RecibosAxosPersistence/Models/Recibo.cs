using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Models
{
    public class Recibo
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public virtual Provedor Provedor { get; set; }
        public Guid IdProvedor { get; set; }
        public ApplicationUser Usuario { get; set; }
        public string IdUsuario { get; set; }
    }
}
