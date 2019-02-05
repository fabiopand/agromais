using System;
using System.Collections.Generic;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class ResultadosSimulacoesPage : ContentPage
    {
        ResultadoSimulacaoViewModel vm;
        public ResultadosSimulacoesPage()
        {
            InitializeComponent();
            vm = new ResultadoSimulacaoViewModel();
            BindingContext = vm;
        }



        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SafraPage());
        }


        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new HomePage());
        }
    }
}
