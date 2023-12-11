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

        //Window
        int windowLeftClamp = 0;
        int windowRightClamp = 90;
        int defenseLineHeight = 650;
        int score = 0;
        int damage = 0;

        //Player
        int playerSpeed = 10;
        int damageThreshold = 99;
        int scoreOnKill = 5;
        int bulletSpeed = 20;
        int bulletHeight = 20;
        int bulletWidth = 5;
        int scoreThreshold = 25;
        Rect playerHitBox;

        //Enemy
        int damageOnMiss = 10;
        int damageOnTouch = 5;
        int limit = 50;
        int enemySpeed = 10;
        int difficultyIncreaseLimit = 20;
        int difficultyIncreaseSpeed = 15;
        int enemySpriteCounter = 0;
        int enemyCounter = 100;
        int enemyHeight = 50;
        int enemyWidth = 56;
        int enemySpawnY = -100;

        public SpaceShooterWindow()
        {
            InitializeComponent();

            gameTimer.Interval = TimeSpan.FromMilliseconds(16.67);
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            GameCanvas.Focus();
            Loaded += GameWindow_Loaded;

            InitializeBackgroundCanvas();
            InitializePlayer();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            enemyCounter -= 1;
            if (enemyCounter < 0)
            {
                SpawnEnemies();
                enemyCounter = limit;
            }
            // Player Movement
            if (moveLeft && Canvas.GetLeft(player) > windowLeftClamp)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (moveRight && Canvas.GetLeft(player) + windowRightClamp < windowWidth)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            foreach (var entity in GameCanvas.Children.OfType<Rectangle>())
            {
                if (entity is Rectangle && (string)entity.Tag == "bullet")
                {
                    Rect bulletHitBox = ShootBullet(entity);

                    // Release bullet from memory
                    if (Canvas.GetTop(entity) < -50) itemRemover.Add(entity);

                    foreach (var enemy in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if (enemy is Rectangle && (string)enemy.Tag == "enemy")
                        {
                            Rect enemySprite = new Rect(Canvas.GetLeft(enemy), Canvas.GetTop(enemy), enemy.Width, enemy.Height);
                            if (bulletHitBox.IntersectsWith(enemySprite))
                            {
                                EnemyHit(entity, enemy);
                            }
                        }
                    }
                }
                if (entity is Rectangle && (string)entity.Tag == "enemy")
                {
                    Canvas.SetTop(entity, Canvas.GetTop(entity) + enemySpeed);

                    if (Canvas.GetTop(entity) > defenseLineHeight)
                    {
                        DamagePlayer(entity, damageOnMiss);
                    }
                    Rect enemyHitBox = new Rect(Canvas.GetLeft(entity), Canvas.GetTop(entity), entity.Width, entity.Height);

                    if (playerHitBox.IntersectsWith(enemyHitBox))
                    {
                        DamagePlayer(entity, damageOnTouch);
                    }
                }
            }
            foreach (Rectangle entityToRemove in itemRemover)
            {
                GameCanvas.Children.Remove(entityToRemove);
            }

            if (score > scoreThreshold)
            {
                IncreaseDifficulty(difficultyIncreaseLimit, difficultyIncreaseSpeed);
            }
            if (damage > damageThreshold)
            {
                GameOver();
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
                            Height = bulletHeight,
                            Width = bulletWidth,
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
                Height = enemyHeight,
                Width = enemyWidth,
                Fill = enemySprite,
            };
            Canvas.SetTop(newEnemy, enemySpawnY);
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));
            GameCanvas.Children.Add(newEnemy);
        }
        private void InitializeBackgroundCanvas()
        {
            ImageBrush bg = new ImageBrush();

            bg.ImageSource = new BitmapImage(new Uri($"{imagePath}space.png"));
            bg.TileMode = TileMode.Tile;
            bg.Viewport = new Rect(0, 0, 0.15, 0.15);
            bg.ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
            GameCanvas.Background = bg;
        }
        private void InitializePlayer()
        {
            ImageBrush playerSprite = new ImageBrush();

            playerSprite.ImageSource = new BitmapImage(new Uri($"{imagePath}player.png"));
            player.Fill = playerSprite;
        }
        private Rect ShootBullet(Rectangle bullet)
        {
            Canvas.SetTop(bullet, Canvas.GetTop(bullet) - bulletSpeed);

            Rect bulletHitBox = new Rect(Canvas.GetLeft(bullet), Canvas.GetTop(bullet), bullet.Width, bullet.Height);
            return bulletHitBox;
        }
        private void EnemyHit(Rectangle entity, Rectangle enemy)
        {
            itemRemover.Add(entity);
            itemRemover.Add(enemy);
            score += scoreOnKill;
            scoreText.Content = $"Score: {score}";
        }
        private void DamagePlayer(Rectangle entity, int damageToDeal)
        {
            itemRemover.Add(entity);
            damage += damageToDeal;
            damageText.Content = $"Damage: {damage}";
        }
        private void IncreaseDifficulty(int newLimit, int newEnemySpeed)
        {
            limit = newLimit;
            enemySpeed = newEnemySpeed;
        }
        private void GameOver()
        {
            gameTimer.Stop();
            damageText.Content = "Damage: 100";
            damageText.Foreground = Brushes.Red;
            MessageBox.Show($"Captain, Your Score Was: {score}\nBetter Luck Next Time!", "Game Over!");
            Close();
        }
        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            windowWidth = ActualWidth;
        }
    }
}
