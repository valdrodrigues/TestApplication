using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestApplication.Domain.Entity;
using TestApplication.Domain.IApplication;
using TestApplication.Domain.IRepository;
using TestApplication.Domain.Utils;

namespace TestApplication.Application
{
    public class CalculadorInssApplication : ISingletonCalculadorInssApplication
    {
        private ISingletonTabelaDescontoService _tabelaDescontoService;

        public CalculadorInssApplication(ISingletonTabelaDescontoService tabelaDescontoService)
        {
            _tabelaDescontoService = tabelaDescontoService;
        }

        public async Task<RetornoRequisicao<decimal>> CalcularDesconto(int ano, decimal salario)
        {
            TabelaDesconto tabela = await _tabelaDescontoService.BuscarTabelaPeloAno(ano);

            if (tabela == null)
            {
                return new RetornoRequisicao<decimal>()
                {
                    Status = HttpStatusCode.NoContent,
                    Mensagem = "Não existe tabela de desconto cadastrada para a data informada!"
                };
            }

            decimal aliquota = 0;
            decimal maiorSalario = 0;

            foreach (var faixa in tabela.FaixaSalarial)
            {
                int maiorSalarioCompare = decimal.Compare(faixa.SalarioInicial, maiorSalario);

                if (maiorSalarioCompare == 0 || maiorSalarioCompare == 1)
                    maiorSalario = faixa.SalarioFinal;

                int salarioInicial = decimal.Compare(salario, faixa.SalarioInicial);
                int salarioFinal = decimal.Compare(salario, faixa.SalarioFinal);

                if ((salarioInicial == 0 || salarioInicial == 1) && (salarioFinal == 0 || salarioFinal == -1))
                {
                    aliquota = faixa.Aliquota;
                    break;
                }
            }

            if (aliquota == 0)
                ValidarAliquotaTeto(tabela, salario, maiorSalario, ref aliquota);

            decimal valorDesconto = (salario * aliquota) / 100;

            ValidarValorDescontoTeto(tabela.Teto, ref valorDesconto);

            return new RetornoRequisicao<decimal>()
            {
                Objeto = valorDesconto,
                Status = HttpStatusCode.OK
            };
        }

        private void ValidarAliquotaTeto(TabelaDesconto tabela, decimal salario, decimal maiorSalario, ref decimal aliquota)
        {
            int tetoCompare = decimal.Compare(salario, maiorSalario);

            if (tetoCompare == 1)
                aliquota = tabela.FaixaSalarial.Where(x => x.SalarioFinal == maiorSalario).Select(x => x.Aliquota).FirstOrDefault();
        }

        private void ValidarValorDescontoTeto(decimal teto, ref decimal valorDesconto)
        {
            int tetoCompare = decimal.Compare(valorDesconto, teto);

            if (tetoCompare == 1)
                valorDesconto = teto;
        }
    }
}
