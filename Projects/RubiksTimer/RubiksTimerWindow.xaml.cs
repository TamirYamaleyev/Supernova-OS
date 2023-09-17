using GameCenterProject.Projects.RubiksTimer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameCenterProject.Projects.RubiksTimer
{
    /// <summary>
    /// Interaction logic for RubiksTimerWindow.xaml
    /// </summary>
    public partial class RubiksTimerWindow : Window
    {
        private CubeTimer timerInstance;
        public bool isCounting = false;
        private int historyCount = 1;
        public RubiksTimerWindow()
        {
            InitializeComponent();
            timerInstance = new CubeTimer();

            DataContext = timerInstance;
            this.KeyDown += OnKeyDownHandler;

            AddNewScrambleText();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !isCounting)
            {
                StartCubeTimer();
            }
            else if (e.Key == Key.Space && isCounting)
            {
                AddTime();
                AddNewScrambleText();
            }
        }
        private void StartCubeTimer()
        {
            isCounting = true;
            timerInstance.StartTimer();
        }
        private void AddTime()
        {
            isCounting = false;
            timerInstance.StopTimer();
            HistoryItem newHistory = new HistoryItem(timerInstance.Milliseconds, timerInstance.Seconds, timerInstance.Minutes, CountSP, HistoryListSP, historyCount);
            historyCount++;
        }
        private void AddNewScrambleText()
        {
            string ScrambleText = Scramble.GenerateScramble(20);
            ScrambleTextElement.Text = ScrambleText;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
