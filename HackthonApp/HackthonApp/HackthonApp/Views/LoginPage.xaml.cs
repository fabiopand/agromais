using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackthonApp.Services;
using HackthonApp.ViewModels;
using Xamarin.Forms;


namespace HackthonApp.Views
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            vm = new LoginViewModel();

            vm.Produtor = new Models.Produtor();
            vm.Produtor.CPF = "123456789-00";
            vm.Produtor.Senha = "123456";

            BindingContext = vm;

            //todo remover codigo


        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {

            if (vm == null || vm.Produtor == null || string.IsNullOrEmpty(vm.Produtor.CPF) || string.IsNullOrEmpty(vm.Produtor.Senha))
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Preencher todos os campos", "OK");
                return;
            }
            try
            {
                var service = new AgriconService();
                var retorno = await service.DoLogin(vm.Produtor);

                if (retorno == null)
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Não foi possível conectar", "OK");
                }
                else
                {
                    vm.Produtor.Id = retorno.Id;
                    App.ProdutorLogin = vm.Produtor;
                    // App.Current.MainPage = new NavigationPage(new SafraPage());
                    App.Current.MainPage = new NavigationPage(new ResultadosSimulacoesPage());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            };
        }
    }
}
