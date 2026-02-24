using ComunicacaoVisual.Client.Models;
using System.Net.Http.Json;

namespace ComunicacaoVisual.Client.Services
{
    public class MonitoramentoGlobalService
    {
        private readonly HttpClient _http;

        public MonitoramentoGlobalService(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<VwMonitoramentoGlobalModel>> GetMonitoramentoGlobalAsync()
        {
            var resultado = await _http.GetFromJsonAsync<List<VwMonitoramentoGlobalModel>>
                ("api/monitoramentoglobal");

            return resultado ?? new List<VwMonitoramentoGlobalModel>();
        }
    }
}