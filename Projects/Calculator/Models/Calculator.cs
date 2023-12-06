using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace GameCenterProject.Projects.Calculator.Models
{
    public class Calculator
    {
        private static Calculator _instance;
        private Calculator() { }

        public static Calculator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Calculator();
                }
                return _instance;
            }
        }

        public Label ResultLabel;

        public static bool firstPress = true;
        private static string _leftNumber { get; set; }
        public static string LeftNumber { get { return _leftNumber; } set { _leftNumber = value; } }
        private static string _rightNumber { get; set; }
        public static string RightNumber { get { return _rightNumber;} set { _rightNumber = value; } }
        private static string _operator { get; set; }
        public static string Operator { get { return _operator; } set { _operator = value; } }
        private static string _result { get; set; }
        public static string Result { get { return _result; } set {  _result = value; } }

        public static double Add()
        {
            return double.Parse(LeftNumber) + double.Parse(RightNumber);
        }
        public static double Subtract()
        {
            return double.Parse(LeftNumber) - double.Parse(RightNumber);
        }
        public static double Multiply()
        {
            return double.Parse(LeftNumber) * double.Parse(RightNumber);
        }
        public static double Divide()
        {
            return double.Parse(LeftNumber) / double.Parse(RightNumber);
        }

        public static void Backspace(Label label)
        {
            if (Operator == "")
            {
                if (LeftNumber.Length == 1 || LeftNumber.Length == 0) LeftNumber = "0";
                if (LeftNumber.Length > 1)
                {
                    LeftNumber = LeftNumber.Remove(LeftNumber.Length - 1);
                }
                UpdateDisplayText(label, LeftNumber);
            }
            else
            {
                if (RightNumber.Length == 1 || RightNumber.Length == 0) RightNumber = "0";
                if (RightNumber.Length > 1)
                {
                    RightNumber = RightNumber.Remove(RightNumber.Length - 1);
                }
                UpdateDisplayText(label, RightNumber);
            }
        }
        public static void Clear(Label label)
        {
            if (Operator == null)
            {
                LeftNumber = "0";
                UpdateDisplayText(label, LeftNumber);
            }
            else
            {
                RightNumber = "0";
                UpdateDisplayText(label, RightNumber);
            }
        }
        public static void ClearAll(Label label)
        {
            LeftNumber = "0";
            RightNumber = "";
            Operator = "";
            UpdateDisplayText(label, LeftNumber);
        }

        public static void PressCalculate(Label label)
        {
            if (RightNumber == "") RightNumber = LeftNumber;
            switch (Operator)
            {
                case "+": Result = Add().ToString(); break;
                case "-": Result = Subtract().ToString(); break;
                case "x": Result = Multiply().ToString(); break;
                case "÷": Result = Divide().ToString(); break;
                default: Result = LeftNumber; break;
            }
            Operator = "";
            LeftNumber = Result;
            UpdateDisplayText(label, LeftNumber);
            RightNumber = "";
            Operator = "";
        }

        public static void UpdateDisplayText(Label label, string valueToDisplay)
        {
            label.Content = valueToDisplay;
        }
    }
}
