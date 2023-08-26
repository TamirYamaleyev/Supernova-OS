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

namespace GameCenterProject.Projects.The_2048
{
    /// <summary>
    /// Interaction logic for The2048Window.xaml
    /// </summary>
    public partial class The2048Window : Window
    {
        public The2048Window()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double minSize = Math.Min(e.NewSize.Width, e.NewSize.Height);
            mainGrid.Width = minSize;
            mainGrid.Height = minSize;
        }
    }
}
