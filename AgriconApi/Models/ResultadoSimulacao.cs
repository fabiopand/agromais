//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgriconApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResultadoSimulacao
    {
        public long Id { get; set; }
        public Nullable<long> QuantidadeSacas { get; set; }
        public Nullable<decimal> Preco { get; set; }
        public Nullable<decimal> SomaDespesas { get; set; }
        public Nullable<decimal> SomaReceitas { get; set; }
        public Nullable<decimal> Rentabilidade { get; set; }
        public Nullable<long> PropriedadeSafraId { get; set; }
    
        public virtual PropriedadeSafra PropriedadeSafra { get; set; }
    }
}
