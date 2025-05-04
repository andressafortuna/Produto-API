namespace Dominio.Exceptions
{
    public class ProdutoNaoExisteException : Exception
    {
        public ProdutoNaoExisteException() : base("O Produto não existe em nosso sistema")
        {

        }
    }
}
