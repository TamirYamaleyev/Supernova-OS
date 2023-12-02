using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenterProject.Projects.Calculator.Models
{
    /// <summary>
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        public bool operatorUsed = false;
        public CalculatorWindow()
        {
            InitializeComponent();
            Calculator calculator = Calculator.Instance;
        }

        private void DigitClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                double number = double.Parse(button.Content.ToString()!);

                if (!operatorUsed)
                {
                    Calculator.LeftNumber += number.ToString();

                    //Test
                    MessageBox.Show($"LeftNumber = {Calculator.LeftNumber}\nRightNumber: {Calculator.RightNumber}\nOperator: {Calculator.Operator}\nResult: {Calculator.Result}");
                }
                else
                {
                    Calculator.RightNumber += number.ToString();

                    //Test
                    MessageBox.Show($"LeftNumber = {Calculator.LeftNumber}\nRightNumber: {Calculator.RightNumber}\nOperator: {Calculator.Operator}\nResult: {Calculator.Result}");
                }
            }
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string buttonContent = button.Content.ToString()!;

                Calculator.Operator = buttonContent;

                MessageBox.Show($"LeftNumber = {Calculator.LeftNumber}\nRightNumber: {Calculator.RightNumber}\nOperator: {Calculator.Operator}\nResult: {Calculator.Result}");
            }
            operatorUsed = true;
        }
        private void FunctionClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    case "C":Calculator.Clear(); break;

                    case "CE": Calculator.ClearAll(); break;

                    case "+/-":
                        // Code for the "+/-" case
                        break;

                    case "⌫": Calculator.Backspace(); break;

                    case "=": Calculator.PressCalculate(ResultValue); break;
                }
            }
        }
    }
}
