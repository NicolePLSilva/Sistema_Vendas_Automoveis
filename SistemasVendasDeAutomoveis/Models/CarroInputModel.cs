using SistemasVendasDeAutomoveis.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SistemasVendasDeAutomoveis.Models
{
    public class CarroInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(15, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int? Ano { get; set; }

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(20, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public EstadoEnum Estado { get; set; }

        [Required(ErrorMessage = "Selecione uma opção")]
        public CombustivelEnum? Combustivel { get; set; }

        [Required(ErrorMessage = "Selecione uma opção")]
        public CambioEnum? Cambio { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public CarroceriaEnum? Carroceria { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(11, ErrorMessage = "Excedeu número máximo de caracteres")]
        public string Quilometragem { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(14, ErrorMessage = "Excedeu número máximo de caracteres")]
        [DisplayName("Preço do veículo")]
        public string? Preco { get; set; }


        public int QuilometragemParaInt(string kmStr)
        {
            kmStr = new string(kmStr.Where(char.IsDigit).ToArray());

            CultureInfo culture = new CultureInfo("pt-BR");
            int km = Int32.Parse(kmStr, culture);

            return km;
        }

        public decimal PrecoParaDecimal(string precoStr)
        {
            precoStr = new string(precoStr.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());

            CultureInfo culture = new CultureInfo("pt-BR");
            decimal preco = decimal.Parse(precoStr, NumberStyles.Currency, culture);

            return preco;
        }


    }
}
