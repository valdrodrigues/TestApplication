using MongoDB.Bson.Serialization.Attributes;

namespace TestApplication.Domain.Entity
{
    public class FaixaTabelaDesconto
    {
        [BsonElement("SalarioInicial")]
        public decimal SalarioInicial { get; set; }

        [BsonElement("SalarioFinal")]
        public decimal SalarioFinal { get; set; }

        [BsonElement("Aliquota")]
        public decimal Aliquota { get; set; }
    }
}
