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
         
            double dstX = rand.Next(100) + 200;
            double dstY = rand.Next(100) + 200;

            m_dirX = dstX - m_x;
            m_dirY = dstY - m_y;

            double r = Math.Sqrt(Math.Pow(m_dirX, 2) + Math.Pow(m_dirY, 2));
            double fr = r / m_speed;

            m_dirY = m_dirY / fr;
            m_dirX = m_dirX / fr;
        }
        public override void Update()
        {
            Move();
            if (500 < m_x || m_x < 0 || 500 < m_y || m_y < 0)
                Initialize();
        }
    }
}
