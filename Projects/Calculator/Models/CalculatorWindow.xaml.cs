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
            Calculator.ClearAll(DisplayText);
            MessageBox.Show($"left{Calculator.LeftNumber}op{Calculator.Operator}right{Calculator.RightNumber}result{Calculator.Result}");
        }

        private void DigitClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                double number = double.Parse(button.Content.ToString()!);

                if (Calculator.LeftNumber == null) Calculator.LeftNumber = "0";

                if (!operatorUsed)
                {
                    if (Calculator.LeftNumber == "0" && number.ToString() != ".")
                    {
                        Calculator.LeftNumber = number.ToString();
                    }
                    else
                    {
                        Calculator.LeftNumber += number.ToString();
                    }
                    Calculator.UpdateDisplayText(DisplayText, Calculator.LeftNumber);
                }
                else
                {
                    if (Calculator.RightNumber == "0" && number.ToString() != ".")
                    {
                        Calculator.RightNumber = number.ToString();
                    }
                    else
                    {
                        Calculator.RightNumber += number.ToString();
                    }
                    Calculator.UpdateDisplayText(DisplayText, Calculator.RightNumber); 
                }
            }
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string buttonContent = button.Content.ToString()!;
                Calculator.Operator = buttonContent;
            }
            operatorUsed = true;
        }
        private void FunctionClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    case "C":Calculator.Clear(DisplayText); break;

                    case "CE": Calculator.ClearAll(DisplayText); break;

                    case "+/-":
                        // Code for the "+/-" case
                        break;

                    case "⌫": Calculator.Backspace(DisplayText); break;

                    case "=": Calculator.PressCalculate(DisplayText); break;
                }
            }
        }
    }
}
