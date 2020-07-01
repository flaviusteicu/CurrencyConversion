using CurrencyConversion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

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

        private void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            var exchangeCardsToSave = new List<ExchangeCardDetails>();

            foreach (var card in CurrencyCardsBox.Items)
            {
                var exchangeCard = card as ExchangeCard;
                var exchangeCardVM = exchangeCard.DataContext as ExchangeCardViewModel;
                var exchangeCardDetails = new ExchangeCardDetails()
                {
                    ExchangeFrom = exchangeCardVM.ExchangeFromCurrency,
                    ExhangeTo = exchangeCardVM.ExchangeToCurrency,
                    FromAmmount = exchangeCardVM.AmmountToExchange,
                    ToAmmount = exchangeCardVM.ExchangedAmmount
                };
                exchangeCardsToSave.Add(exchangeCardDetails);
            }
            SaveCardsToDisk(path, exchangeCardsToSave);
        }

        private void SaveCardsToDisk(string path, List<ExchangeCardDetails> exchangeCardDetails)
        {
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, exchangeCardDetails);
            }
        }

    }
}
