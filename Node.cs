using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_agents
{
    internal class Node
    {
        public Point Position { get; set; }
        public Node Parent { get; set; }
        public int G { get; set; } // Cost from start to this node
        public int H { get; set; } // Heuristic cost to goal
        public int F => G + H;
        public Node(Point position)
        {
            Position = position;
        }
    }
}
