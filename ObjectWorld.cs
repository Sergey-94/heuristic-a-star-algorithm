using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_agents
{
    internal class ObjectWorld
    {
        // Тип объекта
        typeObject type;


        // Конструктор
        public ObjectWorld(typeObject t)
        {
            this.type = t;
        }


        public bool IsObstacle()
        {
            if (this.type == typeObject.Wall) 
            {  
                return true; 
            }
            return false;
        }

        public bool IsPathPoint()
        {
            if (this.type == typeObject.pathPoint)
            {
                return true;
            }
            return false;
        }


        // Получаем цвет объекта для отрисовки
        public Brush GetColorObject()
        {
            switch (type)
            {
                case typeObject.Agent:
                    return Brushes.DarkGreen;
                case typeObject.Target:
                    return Brushes.Maroon;
                case typeObject.Wall:
                    return Brushes.Black;
                case typeObject.pathPoint:
                    return Brushes.Plum;
            }
            return Brushes.Gainsboro;
        }
    }




    // Определяем перечисление типов объектов
    public enum typeObject
    {
        Agent,    // 0
        Target,   // 1
        Wall,     // 2
        pathPoint // 3
    }
}
