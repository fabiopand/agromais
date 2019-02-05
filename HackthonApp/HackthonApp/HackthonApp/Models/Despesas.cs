using System;
namespace HackthonApp.Models
{
    public class Despesas
    {
        public decimal Insumos { get; set; }
        public decimal Operacionais { get; set; }
        public decimal Outros { get; set; }
        public ConsultaCusto ConsultaCusto { get; set; }
    }
}
