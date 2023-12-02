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

        public static void Backspace()
        {
            if (Operator == null)
            {
                if (LeftNumber.Length > 0)
                {
                    LeftNumber = LeftNumber.Remove(LeftNumber.Length - 1);
                }
            }
            else
            {
                if (RightNumber.Length > 0)
                {
                    RightNumber = RightNumber.Remove(RightNumber.Length - 1);
                }
            }
        }
        public static void Clear()
        {
            if (Operator == null)
            {
                LeftNumber = "";
            }
            else
            {
                RightNumber = "";
            }
        }
        public static void ClearAll()
        {
            LeftNumber = "";
            RightNumber = "";
            Operator = "";
        }

        public static void PressCalculate(Label label)
        {
            switch (Operator)
            {
                case "+": Result = Add().ToString(); break;
                case "-": Result = Subtract().ToString(); break;
                case "x": Result = Multiply().ToString(); break;
                case "÷": Result = Divide().ToString(); break;
            }

            MessageBox.Show($"LeftNumber = {LeftNumber}\nRightNumber: {RightNumber}\nOperator: {Operator}\nResult: {Result}");
            Operator = "";
            label.Content = Result;
        }
    }
}
