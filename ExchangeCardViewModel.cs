using CurrencyConversion.Models;
using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CurrencyConversion
{
    public class ExchangeCardViewModel : INotifyPropertyChanged
    {
        private readonly IExchangeRatesService exchangeRateService;

        private decimal calculatedRate;

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
            ExchangeToCurrency = exchangeCardDetails.ExhangeTo;
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

        private decimal ammountToExchange;
        public decimal AmmountToExchange
        {
            get => ammountToExchange;
            set
            {
                ammountToExchange = value;
                OnPropertyChanged();
            }
        }

        private decimal exchangedAmmount;
        public decimal ExchangedAmmount
        {
            get => exchangedAmmount;
            set
            {
                exchangedAmmount = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand SwitchCommand
        {
            get
            {
                return new DelegateCommand<object>(Switch, CanSwitch);
            }
        }

        private void Switch(object context)
        {
            AmmountToExchange = 0;
            ExchangedAmmount = 0;

            var newExchangeTo = ExchangeFromCurrency;

            ExchangeFromCurrency = ExchangeToCurrency;
            ExchangeToCurrency = newExchangeTo;
        }

        private bool CanSwitch(object context)
        {
            return true;
        }

        public async void OnFromAmmountTextChanged()
        {
            calculatedRate = await exchangeRateService.Exchange(ExchangeFromCurrency, ExchangeToCurrency);

            ExchangedAmmount = calculatedRate * AmmountToExchange;
        }


        public async void RefreshRate()
        {
            calculatedRate = await exchangeRateService.Exchange(ExchangeFromCurrency, ExchangeToCurrency);

            ExchangedAmmount = calculatedRate * AmmountToExchange;

            Rate = $"1 {ExchangeFromCurrency} = {calculatedRate} {ExchangeToCurrency}";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
