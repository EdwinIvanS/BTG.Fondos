using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Fondos.Request
{
    public class SuscripcionRequest
    {
        [JsonPropertyName("IdCliente")]
        [Required(ErrorMessage = "El Id del cliente es obligatorio")]
        public string IdCliente { get; set; }

        [JsonPropertyName("IdFondo")]
        [Required(ErrorMessage = "El Id del fondo es obligatorio")]
        public string IdFondo { get; set; }

        [JsonPropertyName("Monto")]
        [Range(0, 999999, ErrorMessage = "El Monto debe estar entre 0 y 999999")]
        public decimal Monto { get; set; }
    }
}
