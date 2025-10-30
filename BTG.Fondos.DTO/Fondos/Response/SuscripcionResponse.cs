using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Fondos.Response
{
    public class SuscripcionResponse
    {
        public string IdTransaccion { get; set; }
        public string Mensaje { get; set; }
        public decimal SaldoActual { get; set; }
    }
}
