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
                _exchangeRates = await _currencyService.GetExchangeRatesAsync();
                string[] currencies = _exchangeRates.Keys.ToArray();
                FromCurrencyComboBox.ItemsSource = currencies;
                ToCurrencyComboBox.ItemsSource = currencies;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading currencies: {e.Message}");
            }
        }
        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string targetCurrency = ToCurrencyComboBox.SelectedItem.ToString();
                string baseCurrency = FromCurrencyComboBox.SelectedItem.ToString();

                double? amount = double.Parse(AmountTextBox.Text);

                double baseToTargetRate = _exchangeRates[targetCurrency];
                double targetToBaseRate = _exchangeRates[baseCurrency];

                if (amount.HasValue && !string.IsNullOrEmpty(targetCurrency) && !string.IsNullOrEmpty(baseCurrency))
                {
                    double? convertedAmount = (baseToTargetRate / targetToBaseRate) * amount;
                    txtResult.Text = $"Converted Amount: {amount} {baseCurrency} is {convertedAmount: 0.00} {targetCurrency}";
                }
            }
            catch { }
        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
