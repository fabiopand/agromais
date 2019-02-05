using System;
namespace HackthonApp.Models
{
    public class ConsultaCustoPost
    {
        public long QuantidadeSacas { get; set; }
        public decimal Preco { get; set; }
        public decimal SomaDespesas { get; set; }
        public decimal SomaReceitas { get; set; }
        public decimal Rentabilidade { get; set; }
        public int PropriedadeId { get; set; }
        public int SafraId { get; set; }

    }
}
