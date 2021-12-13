using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Domain.IApplication;
using TestApplication.Domain.Utils;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class INSSController : ControllerBase
    {
        private readonly ISingletonCalculadorInssApplication _singletonCalculadorInssApplication;

        public INSSController(ISingletonCalculadorInssApplication singletonCalculadorInssApplication)
        {
            _singletonCalculadorInssApplication = singletonCalculadorInssApplication;
        }

        // Adicionado documentação completa só pra mostrar o payload
        /// <summary>
        /// Calcula o desconto do INSS sobre o salário
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="salario">Salario</param>
        /// <returns>Valor do desconto sobre o salário</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET
        ///     {
        ///        "data": 12/12/2011,
        ///        "salario": 2500.00
        ///     }
        ///
        /// </remarks>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Route("CalcularDescontoInss")]
        public async Task<IActionResult> CalcularDescontoInss([FromQuery] DateTime data, decimal salario)
        {
            try
            {
                RetornoRequisicao<decimal> retorno = await _singletonCalculadorInssApplication.CalcularDesconto(data.Year, salario);

                if (retorno.Status == HttpStatusCode.NoContent)
                    return StatusCode((int)HttpStatusCode.OK, retorno);

                return StatusCode((int)HttpStatusCode.OK, retorno);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
