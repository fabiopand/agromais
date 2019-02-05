using System;
using HackthonApp.Models;

namespace HackthonApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        Produtor produtor;
        public Produtor Produtor{
            get{
                return produtor;
            }
            set{
                produtor = value;
                OnPropertyChanged(nameof(Produtor));
            }
        }

    }
}
