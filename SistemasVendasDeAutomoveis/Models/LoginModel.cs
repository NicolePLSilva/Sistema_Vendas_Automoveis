using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Campo obrigatório para efetuar o login!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório para efetuar o login!")]
        public string Senha { get; set; }

        public bool LembrarLogin { get; set; }
 
    }
}
