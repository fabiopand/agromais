using System;
namespace HackthonApp.Models
{
    public class Propriedade
    {
        public int Id { get; set; }
        public string CadPro { get; set; }
        public decimal Area { get; set; }
        public string Nome { get; set; }
        public int ProdutorId { get; set; }
        //public Produtor Produtor { get; set; }
        //public PropriedadeSafra PropriedadeSafra { get; set; }
    }
}
