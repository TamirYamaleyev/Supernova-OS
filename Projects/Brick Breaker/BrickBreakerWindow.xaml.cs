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

namespace GameCenterProject.Projects.Brick_Breaker
{
    /// <summary>
    /// Interaction logic for BrickBreakerWindow.xaml
    /// </summary>
    public partial class BrickBreakerWindow : Window
    {
        public BrickBreakerWindow()
        {
            InitializeComponent();

            // Create the ball (Ellipse)
            Ellipse ball = new Ellipse
            {
                Width = 15,
                Height = 15,
                Fill = Brushes.Blue,
            };

            // Set the initial position of the ball
            Canvas.SetLeft(ball, 100);
            Canvas.SetTop(ball, 100);

            // Add the ball to the canvas
            canvas.Children.Add(ball);
        }
    }
}
