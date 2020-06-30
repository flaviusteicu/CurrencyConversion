using System.Threading.Tasks;

namespace CurrencyConversion
{
    public interface IExchangeRateService
    {
        Task<decimal> Exchange(CurrenciesEnum fromCurrency, CurrenciesEnum toCurrency);
    }
}
