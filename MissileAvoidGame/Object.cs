using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace MissileAvoidGame
{
    abstract class Object
    {
        static protected Random rand = new Random();

        protected double m_x, m_y;
        protected double m_size;

        public abstract void Initialize();
        public abstract void Move();
        public abstract void Render();
        public abstract void Update();

        public abstract Shape GetObject();

        public bool IsCollisionObj(Object obj)
        {
            if (obj.m_x - obj.m_size/2 < m_x && m_x < obj.m_x + obj.m_size / 2)
                if (obj.m_y - obj.m_size / 2 < m_y && m_y < obj.m_y + obj.m_size / 2)
                    return true;
            return false;
        }
    }
}
