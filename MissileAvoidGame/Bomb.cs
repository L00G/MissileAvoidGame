using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MissileAvoidGame
{
    class Bomb : Object
    {
        private Ellipse m_bomb;
        private double m_guage;
        private int bombCount;
        public Bomb()
        {
            m_bomb = new Ellipse();
            m_bomb.StrokeThickness = 2;      
        }

        public override void Initialize()
        {
            m_x = rand.Next(500);
            m_y = rand.Next(500);
            m_size = rand.Next(50);

            m_guage = 0;
            bombCount = 5;
            m_bomb.Stroke = System.Windows.Media.Brushes.Aqua;
            m_bomb.Fill = System.Windows.Media.Brushes.Transparent;

            Canvas.SetTop(m_bomb, m_y - m_size / 2);
            Canvas.SetLeft(m_bomb, m_x - m_size / 2);
        }
        public override void Move() { }
        public override void Render()
        {
            m_bomb.Height = m_bomb.Width = m_size / 100 * m_guage;

            Canvas.SetTop(m_bomb, m_y - m_bomb.Height / 2);
            Canvas.SetLeft(m_bomb, m_x - m_bomb.Width / 2);
        }
        public override void Update()
        {
            if (m_guage < 100)
                m_guage += 1;
            else
            {
                m_bomb.Stroke = System.Windows.Media.Brushes.HotPink;
                m_bomb.Fill = System.Windows.Media.Brushes.HotPink;
                if (bombCount == 0)
                    Initialize();
                bombCount--;
            }
        }
        public override Shape GetObject()
        {
            return m_bomb;
        }
    }
}
