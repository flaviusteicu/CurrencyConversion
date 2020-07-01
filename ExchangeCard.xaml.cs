using CurrencyConversion.Models;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyConversion
{
    /// <summary>
    /// Interaction logic for ExchangeCard.xaml
    /// </summary>
    public partial class ExchangeCard : UserControl
    {
        public ExchangeCard()
        {
            this.DataContext = new ExchangeCardViewModel();
            InitializeComponent();
        }

        public ExchangeCard(ExchangeCardDetails exchangeCard)
        {

            this.DataContext = new ExchangeCardViewModel(exchangeCard);
            InitializeComponent();
        }


        private void AmmountToExchangeTextChanged(object sender, TextChangedEventArgs e)
        {
            ((this.DataContext) as ExchangeCardViewModel).OnFromAmmountTextChanged();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
