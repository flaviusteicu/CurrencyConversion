namespace CurrencyConversion.Models
{
    public class ExchangeCardDetails
    {
        public decimal FromAmmount { get; set; }
        public decimal ToAmmount { get; set; }
        public CurrenciesEnum ExchangeFrom { get; set; }
        public CurrenciesEnum ExhangeTo { get; set; }
    }
}
