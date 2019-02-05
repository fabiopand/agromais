using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HackthonApp.Models;
using Newtonsoft.Json;
using System.Linq;

namespace HackthonApp.Services
{
    public class AgriconService
    {
        string url = "http://172.16.3.165:55527/api/";
        private HttpClient httpClient;


        public AgriconService()
        {
            httpClient = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            httpClient.BaseAddress = new Uri(url);

            //httpClient.DefaultRequestHeaders.Add("key", "value");
            httpClient.Timeout = TimeSpan.FromSeconds(30);

            if (App.ProdutorLogin == null || string.IsNullOrEmpty(App.ProdutorLogin.CPF))
                return;

            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(App.ProdutorLogin.CPF + ":" + App.ProdutorLogin.Senha));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encoded);

        }


        #region Chamadas WS

        public async Task<List<Safra>> GetSafras()
        {
            var retornoObject = await new AgriconService().CallApiMethodGet("safras");
            var retorno = JsonConvert.DeserializeObject<List<Safra>>(retornoObject.ToString());
            return retorno.OrderByDescending(x => x.Descricao).ToList();
        }

        public async Task<List<Propriedade>> GetPropriedades(Safra safra, Produtor produtor)
        {
            var retornoObject = await new AgriconService().CallApiMethodGet(string.Format("propriedades?safraId={0}&produtorId={1}",safra.Id,produtor.Id));
            var retorno = JsonConvert.DeserializeObject<List<Propriedade>>(retornoObject.ToString());
            return retorno.OrderBy(x => x.Id).ToList();
        }

        public async Task<Despesas> GetDespesas(Safra safra, Propriedade propriedade)
        {
            var retornoObject = await new AgriconService().CallApiMethodGet(string.Format("despesas?safraId={0}&propriedadeId={1}", safra.Id, propriedade.Id));
            var retorno = JsonConvert.DeserializeObject<Despesas>(retornoObject.ToString());
            return retorno;
        }

        public async Task<Recepcao> GetRecepcao(Safra safra, Propriedade propriedade)
        {
            var retornoObject = await new AgriconService().CallApiMethodGet(string.Format("recepcaos?safraId={0}&propriedadeId={1}", safra.Id, propriedade.Id));
            var retorno = JsonConvert.DeserializeObject<Recepcao>(retornoObject.ToString());
            return retorno;
        }

        public async Task<Cotacao> GetCotacao()
        {
            var retornoObject = await new AgriconService().CallApiMethodGet("PrecoAgricolas/1");
            var retorno = JsonConvert.DeserializeObject<Cotacao>(retornoObject.ToString());
            return retorno;
        }

        public async Task<Produtor> DoLogin(Produtor produtor)
        {
            string content = JsonConvert.SerializeObject(produtor);
            var retornoObject = await new AgriconService().CallApiMethodPost("login",content);
            var retorno = JsonConvert.DeserializeObject<Produtor>(retornoObject.ToString());
            return retorno;
        }

        public async Task<ConsultaCustoPost> PostConsultaCusto(ConsultaCustoPost consulta)
        {
            string content = JsonConvert.SerializeObject(consulta);
            var retornoObject = await new AgriconService().CallApiMethodPost("ResultadoSimulacaos", content);
            var retorno = JsonConvert.DeserializeObject<ConsultaCustoPost>(retornoObject.ToString());
            return retorno;
        }

        public async Task<List<ResultadoSimulacao>> GetResultadoSimulacoes()
        {
            var retornoObject = await new AgriconService().CallApiMethodGet("ResultadoSimulacaos");
            var retorno = JsonConvert.DeserializeObject<List<ResultadoSimulacao>>(retornoObject.ToString());
          
            return retorno.OrderByDescending(x => x.Id).Take(5).ToList();
        }
        public async Task<string> GetSafraById(int id)
        {
            var retornoObject = await new AgriconService().CallApiMethodGet("safras/" + id);
            var retorno = JsonConvert.DeserializeObject<Safra>(retornoObject.ToString());
            return retorno.Descricao;
        }


        #endregion




        #region Metodos Padrão
        public async Task<object> CallApiMethodGet(string path)
        {


            HttpResponseMessage requisicao = null;
            try
            {
                requisicao = await httpClient.GetAsync(path);
            }
            catch
            {
                try
                {
                    Thread.Sleep(50);

                    requisicao = await httpClient.GetAsync(path);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine(ex.StackTrace);

                    return null;
                }
            }


            var result = await requisicao.Content.ReadAsStringAsync();
            return result;
        }

        public async Task<object> CallApiMethodPost(string path, string contentString, string mediaType = "application/json")
        {

            HttpResponseMessage requisicao = null;

            HttpContent content = new StringContent(contentString, Encoding.UTF8, mediaType);
            try
            {

                requisicao = await httpClient.PostAsync(path, content);
            }
            catch
            {
                try
                {
                    Thread.Sleep(50);
                    requisicao = await httpClient.PostAsync(path, content);
                }
                catch (Exception ex)
                {


                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);

                    return null;
                }
            }


            var result = await requisicao.Content.ReadAsStringAsync();

            return result;
        }
#endregion 

    }
}
