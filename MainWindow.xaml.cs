using CurrencyConversion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CurrencyConversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "savedCards.json");

        public MainWindow()
        {
            InitializeComponent();
            LoadSavedExchangeCards(path);

            if (CurrencyCardsBox.Items.Count == 0)
            {
                CurrencyCardsBox.Items.Add(new ExchangeCard());
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            CurrencyCardsBox.Items.Add(new ExchangeCard());
        }

        private void LoadSavedExchangeCards(string path)
        {
            List<ExchangeCardDetails> cards = new List<ExchangeCardDetails>();

            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    cards = (List<ExchangeCardDetails>)serializer.Deserialize(file, typeof(List<ExchangeCardDetails>));
                }

                foreach (var card in cards)
                {
                    CurrencyCardsBox.Items.Add(new ExchangeCard(card));
                }
            }
        }

        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            var exchangeCardsToSave = new List<ExchangeCardDetails>();

            foreach (var card in CurrencyCardsBox.Items)
            {
                var exchangeCard = card as ExchangeCard;
                var exchangeCardVM = exchangeCard.DataContext as ExchangeCardViewModel;
                var exchangeCardDetails = new ExchangeCardDetails()
                {
                    ExchangeFrom = exchangeCardVM.ExchangeFromCurrency,
                    ExchangeTo = exchangeCardVM.ExchangeToCurrency,
                    FromAmmount = exchangeCardVM.AmmountToExchange,
                    ToAmmount = exchangeCardVM.ExchangedAmmount
                };
                exchangeCardsToSave.Add(exchangeCardDetails);
            }
            SuccesfulSaveTextBlock.Visibility = Visibility.Hidden;
            SaveProgressBar.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            SaveCardsToDisk(path, exchangeCardsToSave);
            SaveProgressBar.Visibility = Visibility.Hidden;
            SuccesfulSaveTextBlock.Visibility = Visibility.Visible;
        }

        private void SaveCardsToDisk(string path, List<ExchangeCardDetails> exchangeCardDetails)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, exchangeCardDetails);
            }
        }

        private void CurrencyCardsBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                CurrencyCardsBox.Items.Remove((sender as ListBox).SelectedItem);
                SaveButton.Focus();
            }
        }
    }
}
