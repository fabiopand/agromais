using System;
using System.Collections.Generic;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class PropriedadePage : ContentPage
    {
        CalculoViewModel vm;
        public PropriedadePage()
        {
            InitializeComponent();
            BindingContext = vm;
        }

        public PropriedadePage(CalculoViewModel viewmodel)
        {
            vm = viewmodel;
            InitializeComponent();
            BindingContext = vm;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            App.Current.MainPage.DisplayAlert(" ","TODO...","OK");
        }

        async void Handle_ProximoClicked(object sender, System.EventArgs e)
        {
            await vm.ConsultarDadosCustos();
            await Navigation.PushAsync(new ValoresPage(vm));
        }
    }
}
