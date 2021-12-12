using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace TestApplication.Domain.Entity
{
    [BsonIgnoreExtraElements]
    public class TabelaDesconto
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("Ano")]
        public int Ano { get; set; }

        [Required]
        [BsonElement("Teto")]
        public decimal Teto { get; set; }

        [Required]
        [BsonElement("FaixaSalarial")]
        public List<FaixaTabelaDesconto> FaixaSalarial { get; set; }
    }
}
