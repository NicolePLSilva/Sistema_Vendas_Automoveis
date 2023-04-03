using SistemasVendasDeAutomoveis.Enums;

namespace SistemasVendasDeAutomoveis.Models
{
    public class VeiculosFiltroModel
    {
        public string Marca { get; set; } = "";

        public string Modelo { get; set; } = "";

        public int Ano { get; set; } 

        public string Cor { get; set; } = "";

        public EstadoEnum Estado { get; set; }

        public CombustivelEnum Combustivel { get; set; }

        public CambioEnum Cambio { get; set; }

        public CarroceriaEnum Carroceria { get; set; }

        public int QuilometragemMaxima { get; set; }

        public decimal PrecoMin { get; set; }

        public decimal PrecoMax { get; set; }

    }
}
