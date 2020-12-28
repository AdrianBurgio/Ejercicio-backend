using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataIQEjercicio.Modelo
{
    // 4 - crear la el modelo de datos (tener en cuenta los using de mongo para crear el id)
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }

    }
}
