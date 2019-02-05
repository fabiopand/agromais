using System;
using System.Collections.Generic;
using System.Diagnostics;
using HackthonApp.Models;
using HackthonApp.Services;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class ProdutividadePage : ContentPage
    {

        CalculoViewModel vm;
        public ProdutividadePage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        public ProdutividadePage(CalculoViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            BindingContext = vm;

            vm.ConsultaCusto.Despesas = vm.Despesas;
            vm.ConsultaCusto.Propriedade = vm.Propriedade;
            vm.ConsultaCusto.Safra = vm.Safra;
            vm.OnPropertyChanged(nameof(vm.ConsultaCusto));
        
        }

        async void Handle_SalvarClicked(object sender, System.EventArgs e)
        {
            try
            {
                ConsultaCustoPost cons = new ConsultaCustoPost
                {
                    Preco = vm.ConsultaCusto.CotacaoVariavel,
                    QuantidadeSacas = Convert.ToInt64(vm.ConsultaCusto.RecepcaoPorHa),
                    Rentabilidade = vm.ConsultaCusto.Rentabilidade,
                    SomaDespesas = vm.ConsultaCusto.TotalDespesa,
                    SomaReceitas = vm.ConsultaCusto.TotalReceita,
                    PropriedadeId = vm.Propriedade.Id,
                    SafraId = vm.Safra.Id

                };
                var service = new AgriconService();
                var retorno = await service.PostConsultaCusto(cons);
                if (retorno != null)
                {
                    await App.Current.MainPage.DisplayAlert("Enviado", "Simulação gravada", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Não foi possível enviar a simulação", "Fechar");
                }
            }catch(Exception ex){
                Debug.WriteLine(ex.StackTrace);
                await App.Current.MainPage.DisplayAlert("Erro", "Erro não esperado", "Fechar");
            }

        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new ResultadosSimulacoesPage());
        }

        bool isChange = true;
        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (isChange)
            {
                isChange = false;
                var value = (decimal)((Xamarin.Forms.Slider)sender).Value;

                var valor = vm.ConsultaCusto.Cotacao * (decimal)0.2;
                //vm.CotacaoVariavel = MapValue(0, 1, vm.ConsultaCusto.Cotacao - valor, vm.ConsultaCusto.Cotacao + valor, value);
                vm.SliderValue = valor;
                vm.OnPropertyChanged(nameof(vm.ConsultaCusto));
                isChange = true;
            }


        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            vm.OnPropertyChanged(nameof(vm.ConsultaCusto));
        }

        public decimal MapValue(decimal a0, decimal a1, decimal b0, decimal b1, decimal a)
        {
            return b0 + (b1 - b0) * ((a - a0) / (a1 - a0));
        }
    }
}
