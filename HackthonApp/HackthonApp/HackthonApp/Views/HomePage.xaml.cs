using System;
using System.Collections.Generic;
using HackthonApp.ViewModels;
using Xamarin.Forms;

namespace HackthonApp.Views
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
