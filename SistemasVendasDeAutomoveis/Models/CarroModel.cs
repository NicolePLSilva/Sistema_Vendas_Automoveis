using SistemasVendasDeAutomoveis.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemasVendasDeAutomoveis.Models
{
    public class CarroModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Ano { get; set; }

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public EstadoEnum Estado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public CombustivelEnum Combustivel { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public CambioEnum Cambio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public CarroceriaEnum Carroceria { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public float Quilometragem { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public float Preco { get; set; }


    }
}
