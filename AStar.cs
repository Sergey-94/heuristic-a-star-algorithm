using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_agents
{
    internal class AStar
    {
        private ObjectWorld[,] map;
        private int width;
        private int height;
 
        public AStar(ObjectWorld[,] map)
        {
            this.map = map;
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
        }

        public List<Point> FindPath(ObjectWorld startObj, ObjectWorld goalObj)
        {
            Point start = GetObjectPosition(startObj);
            Point goal = GetObjectPosition(goalObj);
            var openList = new List<Node>();
            var closedList = new HashSet<Point>();
            Node startNode = new Node(start);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                // Get the node with the lowest F cost
                openList.Sort((node1, node2) => node1.F.CompareTo(node2.F));
                Node currentNode = openList[0];
                // Check if we've reached the goal
                if (currentNode.Position == goal)
                {
                    return ReconstructPath(currentNode);
                }
                openList.Remove(currentNode);
                closedList.Add(currentNode.Position);
                // Get neighbors
                foreach (var neighbor in GetNeighbors(currentNode.Position))
                {
                    if (closedList.Contains(neighbor))
                        continue;
                    int tentativeG = currentNode.G + 1; // Assuming each step costs 1
                    Node neighborNode = openList.Find(n => n.Position == neighbor);
                    if (neighborNode == null)
                    {
                        neighborNode = new Node(neighbor);
                        openList.Add(neighborNode);
                    }
                    else if (tentativeG >= neighborNode.G)
                    {
                        continue;
                    }
                    neighborNode.Parent = currentNode;
                    neighborNode.G = tentativeG;
                    neighborNode.H = Heuristic(neighbor, goal);
                }
            }
            return null; // No path found
        }

        private List<Point> ReconstructPath(Node node)
        {
            var path = new List<Point>();
            while (node != null)
            {
                path.Add(node.Position);
                node = node.Parent;
            }
            path.Reverse();
            return path;
        }

        private IEnumerable<Point> GetNeighbors(Point position)
        {
            var neighbors = new List<Point>();
            int x = position.X;
            int y = position.Y;

            // Проверяем все четыре направления (вверх, вниз, влево, вправо)
            if (x > 0 && map[x - 1, y] == null) // Влево
                neighbors.Add(new Point(x - 1, y));
            if (x < width - 1 && map[x + 1, y] == null) // Вправо
                neighbors.Add(new Point(x + 1, y));
            if (y > 0 && map[x, y - 1] == null) // Вверх
                neighbors.Add(new Point(x, y - 1));
            if (y < height - 1 && map[x, y + 1] == null) // Вниз
                neighbors.Add(new Point(x, y + 1));
            if (x > 0 && (map[x - 1, y] == null || !map[x - 1, y].IsObstacle()))
                neighbors.Add(new Point(x - 1, y));
            return neighbors;
        }

        private int Heuristic(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y); // Manhattan distance
        }

        // Получить позицию экземпляра объекта
        public Point GetObjectPosition(ObjectWorld obj)
        {
            for (int i = 0; i < World.Width; i++)
            {
                for (int j = 0; j < World.Height; j++)
                {
                    if (map[i, j] != null)
                    {
                        if (map[i, j].Equals(obj))
                        {
                            return new Point(i, j);
                        }
                    }
                }
            }
            return new Point(-1, -1);
        }

        // Удалить объекты
        public void RemoveObjects(World w)
        {
            for (int i = 0; i < World.Width; i++)
            {
                for (int j = 0; j < World.Height; j++)
                {
                    if (w.map[i, j] != null)
                    {
                        if (w.map[i, j].IsPathPoint())
                        {
                            w.map[i, j] = null;
                        }
                    }
                }
            }
        }


        // Переместить объект obj в точку target из точки curPos
        public void MoveTo(ObjectWorld obj, Point target)
        {
            Point curPos = GetObjectPosition(obj);
            if (target.X >= 0 & target.X < World.Width && target.Y >= 0 & target.Y < World.Height)
            {
                // Если ячейка пуста
                if (map[target.X, target.Y] == null)
                {
                    // Удаляем объект из прошлой позиции
                    map[curPos.X, curPos.Y] = null;
                    // Добавляем в новую
                    map[target.X, target.Y] = obj;
                }
            }
        }

    }
}
