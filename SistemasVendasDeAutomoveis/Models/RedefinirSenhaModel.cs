using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class RedefinirSenhaModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail digitado é inválido")]
        public string Email { get; set; }
    }
}
