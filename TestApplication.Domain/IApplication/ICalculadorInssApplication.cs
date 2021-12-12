using System.Threading.Tasks;

namespace TestApplication.Domain.IApplication
{
    public interface ICalculadorInssApplication
    {
        Task<decimal> CalcularDesconto(int ano, decimal salario);
    }
}