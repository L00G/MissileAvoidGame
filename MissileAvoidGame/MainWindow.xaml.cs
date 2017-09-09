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
        DispatcherTimer timer;
        TextBlock timerBlock;
        int m_playTime = 0;
        int m_startTime = DateTime.Now.Second;
        public MainWindow()
        {
            InitializeComponent();

            rootCanvas = new Canvas();

            rootCanvas.Height = 500;
            rootCanvas.Width = 500;

            rootCanvas.Background = new SolidColorBrush(Colors.AliceBlue);
            rootCanvas.Children.Add(oPlayer.GetPlayerCharacter());


            timerBlock = new TextBlock();
            timerBlock.Text = "00:00";
            timerBlock.Width = 80;
            Canvas.SetLeft(timerBlock, rootCanvas.Width / 2 - 40);

            rootCanvas.Children.Add(timerBlock);


            oMissile = new List<Missile>();

            for (int i = 0; i <50; i++)
            {
                NormalMissile temp = new NormalMissile();
                temp.Initialize();
                rootCanvas.Children.Add(temp.GetMissileObject());
                oMissile.Add(temp);
            }

            for (int i = 0; i < 10; i++)
            {
                GuidedMissile temp = new GuidedMissile();
                temp.Initialize();
                temp.SetTarget(oPlayer);
                rootCanvas.Children.Add(temp.GetMissileObject());
                oMissile.Add(temp);
            }

            this.PreviewKeyUp += EventKeyUp;
            this.PreviewKeyDown += EventKeyDown;

            timer = new DispatcherTimer();
            timer.Tick += GameLoop;
            timer.Interval = TimeSpan.FromSeconds((1.0/60.0));
            timer.Start();

            this.Width = 600;
            this.Height = 600;

            rootCanvas.HorizontalAlignment = HorizontalAlignment.Center;
            rootCanvas.VerticalAlignment = VerticalAlignment.Center;
            this.Content = rootCanvas;

          
        }

        private void GameLoop(object sender, EventArgs e)
        {
            oPlayer.Move();

            for(int i=0;i<oMissile.Count;i++)
            {
                Missile temp = oMissile.ElementAt(i);
                temp.Update();
                if (!temp.IsOnView())
                {
                    Missile t;
                    if (temp is GuidedMissile)
                    {
                        t = new GuidedMissile();
                        ((GuidedMissile)t).SetTarget(oPlayer);
                    }
                    else
                        t = new NormalMissile();
                    t.Initialize();
                    rootCanvas.Children.RemoveAt(i+2);
                    oMissile.RemoveAt(i);
                    oMissile.Add(t);
                    rootCanvas.Children.Add(t.GetMissileObject());
                    i--;
                }
                else if (oPlayer.IsCollisionObject(temp.GetCenter()))
                {
                    temp.GetMissileObject().Stroke = System.Windows.Media.Brushes.DarkGray;
                    //timer.Stop();
                }
            }
            m_playTime = DateTime.Now.Second - m_startTime ;
            timerBlock.Text = String.Format("{0} : {1}", m_playTime / 60, m_playTime % 60);
        } 
        
        private void EventKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up) oPlayer.RemoveDirection((int)MoveDirection.Up);
            else if(e.Key == Key.Down) oPlayer.RemoveDirection((int)MoveDirection.Down);
            else if(e.Key == Key.Left) oPlayer.RemoveDirection((int)MoveDirection.Left);
            else if(e.Key == Key.Right) oPlayer.RemoveDirection((int)MoveDirection.Right);
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
            else if(e.Key == Key.Left)
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
