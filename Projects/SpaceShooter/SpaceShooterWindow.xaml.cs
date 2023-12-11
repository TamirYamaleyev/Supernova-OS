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

using System.Windows.Threading;

namespace GameCenterProject.Projects.SpaceShooter
{
    /// <summary>
    /// Interaction logic for SpaceShooterWindow.xaml
    /// </summary>
    public partial class SpaceShooterWindow : Window
    {
        double windowWidth = 0;

        DispatcherTimer gameTimer = new DispatcherTimer();
        bool moveLeft, moveRight;
        List<Rectangle> itemRemover = new List<Rectangle>();

        Random rand = new Random();

        string imagePath = "C:\\Users\\Wraithling\\source\\repos\\GameCenterProject\\Projects\\SpaceShooter\\Images\\";

        int enemySpriteCounter = 0;
        int enemyCounter = 100;
        int playerSpeed = 10;
        int limit = 50;
        int score = 0;
        int damage = 0;
        int enemySpeed = 10;

        Rect playerHitBox;
        public SpaceShooterWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            GameCanvas.Focus();
            Loaded += GameWindow_Loaded;

            ImageBrush bg = new ImageBrush();

            bg.ImageSource = new BitmapImage(new Uri($"{imagePath}purple.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            GameCanvas.Background = bg;

            ImageBrush playerSprite = new ImageBrush();

            playerSprite.ImageSource = new BitmapImage(new Uri($"{imagePath}player.png"));
            player.Fill = playerSprite;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            enemyCounter -= 1;

            scoreText.Content = "Score: " + score;
            damageText.Content = "Damage " + damage;

            if (enemyCounter < 0)
            {
                SpawnEnemies();
                enemyCounter = limit;
            }

            if (moveLeft && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight && Canvas.GetLeft(player) + 90 < windowWidth)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }

            foreach (var entity in GameCanvas.Children.OfType<Rectangle>())
            {
                if (entity is Rectangle && (string)entity.Tag == "bullet")
                {
                    Canvas.SetTop(entity, Canvas.GetTop(entity) - 20);

                    Rect bulletHitBox = new Rect(Canvas.GetLeft(entity), Canvas.GetTop(entity), entity.Width, entity.Height);

                    if (Canvas.GetTop(entity) < 10) itemRemover.Add(entity);

                    foreach (var y in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            Rect enemyHit = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                            if (bulletHitBox.IntersectsWith(enemyHit))
                            {
                                itemRemover.Add(entity);
                                itemRemover.Add(y);
                                score += 5;
                            }
                        }
                    }
                }
                if (entity is Rectangle && (string)entity.Tag == "enemy")
                {
                    Canvas.SetTop(entity, Canvas.GetTop(entity) + enemySpeed);

                    if (Canvas.GetTop(entity) > 750)
                    {
                        itemRemover.Add(entity);
                        damage += 10;
                    }
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(entity), Canvas.GetTop(entity), entity.Width, entity.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        itemRemover.Add(entity);
                        damage += 5;
                    }
                }
            }
            foreach (Rectangle entityToRemove in itemRemover)
            {
                GameCanvas.Children.Remove(entityToRemove);
            }

            if (score > 25)
            {
                limit = 20;
                enemySpeed = 15;
            }
            if (damage > 99)
            {
                gameTimer.Stop();
                damageText.Content = "Damage: 100";
                damageText.Foreground = Brushes.Red;
                MessageBox.Show($"Captain, Your Score was: {score}!\nPress OK to Play Again", "Game Over!");

                RestartGame();

            }

        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: moveLeft = true; break;
                case Key.Right: moveRight = true; break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left: moveLeft = false; break;
                case Key.Right: moveRight = false; break;
                case Key.Space:
                    {
                        Rectangle newBullet = new Rectangle
                        {
                            Tag = "bullet",
                            Height = 20,
                            Width = 5,
                            Fill = Brushes.White,
                            Stroke = Brushes.Red,
                        };
                        Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                        Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);

                        GameCanvas.Children.Add(newBullet);
                    }
                    break;
            }
        }
        
        private void SpawnEnemies()
        {
            ImageBrush enemySprite = new ImageBrush();
            enemySpriteCounter = rand.Next(1, 5);
            
            switch(enemySpriteCounter)
            {
                case 1: enemySprite.ImageSource = new BitmapImage(new Uri($"{imagePath}1.png")); break;
                case 2: enemySprite.ImageSource = new BitmapImage(new Uri($"{imagePath}2.png")); break;
                case 3: enemySprite.ImageSource = new BitmapImage(new Uri($"{imagePath}3.png")); break;
                case 4: enemySprite.ImageSource = new BitmapImage(new Uri($"{imagePath}4.png")); break;
                case 5: enemySprite.ImageSource = new BitmapImage(new Uri($"{imagePath}5.png")); break;
            }
            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 50,
                Width = 56,
                Fill = enemySprite,
            };
            Canvas.SetTop(newEnemy, -100);
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));
            GameCanvas.Children.Add(newEnemy);
        }
        private void RestartGame()
        {
            enemySpriteCounter = 0;
            enemyCounter = 100;
            playerSpeed = 10;
            limit = 50;
            score = 0;
            damage = 0;
            enemySpeed = 10;

            foreach (var enemy in GameCanvas.Children.OfType<Rectangle>())
            {
                if (enemy is Rectangle && (string)enemy.Tag == "enemy") GameCanvas.Children.Remove(enemy);
            }
        }
        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            windowWidth = ActualWidth;
        }
    }
}
