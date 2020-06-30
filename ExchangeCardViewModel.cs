using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CurrencyConversion
{
    public class ExchangeCardViewModel : INotifyPropertyChanged
    {
        private readonly IExchangeRatesService exchangeRateService;

        private decimal rate;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExchangeCardViewModel()
        {
            // It could be injected
            exchangeRateService = new ExchangeRatesService();
        }


        private CurrenciesEnum exchangeFromCurrency;
        public CurrenciesEnum ExchangeFromCurrency
        {
            get => exchangeFromCurrency;
            set
            {
                exchangeFromCurrency = value;
                RefreshDisplayRate();
                OnPropertyChanged();
            }
        }

        private CurrenciesEnum exchangeToCurrency;
        public CurrenciesEnum ExchangeToCurrency
        {
            get => exchangeToCurrency;
            set
            {
                exchangeToCurrency = value;
                RefreshDisplayRate();
                OnPropertyChanged();
            }
        }

        private string displayRate;
        public string DisplayRate
        {
            get => displayRate;
            set
            {
                displayRate = value;
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
            if(rate != 0)
            {
                ExchangedAmmount = AmmountToExchange * rate;
            }
            else
            {
                RefreshDisplayRate();
                ExchangedAmmount = AmmountToExchange * rate;
            }
            
        }

        private bool CanExchange(object context)
        {          
            return true;
        }

        public async void RefreshDisplayRate()
        {
            rate = await exchangeRateService.Exchange(ExchangeFromCurrency, ExchangeToCurrency);

            DisplayRate = $"1 {ExchangeFromCurrency} = {rate} {ExchangeToCurrency}";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
