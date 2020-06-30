using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion
{
    public class ExchangeCardViewModel 
    {
        private readonly IExchangeRateService exchangeRateService;

        public ExchangeCardViewModel()
        {
            // It could be injected
            exchangeRateService = new ExchangeRatesService();
        }
    }
}
