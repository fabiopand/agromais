
namespace AgriconApi.Models
{
    using System;
    using System.Collections.Generic;

    public partial class ResultadoSimulacao
    {
        public Nullable<long> PropriedadeId { get; set; }
        public Nullable<long> SafraId { get; set; }
        public string SafraDescricao { get; set; }
    }
}
