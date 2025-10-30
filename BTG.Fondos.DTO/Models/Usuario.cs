using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [BsonElement("contrasenaHash")]
        public string ContrasenaHash { get; set; }

        [BsonElement("rol")]
        public string Rol { get; set; } // "Cliente", "Admin"

        [BsonElement("email")]
        public string Email { get; set; }
    }
}
