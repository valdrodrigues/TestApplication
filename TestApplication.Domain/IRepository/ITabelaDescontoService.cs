using System.Threading.Tasks;
using TestApplication.Domain.Entity;

namespace TestApplication.Domain.IRepository
{
    public interface ITabelaDescontoService
    {
        Task<TabelaDesconto> BuscarTabelaPeloAno(int ano);
    }
}
