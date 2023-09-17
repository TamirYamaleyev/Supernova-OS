using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GameCenterProject.Projects.RubiksTimer.Models
{
    class HistoryItem
    {
        public int Milliseconds { get; set; }
        public int Seconds { get; set; }
        public int Minutes { get; set; }
        public Scramble CurrScramble { get; set; }

        public HistoryItem(int milli, int sec, int min , StackPanel countSP, StackPanel historySP, int count)
        {
            Milliseconds = milli;
            Seconds = sec;
            Minutes = min;
            //CurrScramble = scr;

            TextBlock countText = new TextBlock();
            countText.Text = $"{count:D2})";
            countText.FontSize = 32;
            countText.FontWeight = FontWeights.Bold;

            countSP.Children.Add(countText);

            TextBlock timeText = new TextBlock();
            timeText.Text = $" {Minutes:D2}:{Seconds:D2}.{Milliseconds/10:D2}";
            timeText.FontSize = 32;

            historySP.Children.Add(timeText);
        }
    }
}
