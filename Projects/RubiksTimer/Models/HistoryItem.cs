using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameCenterProject.Projects.RubiksTimer.Models
{
    class HistoryItem
    {
        public int Milliseconds { get; set; }
        public int Seconds { get; set; }
        public int Minutes { get; set; }
        public Scramble currScramble { get; set; }

        public HistoryItem(int milli, int sec, int min , StackPanel sp)
        {
            Milliseconds = milli;
            Seconds = sec;
            Minutes = min;
            //currScramble = scr;

            TextBlock timeText = new TextBlock();
            timeText.Text = $"{Minutes}:{Seconds}:{Milliseconds}";
            timeText.FontSize = 16;

            sp.Children.Add(timeText);
        }
    }
}
