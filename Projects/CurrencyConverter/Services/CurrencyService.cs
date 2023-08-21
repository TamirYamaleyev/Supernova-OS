using GameCenterProject.Projects.CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameCenterProject.Projects.CurrencyConverter.Services
{
    internal class CurrencyService
    {
        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/latest";
        private const string ApiKey = "afbc42300b090ebc7fea5e84936d33ba";
        private HttpClient Http_Client = new HttpClient();

        public async Task<Dictionary<string, double>> GetExchangeRatesAsync()
        {
            // "access_key" is just part of the website URL
            string requestUrl = $"{BaseApiEndPoint}?access_key={ApiKey}";
            // Return only the body of the API call without all of the headers and cookies
            string response = await Http_Client.GetStringAsync(requestUrl);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Translate the JSON object into a C# object of the type ExchangeResponse
            ExchangeResponse exchangeData = JsonSerializer.Deserialize<ExchangeResponse>(response, options);

            if (exchangeData == null || exchangeData.Rates == null)
                throw new InvalidOperationException("Failed to fetch exchange rates.");

            return exchangeData.Rates;
        }
    }
}
