using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a senha atual")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Digite a senha atual")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }

    }
}
