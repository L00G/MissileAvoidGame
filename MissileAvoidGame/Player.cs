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

    class Player : Object
    {
        private Rectangle m_character;
        private int m_speed;
        private int m_moveFlag;

        public Player()
        {
            m_size = 15;
            m_character = new Rectangle
            {
                Width = m_size,
                Height = m_size,
                Stroke = System.Windows.Media.Brushes.BlanchedAlmond,
                StrokeThickness = 2
            };      
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

        public override void Initialize()
        {
            m_speed = 4;
            m_moveFlag = 0;
            m_x = m_y = 250;
            m_character.SetValue(Canvas.TopProperty, m_y - 10);
            m_character.SetValue(Canvas.LeftProperty, m_x - 10);
        }
        public override void Move()
        {
            double dx = 0, dy = 0;
            if ((m_moveFlag&(int)MoveDirection.Up)== (int)MoveDirection.Up)
                dy = - m_speed;
            else if ((m_moveFlag & (int)MoveDirection.Down) == (int)MoveDirection.Down)
                dy = + m_speed;

            if ((m_moveFlag & (int)MoveDirection.Left) == (int)MoveDirection.Left)
                dx = - m_speed;
            else if ((m_moveFlag & (int)MoveDirection.Right) == (int)MoveDirection.Right)
                dx = + m_speed;

            double r = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
            double fr = r / m_speed;

            if (fr != 0)
            {
                m_x += dx / fr;
                m_y += dy / fr;
            }         
        }
        public override void Render()
        {
            m_character.SetValue(Canvas.TopProperty, m_y - 10);
            m_character.SetValue(Canvas.LeftProperty, m_x - 10);
        }
        public override void Update() { }
        public override Shape GetObject()
        {
            return m_character;
        }
    }
}
