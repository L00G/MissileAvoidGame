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

            rootCanvas = new Canvas();
            rootCanvas.Height = 500;
            rootCanvas.Width = 500;
            rootCanvas.Background = new SolidColorBrush(Colors.AliceBlue); 
            rootCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            rootCanvas.VerticalAlignment = VerticalAlignment.Center;
            rootCanvas.Children.Add(oPlayer.GetPlayerCharacter());

            oMissile = new List<Missile>();

            for (int i = 0; i < 50; i++)
            {
                NormalMissile missile = new NormalMissile();
                missile.Initialize();
                rootCanvas.Children.Add(missile.GetMissileObject());
                oMissile.Add(missile);
            }

            this.PreviewKeyUp += EventKeyUp;
            this.PreviewKeyDown += EventKeyDown;

            gameLoopTimer = new DispatcherTimer();
            gameLoopTimer.Tick += GameLoop;
            gameLoopTimer.Interval = TimeSpan.FromSeconds(1.0 / 60.0);
            gameLoopTimer.Start();

            addMissileTimer = new DispatcherTimer();
            addMissileTimer.Tick += AddMissile;
            addMissileTimer.Interval = TimeSpan.FromSeconds(1);
            addMissileTimer.Start();

            this.Width = 600;
            this.Height = 600;

            this.Content = rootCanvas;
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
                    gameLoopTimer.Stop();
                    addMissileTimer.Stop();
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
