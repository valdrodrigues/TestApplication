using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestApplication.Domain.Entity;
using TestApplication.Domain.IRepository;
using TestApplication.Infra.Configuration;

namespace TestApplication.Infra.Repository
{
    public class TabelaDescontoService : ISingletonTabelaDescontoService
    {
        private readonly IMongoCollection<TabelaDesconto> _tabelaDesconto;
        private readonly TabelaDescontoConfiguration _settings;

        public TabelaDescontoService(IOptions<TabelaDescontoConfiguration> settings)
        {
            _settings = settings.Value;
            MongoClient client = new MongoClient(_settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(_settings.DatabaseName);
            _tabelaDesconto = database.GetCollection<TabelaDesconto>(_settings.TabelaDescontoCollectionName);
        }

        public async Task<TabelaDesconto> BuscarTabelaPeloAno(int ano)
        {
            return await _tabelaDesconto.Find(x => x.Ano == ano).FirstOrDefaultAsync();
        }

        // Objetos inseridos manualmente

        //db.TabelaDesconto.insert( {
        //Ano: NumberInt(2011), 
        //Teto: 405.86,
        //FaixaSalarial: [
        // { SalarioInicial: 1006.90, SalarioFinal: 1006.90, Aliquota: 8 }, 
        // { SalarioInicial: 1006.91, SalarioFinal: 1844.83, Aliquota: 9 }, 
        // { SalarioInicial: 1844.84, SalarioFinal: 3689.66, Aliquota: 11 }
        // ] } )

        //db.TabelaDesconto.insert( {
        //Ano: NumberInt(2012), 
        //Teto: 500.00,
        //FaixaSalarial: [
        // { SalarioInicial: 1000.00, SalarioFinal: 1000.00, Aliquota: 7 }, 
        // { SalarioInicial: 1000.01, SalarioFinal: 1500.00, Aliquota: 8 }, 
        // { SalarioInicial: 1500.01, SalarioFinal: 3000.00, Aliquota: 9 },
        // { SalarioInicial: 3000.01, SalarioFinal: 4000.00, Aliquota: 11 }
        // ] } )
    }
}
