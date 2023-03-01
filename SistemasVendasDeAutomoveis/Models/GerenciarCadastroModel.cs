using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SistemasVendasDeAutomoveis.Enums;
using SistemasVendasDeAutomoveis.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class GerenciarcadastroModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress(ErrorMessage = "Por favor, insira um endereço de e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Celular { get; set; }

        public string? CPF { get; set; }

        public string? Estado { get; set; }

        public string? Cidade { get; set; }

        public string? Endereco { get; set; }

        public bool IsAnunciante { get; set; }

    }
}
