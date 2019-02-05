using System;
namespace HackthonApp.Models
{
    public class ResultadoSimulacao
    {
        public int Id { get; set; }
        public int QuantidadeSacas { get; set; }
        public decimal Preco { get; set; }
        public decimal SomaDespesas { get; set; }
        public decimal SomaReceitas { get; set; }
        public decimal Rentabilidade { get; set; }
        public string SafraDescricao { get; set; }
    }
}
