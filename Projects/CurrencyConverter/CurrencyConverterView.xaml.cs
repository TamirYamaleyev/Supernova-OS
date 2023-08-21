using GameCenterProject.Projects.CurrencyConverter.Models;
using GameCenterProject.Projects.CurrencyConverter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenterProject.Projects.CurrencyConverter
{
    /// <summary>
    /// Interaction logic for CurrencyConverterView.xaml
    /// </summary>
    public partial class CurrencyConverterView : Window
    {
        private CurrencyService _currencyService;
        private Dictionary<string, double> _exchangeRates;

        public CurrencyConverterView()
        {
            InitializeComponent();
            _currencyService = new CurrencyService();
            LoadCurrencies();
        }

        private async void LoadCurrencies()
        {
            try
            {
                // Retrieve list of currencies from the API (GetExchangeRatesAsync())
                _exchangeRates = await _currencyService.GetExchangeRatesAsync();
                string[] currencies = _exchangeRates.Keys.ToArray();
                // Save the Dictionary values as an array of currencies
                // Set the dropdown menu keys to be each item in the array of currencies (ItemSource of ComboBox)
                FromCurrencyComboBox.ItemsSource = currencies;
                ToCurrencyComboBox.ItemsSource = currencies;
                // Error handling (try catch => MessageBox.Show(...) 
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading currencies: {e.Message}");
            }
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected currencies by the user
            string targetCurrency = ToCurrencyComboBox.SelectedItem.ToString();
            string baseCurrency = FromCurrencyComboBox.SelectedItem.ToString();
            // Get the Amount to convert
            double amount = double.Parse(AmountTextBox.Text);

            double baseToTargetRate = _exchangeRates[targetCurrency];
            double targetToBaseRate = _exchangeRates[baseCurrency];

            double convertedAmount = (baseToTargetRate / targetToBaseRate) * amount;

            txtResult.Text = $"Converted Amount: {amount} {baseCurrency} is {convertedAmount: 0.00} {targetCurrency}";


        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
