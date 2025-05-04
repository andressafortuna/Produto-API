namespace Infra.Exceptions
{
    public class AcessoDadosException : Exception
    {
        public AcessoDadosException()
            : base("Erro ao acessar dados.") { }

        public AcessoDadosException(string mensagem, Exception excecao)
            : base(mensagem, excecao) { }
    }
}
