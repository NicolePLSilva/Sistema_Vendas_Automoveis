namespace SistemasVendasDeAutomoveis.Helper
{
    public interface IEmail
    {
        bool EnviarEmail(string email, string assunto, string mensagem);
    }
}
