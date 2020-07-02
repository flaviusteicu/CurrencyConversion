namespace CurrencyConversion.Models
{
    // Container class
    public class ExchangeCardDetails
    {
        public decimal? FromAmmount { get; set; }
        public decimal? ToAmmount { get; set; }
        public CurrenciesEnum ExchangeFrom { get; set; }
        public CurrenciesEnum ExchangeTo { get; set; }
    }
}
