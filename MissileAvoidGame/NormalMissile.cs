using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MissileAvoidGame
{
    class NormalMissile : Missile
    {
        public NormalMissile() : base()
        {
            
        }
        public override void Initialize()
        {
            base.Initialize();
            m_missile.Stroke = System.Windows.Media.Brushes.Chocolate;
            m_missile.Fill = System.Windows.Media.Brushes.Chocolate;

            double dstX = c_rand.Next(100) + 200;
            double dstY = c_rand.Next(100) + 200;

            m_dx = dstX - m_nowX;
            m_dy = dstY - m_nowY;

            double r = Math.Sqrt(Math.Pow(m_dx, 2) + Math.Pow(m_dy, 2));
            double fr = r / m_speed;

            m_dy = m_dy / fr;
            m_dx = m_dx / fr;
        }
        public override void Move()
        {
            m_nowX += m_dx;
            m_nowY += m_dy;

            Canvas.SetTop(m_missile, m_nowY - m_radius / 2);
            Canvas.SetLeft(m_missile, m_nowX - m_radius / 2);
        }
        public override void Update()
        {
            Move();
        }
    }
}
