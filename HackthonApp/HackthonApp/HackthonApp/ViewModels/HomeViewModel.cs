using System;
using System.Collections.Generic;

namespace HackthonApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {



        public List<KeyValuePair<string, string>> ListaNoticias
        {
            get
            {
                var retorno = new List<KeyValuePair<string, string>>();
                retorno.Add(new KeyValuePair<string, string>("Ferrugem na Soja","Alerta de Ferrugem na Soja para agricultora da região oeste..."));
                retorno.Add(new KeyValuePair<string, string>("Milho em alta", "Preço dispara após notícias de quebra de safra no exterior..."));
                retorno.Add(new KeyValuePair<string, string>("Distribuição de Sobras", "As sobras começam ser distribuídas essa semana..."));
                retorno.Add(new KeyValuePair<string, string>("Promoção de Inseticidas", "Inseticidas com até 25% de desconto até o final de semana..."));

                return retorno;
            }
        }
    }
}
