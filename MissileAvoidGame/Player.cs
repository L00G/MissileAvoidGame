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
    enum MoveDirection{
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }

    class Player
    {
        private Rectangle m_character;
        private int m_speed = 4;
        private int m_moveFlag=0;
        private double m_x, m_y;
        public Player()
        {
            m_x = 200;
            m_y = 200;
            m_character = new Rectangle();
            m_character.Width = 15;
            m_character.Height = 15;
            m_character.Stroke = System.Windows.Media.Brushes.BlanchedAlmond;
            m_character.StrokeThickness = 2;
            Canvas.SetTop(m_character, m_y-10);
            Canvas.SetLeft(m_character,m_x-10);
        }

        public Rectangle GetPlayerCharacter()
        {
            return m_character;
        }

        public Point GetPlayerPosition()
        {
            return new Point(m_x, m_y);
        }

        public void AddDirection(int flag)
        {
            m_moveFlag = m_moveFlag | flag;
        }

        public void RemoveDirection(int flag)
        {
            if((m_moveFlag&flag)==flag)
                m_moveFlag = m_moveFlag - flag;
        }

        public bool IsCollisionObject(Point center)
        {
            if (Math.Abs(m_x - center.X) < 6 && Math.Abs(m_y - center.Y) < 6)
                return true;
            return false;
        }
        public void Move()
        {
            double dx = 0, dy = 0;
            if ((m_moveFlag&(int)MoveDirection.Up)== (int)MoveDirection.Up)
            {
                dy = - m_speed;
            }
            else if ((m_moveFlag & (int)MoveDirection.Down) == (int)MoveDirection.Down)
            {
                dy = + m_speed;
            }
            if ((m_moveFlag & (int)MoveDirection.Left) == (int)MoveDirection.Left)
            {
                dx = - m_speed;
            }
            else if ((m_moveFlag & (int)MoveDirection.Right) == (int)MoveDirection.Right)
            {
                dx = + m_speed;
            }
            double r = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            double fr = r / m_speed;

            if (fr != 0)
            {
                m_x += dx / fr;
                m_y += dy / fr;
            }
            Canvas.SetTop(m_character, m_y - 10);
            Canvas.SetLeft(m_character, m_x - 10);

        }
    }
}
