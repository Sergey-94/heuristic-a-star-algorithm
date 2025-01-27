using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_agents
{
    internal class World
    {
        // Размеры сетки
        public static int gridSize = 20; // Размер ячейки сетки

        // Ширина
        public static int Width = 50;
        // Высота
        public static int Height = 50;

        // Создаем двумерный массив карты мира
        public ObjectWorld[,] map = new ObjectWorld[Width, Height];

        //-----
        public AStar aStar;
        //-----

        public World()
        {
            aStar = new AStar(map);
        }


        // Обязательные объекты на карте
        public ObjectWorld Agent = new ObjectWorld(typeObject.Agent);
        public ObjectWorld Target = new ObjectWorld(typeObject.Target);
        public ObjectWorld PathPoint = new ObjectWorld(typeObject.pathPoint);

        // Инициализация - заполнение объектами мира
        internal void Init()
        {
            map[10, 10] = Agent;

            map[20, 8] = new ObjectWorld(typeObject.Wall);
            map[20, 9] = new ObjectWorld(typeObject.Wall);
            map[20, 10] = new ObjectWorld(typeObject.Wall);
            map[20, 11] = new ObjectWorld(typeObject.Wall);
            map[20, 12] = new ObjectWorld(typeObject.Wall);
            map[20, 13] = new ObjectWorld(typeObject.Wall);

            map[25, 9] = Target;
        }

        public void AddObject(Point pos, typeObject type)
        {
            map[pos.X, pos.Y] = new ObjectWorld(type);
        }

        // Возвращает массив клеток мира
        public ObjectWorld[,] GetObjectWorlds()
        {
            return this.map;
        }
    }
}
