using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackthonApp.Models;
using HackthonApp.Services;

namespace HackthonApp.ViewModels
{
    public class ResultadoSimulacaoViewModel : BaseViewModel
    {
        public ResultadoSimulacaoViewModel()
        {
            Task.Run(() => Consultar());
        }

        public List<ResultadoSimulacao> _lista;
        public List<ResultadoSimulacao> Lista
        {
            get{
                return _lista;
            }
            set{
                _lista = value;
                OnPropertyChanged(nameof(Lista));
            }
        }


        async Task Consultar()
        {
            var service = new AgriconService();
            Lista = await service.GetResultadoSimulacoes();


        }
    }
}
