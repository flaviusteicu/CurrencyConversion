using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConversion
{
    public class ExchangeRatesService : IExchangeRateService
    {

        private readonly HttpClient httpClient;
        private readonly string apiBaseURL = "https://prime.exchangerate-api.com/v5/44bd5608b9f540b33553a26b/latest";

        public ExchangeRatesService()
        {
            httpClient = new HttpClient();
        }

        public async Task<decimal> Exchange(CurrenciesEnum fromCurrency, CurrenciesEnum toCurrency)
        {
            try
            {
                var URLString = $"{apiBaseURL}/{fromCurrency}";

                var response = await httpClient.GetAsync(URLString);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                var exchangeResponse = JsonConvert.DeserializeObject<ExchangeResponse>(responseBody);

                return exchangeResponse.conversion_rates.GetRate(toCurrency);
            }
            catch (Exception e)
            {
                //Log
                throw;
            }
        }
    }
}
