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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MissileAvoidGame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        Player oPlayer = new Player();
        List<Missile> oMissile;
        Canvas rootCanvas;
        DispatcherTimer gameLoopTimer,addMissileTimer;
        private int misiilleCount;
        public MainWindow()
        {
            InitializeComponent();

            rootCanvas = new Canvas
            {
                Height = 500,
                Width = 500,
                Background = new SolidColorBrush(Colors.AliceBlue),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Initialize();

            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoop;
            gameLoopTimer.Interval = TimeSpan.FromSeconds(1.0 / 60.0);
            gameLoopTimer.Start();

            addMissileTimer = new DispatcherTimer();
            addMissileTimer.Tick += AddMissile;
            addMissileTimer.Interval = TimeSpan.FromSeconds(1);
            addMissileTimer.Start();

            PreviewKeyUp += EventKeyUp;
            PreviewKeyDown += EventKeyDown;

            Width = 600;
            Height = 600;

            Content = rootCanvas;
        }
      
        public void Initialize()
        {
            rootCanvas.Children.Clear();
           
            if (oPlayer == null)
                oPlayer = new Player();
            oPlayer.Initialize();

            rootCanvas.Children.Add(oPlayer.GetPlayerCharacter());
            
            if (oMissile == null)
                oMissile = new List<Missile>();
            else
                oMissile.Clear();

            for (int i = 0; i < 50; i++)
            {
                NormalMissile missile = new NormalMissile();
                missile.Initialize();
                rootCanvas.Children.Add(missile.GetMissileObject());
                oMissile.Add(missile);
            }
            misiilleCount = 50;
        }
        public void Update()
        {
            oPlayer.Move();

            for (int i = 0; i < oMissile.Count; i++)
            {
                Missile missile = oMissile.ElementAt(i);
                missile.Update();
                if (!missile.IsOnView())
                    missile.Initialize();
                if (oPlayer.IsCollisionObject(missile.GetObjectPosition()))
                {
                    MessageBoxResult result = MessageBox.Show("are you ready?", "replay?", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                        Initialize();
                    else if (result == MessageBoxResult.No)
                        Close();
                }
            }
        }
        public void Render()
        {
            oPlayer.Render();

            for (int i = 0; i < oMissile.Count; i++)
            {
                Missile temp = oMissile.ElementAt(i);
                temp.Render();
            }
        }
        public void GameLoop(object sender, EventArgs e)
        {
            Update();
            Render();
        }
        public void AddMissile(object sender, EventArgs e)
        {
            Missile missile;
            if (++misiilleCount % 5 == 0)
            {
                missile = new GuidedMissile();
                ((GuidedMissile)missile).SetTarget(oPlayer);
            }
            else
                missile = new NormalMissile();
            missile.Initialize();
            missile.Render();

            oMissile.Add(missile);
            rootCanvas.Children.Add(missile.GetMissileObject());
        }

        private void EventKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) oPlayer.RemoveDirection((int)MoveDirection.Up);
            else if (e.Key == Key.Down) oPlayer.RemoveDirection((int)MoveDirection.Down);
            else if (e.Key == Key.Left) oPlayer.RemoveDirection((int)MoveDirection.Left);
            else if (e.Key == Key.Right) oPlayer.RemoveDirection((int)MoveDirection.Right);
        }
        private void EventKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                oPlayer.RemoveDirection((int)MoveDirection.Down);
                oPlayer.AddDirection((int)MoveDirection.Up);
            }
            else if (e.Key == Key.Down)
            {
                oPlayer.RemoveDirection((int)MoveDirection.Up);
                oPlayer.AddDirection((int)MoveDirection.Down);
            }
            else if (e.Key == Key.Left)
            {
                oPlayer.RemoveDirection((int)MoveDirection.Right);
                oPlayer.AddDirection((int)MoveDirection.Left);
            }
            else if (e.Key == Key.Right)
            {
                oPlayer.RemoveDirection((int)MoveDirection.Left);
                oPlayer.AddDirection((int)MoveDirection.Right);
            }
        }
    }
}
