using System;
using System.Data;
using System.Windows;

namespace CurrencyConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var rateService = new ExchangeRatesService();

            var exchangedAmmount = await rateService.Exchange(CurrenciesEnum.CHF, CurrenciesEnum.RON);

            Console.WriteLine(exchangedAmmount);
        }
    }
}
