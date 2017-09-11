using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MissileAvoidGame
{
    class GuidedMissile : Missile
    {
        private Player m_target;
        private double m_dstX, m_dstY;
        private double m_maxSpeed;
        private double m_degree;
        private double m_dstDegree;
        private int m_tick = 0;

        public GuidedMissile() : base()
        {
            m_missile.Stroke = System.Windows.Media.Brushes.CadetBlue;
            m_missile.Fill = System.Windows.Media.Brushes.CadetBlue;
        }

        public override void Initialize()
        {
            base.Initialize();
   
            m_maxSpeed = m_speed;
        }
        public void SetTarget(Player player)
        {
            m_target = player;
            m_dstX = m_target.GetPlayerPosition().X;
            m_dstY = m_target.GetPlayerPosition().Y;

            m_degree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
            m_dirX = Math.Cos(m_degree / 180 * Math.PI) * m_speed;
            m_dirY = Math.Sin(m_degree / 180 * Math.PI) * m_speed;

            m_dstDegree = m_degree;
            m_degree = (int)m_degree;
            m_dstDegree = (int)m_dstDegree;
        }
        public void ReTargeting()
        {
            m_dstX = m_target.GetPlayerPosition().X;
            m_dstY = m_target.GetPlayerPosition().Y;

            m_dstDegree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
            m_dstDegree = (int)m_dstDegree;

        }
        public void Rotate()
        {  
            if (m_degree != m_dstDegree)
            {
                bool rotation = false;
                if (m_degree < m_dstDegree)
                {
                    if (m_dstDegree  - m_degree < 360 - m_dstDegree  + m_degree)
                        rotation = false;
                    else
                        rotation = true;
                }
                else
                {
                    if (m_degree - m_dstDegree < 360 - m_degree + m_dstDegree)
                        rotation = true;
                    else
                        rotation = false;
                }

                double t = Math.Abs(m_dstDegree - m_degree);
                if (t > 5) t = 5;
                if (rotation)
                {
                    m_degree = m_degree - t;
                    if (m_degree < -180) m_degree = 179;
                }
                else
                {
                    m_degree = m_degree + t;
                    if (m_degree > 180) m_degree = -179;
                }

                m_speed -= 0.05;
                if (m_speed<0.5) m_speed = 0.5;
                m_dirX = Math.Cos(m_degree / 180 * Math.PI) * m_speed;
                m_dirY = Math.Sin(m_degree / 180 * Math.PI) * m_speed;

                m_dstDegree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
                m_dstDegree = (int)m_dstDegree;
            }
            else
            {
                m_speed +=0.08;
                if (m_speed > m_maxSpeed) m_speed = m_maxSpeed;
                m_dirX = Math.Cos(m_degree / 180 * Math.PI) * m_speed;
                m_dirY = Math.Sin(m_degree / 180 * Math.PI) * m_speed;
            }
        }

        public override void Update()
        {
            m_tick++;
            if (m_tick == 120)
            {
                ReTargeting();
                m_tick = 0;
            }
            Rotate();
            Move();
        }
    }
}
