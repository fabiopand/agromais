using System;
using System.Collections.Generic;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class SafraPage : ContentPage
    {
        CalculoViewModel vm = new CalculoViewModel();
        public SafraPage()
        {
            InitializeComponent();

            BindingContext = vm;
        }

        async void Handle_ProximoClicked(object sender, System.EventArgs e)
        {
            if (vm.Safra != null)
            {
                await vm.ConsultarDadosPropriedades();
                await Navigation.PushAsync(new PropriedadePage(vm));
            }else{
                await App.Current.MainPage.DisplayAlert("Alerta","Seleciona uma Safra","OK");
            }
        }
    }
}
