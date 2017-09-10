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
            m_missile.Stroke = System.Windows.Media.Brushes.Chocolate;
            m_missile.Fill = System.Windows.Media.Brushes.Chocolate;
        }

        public override void Initialize()
        {
            base.Initialize();
         
            double dstX = c_rand.Next(100) + 200;
            double dstY = c_rand.Next(100) + 200;

            m_dirX = dstX - m_nowX;
            m_dirY = dstY - m_nowY;

            double r = Math.Sqrt(Math.Pow(m_dirX, 2) + Math.Pow(m_dirY, 2));
            double fr = r / m_speed;

            m_dirY = m_dirY / fr;
            m_dirX = m_dirX / fr;
        }
        public override void Update()
        {
            Move();
        }
    }
}
