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
    class Missile
    {
        protected static Random c_rand = new Random();

        protected Ellipse m_missile;
        protected double m_nowX, m_nowY;
        protected double m_dx, m_dy;
        protected double m_speed;
        protected int m_radius;

        public Missile()
        {
            m_radius = 4;
            m_missile = new Ellipse();    
            m_missile.StrokeThickness = 2;      
            m_missile.Width = m_radius;
            m_missile.Height = m_radius;
        }

        public virtual void Move() { }
        public virtual void Update() { }
        public virtual void Initialize()
        {
            m_speed = c_rand.NextDouble() * 2 + 0.3;
            double direction = c_rand.NextDouble();
            if (direction > 0.75)
            {
                m_nowY = 0;
                m_nowX = c_rand.Next(500);
            }
            else if (direction > 0.50)
            {
                m_nowY = c_rand.Next(500);
                m_nowX = 500;
            }
            else if (direction > 0.25)
            {
                m_nowY = 500;
                m_nowX = c_rand.Next(500);
            }
            else
            {
                m_nowY = c_rand.Next(500);
                m_nowX = 0;
            }

            Canvas.SetTop(m_missile, m_nowY);
            Canvas.SetLeft(m_missile, m_nowX);
        }

        public Point GetCenter()
        {
            return new Point(m_nowX, m_nowY);
        }

        public Ellipse GetMissileObject()
        {
            return m_missile;
        }

        public bool IsOnView()
        {
            if (500 < m_nowX || m_nowX < 0 || 500 < m_nowY || m_nowY<0)
            {
                return false;
            }
            return true;
        } 
    }
}
