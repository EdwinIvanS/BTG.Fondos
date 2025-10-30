using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Fondos.DTO.Models
{
    public class Cliente
    {
        [BsonId] 
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public decimal Saldo { get; set; } = 500000; 
        public string PreferenciaNotificacion { get; set; } 
    }
}
