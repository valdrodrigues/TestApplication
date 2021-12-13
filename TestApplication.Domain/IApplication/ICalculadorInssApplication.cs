using System.Threading.Tasks;
using TestApplication.Domain.Utils;

namespace TestApplication.Domain.IApplication
{
    public interface ICalculadorInssApplication
    {
        Task<RetornoRequisicao<decimal>> CalcularDesconto(int ano, decimal salario);
    }
}