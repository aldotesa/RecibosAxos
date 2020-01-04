using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecibosAxosPersistence.Models.Interfaces
{
    public interface IRecibo
    {
        decimal Monto { get; set; }
        string Moneda { get; set; }
        string Comentario { get; set; }
        Guid IdProvedor { get; set; }
        DateTime Fecha { get; set; }
    }
}
