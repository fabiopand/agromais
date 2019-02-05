using System;
using System.Collections.Generic;

namespace HackthonApp.Models
{
    public class Produtor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public List<Propriedade> Propriedade { get; set; }
    }
}
