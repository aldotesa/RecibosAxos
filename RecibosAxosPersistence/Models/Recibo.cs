using RecibosAxosPersistence.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Models
{
    public class Recibo: IRecibo
    {

        public Recibo()
        {

        }

        public Recibo(IRecibo recibo)
        {
            Monto = recibo.Monto;
            Moneda = recibo.Moneda;
            Fecha = recibo.Fecha;
            Comentario = recibo.Comentario;
            IdProvedor = recibo.IdProvedor;
        }
        public int IdRecibo { get; set; }
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
