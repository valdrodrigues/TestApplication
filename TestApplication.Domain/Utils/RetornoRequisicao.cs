using System.Net;

namespace TestApplication.Domain.Utils
{
    public class RetornoRequisicao<T>
    {
        public T Objeto { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Mensagem { get; set; }

        public RetornoRequisicao()
        {
            Status = HttpStatusCode.OK;
            Mensagem = "";
        }

        public RetornoRequisicao(T obj) => Objeto = obj;
    }
}
