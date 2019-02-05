using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HackthonApp.Views;
using HackthonApp.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HackthonApp
{
    public partial class App : Application
    {
        public static Produtor ProdutorLogin { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
