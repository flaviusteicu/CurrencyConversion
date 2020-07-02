using CurrencyConversion.Models;
using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CurrencyConversion
{

    //TODO: Further development could include:
    //
    //              - a sort of watchdog with an auto-exchange functionality for when a certain currency reaches a desired exchange rate
    //              - more currencies (ex crypto)
    //              - having a balance (linking an account - this opens up a lot of opportunities)
    //              - a login mechanism for security reasons
    //              Banking operations such as:
    //              - donations 
    //              - bank transfer
    //              - stock market investments


    public class ExchangeCardViewModel : INotifyPropertyChanged
    {
        private readonly IExchangeRatesService exchangeRateService;

        private decimal? calculatedRate = null;


        public event PropertyChangedEventHandler PropertyChanged;

        public ExchangeCardViewModel()
        {
            // It could be injected
            exchangeRateService = new ExchangeRatesService();
        }

        public ExchangeCardViewModel(ExchangeCardDetails exchangeCardDetails)
        {
            exchangeRateService = new ExchangeRatesService();

            ExchangeFromCurrency = exchangeCardDetails.ExchangeFrom;
            ExchangeToCurrency = exchangeCardDetails.ExchangeTo;
            ExchangedAmmount = exchangeCardDetails.ToAmmount;
            AmmountToExchange = exchangeCardDetails.FromAmmount;
        }


        private CurrenciesEnum exchangeFromCurrency;
        public CurrenciesEnum ExchangeFromCurrency
        {
            get => exchangeFromCurrency;
            set
            {
                exchangeFromCurrency = value;
                RefreshRate();
                OnPropertyChanged();
            }
        }

        private CurrenciesEnum exchangeToCurrency = CurrenciesEnum.ARS;
        public CurrenciesEnum ExchangeToCurrency
        {
            get => exchangeToCurrency;
            set
            {
                exchangeToCurrency = value;
                RefreshRate();

                OnPropertyChanged();
            }
        }

        private string rate;
        public string Rate
        {
            get => rate;
            set
            {
                rate = value;
                IsRateVisible = true;
                OnPropertyChanged();
            }
        }

        private bool isRateVisible = false;
        public bool IsRateVisible
        {
            get => isRateVisible;
            set
            {
                isRateVisible = value;
                OnPropertyChanged();
            }
        }

        private decimal? ammountToExchange = null;
        public decimal? AmmountToExchange
        {
            get => ammountToExchange;
            set
            {
                OnFromAmmountTextChanged();
                ammountToExchange = value;
                OnPropertyChanged();
            }
        }

        private decimal? exchangedAmmount = null;
        public decimal? ExchangedAmmount
        {
            get => exchangedAmmount;
            set
            {
                exchangedAmmount = value;
                OnPropertyChanged();
            }
        }

        private decimal? exchangedAmmountPreview = null;
        public decimal? ExchangedAmmountPreview
        {
            get => exchangedAmmountPreview;
            set
            {
                exchangedAmmountPreview = value;
                OnPropertyChanged();
            }
        }

        #region Exchange Command

        public ICommand ExchangeCommand
        {
            get
            {
                return new DelegateCommand<object>(Exchange, CanExchange);
            }
        }

        private void Exchange(object context)
        {
            RefreshRate();
            ExchangedAmmount = AmmountToExchange * calculatedRate;
        }

        private bool CanExchange(object context)
        {
            return true;
        }

        #endregion


        #region Switch Command
        public ICommand SwitchCommand
        {
            get
            {
                return new DelegateCommand<object>(Switch, CanSwitch);
            }
        }

        private void Switch(object context)
        {
            AmmountToExchange = null;
            ExchangedAmmount = null;

            var newExchangeTo = ExchangeFromCurrency;

            ExchangeFromCurrency = ExchangeToCurrency;
            ExchangeToCurrency = newExchangeTo;
        }

        private bool CanSwitch(object context)
        {
            return true;
        }

        #endregion


        // Called when the user inputs a number in the exchange box
        public async void OnFromAmmountTextChanged()
        {
            calculatedRate = await exchangeRateService.Exchange(ExchangeFromCurrency, ExchangeToCurrency);

            Rate = $"1 {ExchangeFromCurrency} = {calculatedRate} {ExchangeToCurrency}";

            ExchangedAmmountPreview = calculatedRate * AmmountToExchange;
        }

        // Refreshes the exchange rate based on user currency input
        public async void RefreshRate()
        {
            calculatedRate = await exchangeRateService.Exchange(ExchangeFromCurrency, ExchangeToCurrency);

            ExchangedAmmountPreview = calculatedRate * AmmountToExchange;

            Rate = $"1 {ExchangeFromCurrency} = {calculatedRate} {ExchangeToCurrency}";
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
