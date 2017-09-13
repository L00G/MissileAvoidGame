using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MissileAvoidGame
{
    class Missile : Object
    {
        protected Ellipse m_missile;
        protected double m_dirX, m_dirY;
        protected double m_speed;

        public Missile()
        {
            m_size = 4;
            m_missile = new Ellipse
            {
                StrokeThickness = 2,
                Width = m_size,
                Height = m_size
            };
        }

        public override void Initialize()
        {
            m_speed = rand.NextDouble() * 2 + 0.3;
            double direction = rand.NextDouble();
            m_x = m_y = (direction > 0.5) ? 0 : 500;
            if (direction > 0.75)
                m_x = rand.Next(500);
            else if (direction > 0.50)
                m_y = rand.Next(500);
            else if (direction > 0.25)
                m_x = rand.Next(500);
            else
                m_y = rand.Next(500);
        }
        public override void Move()
        {
            m_x += m_dirX;
            m_y += m_dirY;
        }
        public override void Render()
        {
            Canvas.SetTop(m_missile, m_y - m_size / 2);
            Canvas.SetLeft(m_missile, m_x - m_size / 2);
        }
        public override void Update() { }
        public override Shape GetObject()
        {
            return m_missile;
        }

        public bool IsOnView()
        {
            if (500 < m_x || m_x < 0 || 500 < m_y || m_y<0)
            {
                return false;
            }
            return true;
        }

     
    }
}
