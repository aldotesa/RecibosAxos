using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Models
{
    public class Provedor
    {
        public Provedor()
        {
            Recibos = new List<Recibo>();
        }
        public Guid IdProvedor { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Recibo> Recibos { get; set; }
    }
}
