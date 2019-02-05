using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HackthonApp.Models;
using HackthonApp.Services;

namespace HackthonApp.ViewModels
{
    public class CalculoViewModel : BaseViewModel
    {

        public CalculoViewModel()
        {
            Task.Run(() => ConsultarDadosSafras());
            ConsultaCusto = new ConsultaCusto();
        }


        ConsultaCusto consultaCusto;
        public ConsultaCusto ConsultaCusto
        {
            get
            {
                return consultaCusto;
            }
            set
            {
                consultaCusto = value;
                OnPropertyChanged(nameof(ConsultaCusto));
            }
        }


        decimal cotacaoVariavel;
        public decimal CotacaoVariavel
        {
            get
            {
                return cotacaoVariavel;
            }
            set
            {
                cotacaoVariavel = value;
                OnPropertyChanged(nameof(CotacaoVariavel));
            }
        }


        List<Safra> listaSafras;
        public List<Safra> ListaSafras
        {
            get
            {
                return listaSafras;
            }
            set
            {
                listaSafras = value;
                Safra = listaSafras.FirstOrDefault();
                OnPropertyChanged(nameof(ListaSafras));
            }
        }

        Safra safra;
        public Safra Safra
        {
            get
            {
                return safra;
            }
            set
            {
                safra = value;
                OnPropertyChanged(nameof(Safra));
            }
        }

        decimal sliderValue =(decimal)0.5;
        public decimal SliderValue
        {
            get
            {
                return sliderValue;
            }
            set
            {
                sliderValue = value;
                OnPropertyChanged(nameof(SliderValue));
            }
        }


        List<Propriedade> listaPropriedades;
        public List<Propriedade> ListaPropriedades
        {
            get
            {
                return listaPropriedades;
            }
            set
            {
                listaPropriedades = value;
                Propriedade = listaPropriedades.FirstOrDefault();
                OnPropertyChanged(nameof(ListaPropriedades));
            }
        }

        Propriedade propriedade;
        public Propriedade Propriedade
        {
            get
            {
                return propriedade;
            }
            set
            {
                propriedade = value;
                OnPropertyChanged(nameof(Propriedade));
            }
        }

        Despesas despesas;
        public Despesas Despesas{
            get{
                return despesas;
            }
            set{
                despesas = value;
                OnPropertyChanged(nameof(Despesas));
            }
        }


        async Task ConsultarDadosSafras()
        {
            var service = new AgriconService();
            try
            {
                ListaSafras = await service.GetSafras();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

        public async Task ConsultarDadosPropriedades()
        {
          
            try
            {
                if (safra != null)
                {
                    var service = new AgriconService();
                    ListaPropriedades = await service.GetPropriedades(Safra, App.ProdutorLogin);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

        public async Task ConsultarDadosCustos()
        {

            try
            {
                if (propriedade != null)
                {
                    var service = new AgriconService();
                    Despesas = await service.GetDespesas(Safra, propriedade);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

        public async Task ConsultarDadosRecepcao()
        {

            try
            {
                if (propriedade != null)
                {
                    var service = new AgriconService();
                    var recepcao = await service.GetRecepcao(Safra, propriedade);
                    consultaCusto.RecepcaoPorHa = recepcao.QuantidadeSacas / Propriedade.Area;
                    OnPropertyChanged(nameof(ConsultaCusto));
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }

        public async Task ConsultarDadosCotacao()
        {

            try
            {
                if (propriedade != null)
                {
                    var service = new AgriconService();
                    var cotacao = await service.GetCotacao();
                    consultaCusto.Cotacao = cotacao.Valor;
                    OnPropertyChanged(nameof(ConsultaCusto));
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

        }



        public double MapValue(double a0, double a1, double b0, double b1, double a)
        {
            return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
        }




    }
}
