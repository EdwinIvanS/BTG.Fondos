using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Auth.Request
{
    public class LoginRequest
    {
        [JsonPropertyName("Username")]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Username { get; set; }

        [JsonPropertyName("Password")]
        [Required(ErrorMessage = "El Password es es obligatorio y debe ser una cadena")]
        public string Password { get; set; }
    }
}
