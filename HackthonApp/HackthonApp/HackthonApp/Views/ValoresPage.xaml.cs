using System;
using System.Collections.Generic;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class ValoresPage : ContentPage
    {
        CalculoViewModel vm;
        public ValoresPage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        public ValoresPage(CalculoViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            BindingContext = vm;
        }

        async void Handle_ProximoClicked(object sender, System.EventArgs e)
        {
            await vm.ConsultarDadosRecepcao();
            await vm.ConsultarDadosCotacao();
            await Navigation.PushAsync(new ProdutividadePage(vm));
        }



    }
}
