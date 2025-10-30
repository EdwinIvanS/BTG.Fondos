using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Fondos.Response
{
    public class HistorialTransaccionResponse
    {
        public string IdTransaccion { get; set; }
        public string NombreFondo { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
