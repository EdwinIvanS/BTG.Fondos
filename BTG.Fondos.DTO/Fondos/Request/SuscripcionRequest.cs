using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Fondos.Request
{
    public class SuscripcionRequest
    {
        public string IdCliente { get; set; }
        public string IdFondo { get; set; }
        public decimal Monto { get; set; }
    }
}
