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
        Player m_target;
        private double m_dirX, m_dirY;
        protected double m_dstX, m_dstY;
        double maxSpeed;
        double degree = 0;
        double dstDegree;
        int tick = 0;

        public GuidedMissile() : base()
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();
            m_missile.Stroke = System.Windows.Media.Brushes.CadetBlue;
            m_missile.Fill = System.Windows.Media.Brushes.CadetBlue;

            maxSpeed = m_speed;
        }

        public void SetTarget(Player player)
        {
            m_target = player;
            m_dstX = m_target.GetPlayerPosition().X;
            m_dstY = m_target.GetPlayerPosition().Y;

            degree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
            m_dirX = Math.Cos(degree / 180 *Math.PI) * m_speed;
            m_dirY = Math.Sin(degree/180 *Math.PI) * m_speed;

            dstDegree = degree;
            degree = (int)degree;
            dstDegree = (int)dstDegree;
        }
        public void ReTargeting()
        {
            m_dstX = m_target.GetPlayerPosition().X;
            m_dstY = m_target.GetPlayerPosition().Y;

            dstDegree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
            dstDegree = (int)dstDegree;

        }
        public void Rotate()
        {
            bool rotation = false;
            if (degree != dstDegree)
            {
                if (degree + 180 < dstDegree + 180)
                {
                    if (dstDegree + 180 - degree + 180 < 360 - dstDegree + 180 + degree + 180)
                    {
                        rotation = false;
                    }
                    else
                    {
                        rotation = true;
                    }
                }
                else
                {
                    if (degree + 180 - dstDegree + 180 < 360 - degree + 180 + dstDegree + 180)
                    {
                        rotation = true;
                    }
                    else
                    {
                        rotation = false;
                    }
                }

                if (rotation)
                {
                    double t = Math.Abs(dstDegree - degree);
                    if (t > 5) t = 5;
                    degree = degree - t;
                    if (degree < -180) degree = 179;
                }
                else
                {
                    double t = Math.Abs(dstDegree - degree);
                    if (t > 5) t = 5;
                    degree = degree + t;
                    if (degree > 180) degree = -179;
                }
                m_speed -= 0.05;
                if (m_speed<0.5) m_speed = 0.5;
                m_dirX = Math.Cos(degree / 180 * Math.PI) * m_speed;
                m_dirY = Math.Sin(degree / 180 * Math.PI) * m_speed;

                dstDegree = Math.Atan2(m_dstY - m_nowY, m_dstX - m_nowX) * 180 / Math.PI;
                dstDegree = (int)dstDegree;
            }
            else
            {
                m_speed +=0.08;
                if (m_speed > maxSpeed) m_speed = maxSpeed;
                m_dirX = Math.Cos(degree / 180 * Math.PI) * m_speed;
                m_dirY = Math.Sin(degree / 180 * Math.PI) * m_speed;
            }
        }

        public override void Move()
        {
            m_nowY += m_dirY;
            m_nowX += m_dirX; 

            Canvas.SetTop(m_missile, m_nowY - m_radius / 2);
            Canvas.SetLeft(m_missile, m_nowX - m_radius / 2);
        }

        public override void Update()
        {
            tick++;
            if (tick == 120)
            {
                ReTargeting();
                tick = 0;
            }
            Rotate();
            Move();
        }
    }
}
