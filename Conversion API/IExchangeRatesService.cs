using CurrencyConversion.Models;
using System.Threading.Tasks;

namespace CurrencyConversion
{
    public interface IExchangeRatesService
    {
        Task<decimal> Exchange(CurrenciesEnum fromCurrency, CurrenciesEnum toCurrency);
    }
}
