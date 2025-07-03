using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ReporteService.Infrastructure.MongoDB.Documents
{
    public class ReporteDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("fechacreacion")]
        public DateTime FechaCreacion { get; set; }

        [BsonElement("idUsuario")]
        [BsonRepresentation(BsonType.String)]
        public Guid IdUsuario { get; set; }

        [BsonElement("idSubasta")]
        [BsonRepresentation(BsonType.String)]
        public Guid IdSubasta { get; set; }

    }
}