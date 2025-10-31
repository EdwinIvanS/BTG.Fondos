using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Fondos.Request
{
    public class CancelacionRequest
    {
        [JsonPropertyName("IdCliente")]
        [Required(ErrorMessage = "El Id del cliente es obligatorio")]
        public string IdCliente { get; set; }

        [JsonPropertyName("IdFondo")]
        [Required(ErrorMessage = "El Id del fondo es obligatorio")]
        public string IdFondo { get; set; }
    }
}
