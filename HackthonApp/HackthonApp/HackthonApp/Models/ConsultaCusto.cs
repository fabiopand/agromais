using System;
namespace HackthonApp.Models
{
    public class ConsultaCusto
    {

        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public Propriedade Propriedade { get; set; }
        public Safra Safra { get; set; }
        public decimal Cotacao { get; set; }
        public decimal RecepcaoPorHa { get; set; }
        public Despesas Despesas { get; set; }
        public decimal CotacaoVariavel
        {
            get
            {
                var varPerc = Cotacao * (decimal)0.2;
                return MapValue(0, 1, Cotacao - varPerc, Cotacao + varPerc, SliderValue);
            }
        }
        public decimal SliderValue
        {
            get
            {

                return _sliderValue;
            }
            set
            {
                _sliderValue = value;
            }
        }
        decimal _sliderValue = (decimal)0.5;

        public decimal Rentabilidade
        {
            get
            {

                return TotalReceita - TotalDespesa;
            }
        }
        public decimal TotalDespesa
        {
            get
            {
                if (Despesas != null)
                    return Despesas.Insumos + Despesas.Operacionais + Despesas.Outros;
                return 0;
            }
        }

        public decimal TotalReceita
        {
            get
            {
                if (Propriedade != null)
                    return Propriedade.Area * CotacaoVariavel * RecepcaoPorHa;
                return 0;
            }

        }

        public decimal MapValue(decimal a0, decimal a1, decimal b0, decimal b1, decimal a)
        {
            return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
        }
    }
}
