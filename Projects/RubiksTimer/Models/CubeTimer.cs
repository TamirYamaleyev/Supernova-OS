using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameCenterProject.Projects.RubiksTimer.Models
{
    class CubeTimer : INotifyPropertyChanged
    {
        public DispatcherTimer DTimer;
        private int _milliseconds;
        private bool timerRan = false;
        public int Milliseconds
        {
            get { return _milliseconds; }
            set 
            {
                if (_milliseconds != value)
                {
                    _milliseconds = value;
                    OnPropertyChanged(nameof(Milliseconds));
                }   
            } 
        }
        private int _seconds;
        public int Seconds
        {
            get { return _seconds; }
            set
            {
                if (_seconds != value)
                {
                    _seconds = value;
                    OnPropertyChanged(nameof(Seconds));
                }
            }
        }
        private int _minutes;
        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes != value)
                {
                    _minutes = value;
                    OnPropertyChanged(nameof(Minutes));
                }
            }
        }
        public CubeTimer() // Constructor
        {
            DTimer = new DispatcherTimer();
            DTimer.Interval = TimeSpan.FromMilliseconds(1);
            DTimer.Tick += DTimer_Tick!;
        }

        public void DTimer_Tick(object sender, EventArgs e)
        {
            // Updates timer value
            Milliseconds += 10;

            if (Milliseconds >= 1000)
            {
                Milliseconds = 0;
                Seconds++;

                if (Seconds >= 60)
                {
                    Seconds = 0;
                    Minutes++;
                }
            }
        }
        public void StartTimer()
        {
            if (timerRan)
            {
                RestartTimer();
            }
            DTimer.Start();
        }
        public void StopTimer()
        { 
            DTimer.Stop();
            timerRan = true;

            // Add time to history
        }
        public void RestartTimer()
        {
            Milliseconds = 0;
            Seconds = 0;
            Minutes = 0;

            // Reroll scramble
        }

        // Handles updating the timer UI text
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
