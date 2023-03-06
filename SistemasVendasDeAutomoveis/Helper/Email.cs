using System.Net;
using System.Net.Mail;

namespace SistemasVendasDeAutomoveis.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool EnviarEmail(string email, string assunto, string mensagem)
        {
			
			try
			{
                string userName = _configuration.GetValue<string>("SMTP:UserName");
                string nome = _configuration.GetValue<string>("SMTP:Nome");
                string host = _configuration.GetValue<string>("SMTP:Host");
                string senha = _configuration.GetValue<string>("SMTP:Senha");
                int porta = _configuration.GetValue<int>("SMTP:Porta");
                //string ssl = _configuration.GetValue<string>("SMTP:EnableSsl");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(userName, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using(SmtpClient smtp = new SmtpClient(host, porta))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(userName, senha);
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    return true;
                }
			}
			catch (Exception erro)
			{
                return false;
			} 
        }
    }
}
