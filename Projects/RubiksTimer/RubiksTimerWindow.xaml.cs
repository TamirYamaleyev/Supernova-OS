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
        public RubiksTimerWindow()
        {
            InitializeComponent();
            timerInstance = new CubeTimer();

            DataContext = timerInstance;
            this.KeyDown += OnKeyDownHandler;
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && !isCounting)
            {
                    isCounting = true;
                    timerInstance.StartTimer();
            }
            else if (e.Key == Key.Space && isCounting)
            {
                isCounting = false;
                timerInstance.StopTimer();
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
