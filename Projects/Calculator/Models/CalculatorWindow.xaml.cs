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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Calculator.ClearAll(DisplayText, DisplayStory);
        }

        private void DigitClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                double number = double.Parse(button.Content.ToString()!);

                if (!operatorUsed)
                {
                    if (Calculator.LeftNumber == "0")
                    {
                        Calculator.LeftNumber = number.ToString();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder(Calculator.LeftNumber);
                        sb.Append(number.ToString());
                        Calculator.LeftNumber = sb.ToString();
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
                        StringBuilder sb = new StringBuilder(Calculator.RightNumber);
                        sb.Append(number.ToString());
                        Calculator.RightNumber = sb.ToString();
                    }
                    Calculator.UpdateDisplayText(DisplayText, Calculator.RightNumber); 
                }
            }
            Calculator.UpdateDisplayStory(DisplayStory);
        }

        private void OperatorClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string buttonContent = button.Content.ToString()!;
                Calculator.Operator = buttonContent;
            }
            operatorUsed = true;
            Calculator.UpdateDisplayStory(DisplayStory);
        }

        private void FunctionClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                switch (button.Content.ToString())
                {
                    case "C":Calculator.Clear(DisplayText, DisplayStory); break;
                    case "CE":
                        {
                            Calculator.ClearAll(DisplayText, DisplayStory);
                            operatorUsed = false;
                            break;
                        }
                    case "⌫": Calculator.Backspace(DisplayText, DisplayStory); break;
                    case "=": Calculator.PressCalculate(DisplayText, DisplayStory); break;
                }
            }
        }

        private void DecimalClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (!operatorUsed && !Calculator.leftDecimalUsed)
                {
                    StringBuilder sb = new StringBuilder(Calculator.LeftNumber);
                    sb.Append(".");
                    Calculator.LeftNumber = sb.ToString();
                    Calculator.UpdateDisplayText(DisplayText, Calculator.LeftNumber);
                    Calculator.leftDecimalUsed = true;
                }
                else if (operatorUsed && !Calculator.rightDecimalUsed)
                {
                    StringBuilder sb = new StringBuilder(Calculator.RightNumber);
                    sb.Append(".");
                    Calculator.RightNumber = sb.ToString();
                    Calculator.UpdateDisplayText(DisplayText, Calculator.RightNumber);
                    Calculator.rightDecimalUsed = true;
                }
            }
            Calculator.UpdateDisplayStory(DisplayStory);
        }
    }
}
