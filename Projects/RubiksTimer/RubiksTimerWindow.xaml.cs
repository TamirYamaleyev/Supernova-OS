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
using System.Windows.Threading;

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

        private DispatcherTimer colorTimer;
        private int colorIndex;
        private Color[] allColors;
        public RubiksTimerWindow()
        {
            InitializeComponent();

            InitializeColors();

            colorTimer = new DispatcherTimer();
            colorTimer.Interval = TimeSpan.FromMilliseconds(5);
            colorTimer.Tick += ColorTimer_Tick!;

            colorIndex = 0;
            StartColorCycle();

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
            HistoryItem newHistory = new HistoryItem(timerInstance.Milliseconds, timerInstance.Seconds, timerInstance.Minutes, HistoryListSP, historyCount);
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

        private void InitializeColors()
        {
            int colorCount = 256;
            allColors = new Color[colorCount];

            for (int i = 0; i < colorCount; i++)
            {
                double hue = (i / (double)colorCount) * 360;
                Color color = ColorFromHSL(hue, 0.2, 0.4);
                allColors[i] = color;
            }
        }
        private void StartColorCycle()
        {
            colorTimer.Start();
        }
        private void StopColorCycle()
        {
            colorTimer.Stop();
        }
        private void ColorTimer_Tick (object sender, EventArgs e)
        {
            ScrambleTextElement.Foreground = new SolidColorBrush(allColors[colorIndex]);
            colorIndex = (colorIndex +1) % allColors.Length;
        }
        private Color ColorFromHSL(double hue, double saturation, double lightness)
        {
            double chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            double huePrime = hue / 60;
            double x = chroma * (1 - Math.Abs(huePrime % 2 - 1));

            double red = 0, green = 0, blue = 0;

            if (huePrime >= 0 && huePrime < 1)
            {
                red = chroma;
                green = x;
            }
            else if (huePrime >= 1 && huePrime < 2)
            {
                red = x;
                green = chroma;
            }
            else if (huePrime >= 2 && huePrime < 3)
            {
                green = chroma;
                blue = x;
            }
            else if (huePrime >= 3 && huePrime < 4)
            {
                green = x;
                blue = chroma;
            }
            else if (huePrime >= 4 && huePrime < 5)
            {
                red = x;
                blue = chroma;
            }
            else if (huePrime >= 5 && huePrime < 6)
            {
                red = chroma;
                blue = x;
            }

            double m = lightness - chroma / 2;
            red += m;
            green += m;
            blue += m;

            byte byteRed = (byte)(red * 255);
            byte byteGreen = (byte)(green * 255);
            byte byteBlue = (byte)(blue * 255);

            return Color.FromRgb(byteRed, byteGreen, byteBlue);
        }
    }
}
