using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class CadastroModel
    {
        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "Senha não confere com a nova senha")]
        public string ConfirmarSenha { get; set; }

    }
}
