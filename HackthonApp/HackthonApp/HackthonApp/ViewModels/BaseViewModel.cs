using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace HackthonApp.ViewModels
{

    public class BaseViewModel : INotifyPropertyChanged
    {

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

