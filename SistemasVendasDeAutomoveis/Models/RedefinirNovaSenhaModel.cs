using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class RedefinirNovaSenhaModel
    {
        [Required(ErrorMessage = "Insira sua nova senha", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Compare("NovaSenha", ErrorMessage = "Este campo não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }

        [Required]
        public string CodigoRedefinicao { get; set; }
    }
}
