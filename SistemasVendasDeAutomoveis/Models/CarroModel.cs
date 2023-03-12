using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SistemasVendasDeAutomoveis.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SistemasVendasDeAutomoveis.Models
{
    public class CarroModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(15, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int Ano { get; set; }

        public string? Descricao { get; set; } = " ";

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public EstadoEnum Estado { get; set; }

        [Required(ErrorMessage = "Selecione uma opção")]
        public CombustivelEnum Combustivel { get; set; }

        [Required(ErrorMessage = "Selecione uma opção")]
        public CambioEnum Cambio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public CarroceriaEnum Carroceria { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(11, ErrorMessage = "Excedeu número máximo de caracteres")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(14, ErrorMessage = "Excedeu número máximo de caracteres")]
        [DisplayName("Preço do veículo")]
        public decimal Preco { get; set; }

        public UsuarioModel Vendedor { get; set; }
        public UsuarioModel Comprador { get; set; }

        [ValidateNever]
        public bool Vendido { get; set; }    

    }
}
