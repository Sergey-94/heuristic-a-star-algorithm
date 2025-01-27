using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace AI_agents
{
    public partial class Form1 : Form
    {
        // Мир
        World world;

        // Смещение в окне
        int startpositionH = 0;// World.Height * World.gridSize / 4;
        int startpositionW = 0;// World.Width * World.gridSize / 4;

        public Form1()
        {
            InitializeComponent();
            // Подписываемся на событие PaintEventHandler
            this.Paint += new PaintEventHandler(Form1_Paint);

            // Подписываемся на событие KeyDown
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            // Подписываемся на событие MouseDown
            this.MouseDown += new MouseEventHandler(OnMouseDown);

            // Включаем двойную буферизацию
            this.DoubleBuffered = true;

            // Создаем мир
            world = new World();
            // Заполняем объектами
            world.Init();
        }


        // Перерисовываем сцену
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Рисуем прямоугольники по сетке
            for (int i = 0; i < World.Height; i++)
            {
                for (int j = 0; j < World.Width; j++)
                {
                    // Вычисляем координаты прямоугольника
                    int y = i * World.gridSize + startpositionH;
                    int x = j * World.gridSize + startpositionW;

                    // Рисуем прямоугольник
                    if (world.GetObjectWorlds()[j, i] == null)
                    {
                        // Пустота
                        e.Graphics.FillRectangle(Brushes.Blue, x, y, World.gridSize, World.gridSize);
                        //e.Graphics.DrawString("n", new Font("Areal", 6, FontStyle.Regular), Brushes.White, new Point(x, y));
                    }
                    else
                    {
                        // Объект
                        e.Graphics.FillRectangle(world.GetObjectWorlds()[j, i].GetColorObject(), x, y, World.gridSize, World.gridSize);
                        //e.Graphics.DrawString(world.GetObjectWorlds()[j, i].GetColorObject().GetHashCode().ToString(), new Font("Areal", 5, FontStyle.Regular), Brushes.White, new Point(x, y));
                    }

                    // Рисуем границу прямоугольника
                    e.Graphics.DrawRectangle(Pens.Black, x, y, World.gridSize, World.gridSize);
                }
            }
        }


        // Обновляем сцену
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        // Обработчик события KeyDown
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Point pos = world.aStar.GetObjectPosition(world.Agent);
            // Проверяем, какая клавиша была нажата
            switch (e.KeyCode)
            {
                //case Keys.Up:
                //    world.aStar.MoveTo(world.Agent, new Point(pos.X, pos.Y - 1));
                //    break;
                //case Keys.Down:
                //    world.aStar.MoveTo(world.Agent, new Point(pos.X, pos.Y + 1));
                //    break;
                //case Keys.Left:
                //    world.aStar.MoveTo(world.Agent, new Point(pos.X - 1, pos.Y));
                //    break;
                //case Keys.Right:
                //    world.aStar.MoveTo(world.Agent, new Point(pos.X + 1, pos.Y));
                //    break;
                case Keys.Space:
                    world.aStar.RemoveObjects(world);
                    var path = world.aStar.FindPath(world.Agent, world.Target);

                    if (path != null)
                    {
                        bool isFirst = true;
                        foreach (var n in path)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                                continue;
                            }

                            if (n.Equals(path.Last()))
                            {
                                continue;
                            }
                            else
                            {
                                world.AddObject(n, typeObject.pathPoint);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Путь не существует!");
                    }
                    
                    break;
            }
        }


        // Обработчик события MouseDown
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Получаем позицию мыши относительно окна
            Point mousePosition = e.Location;

            Point point = new Point(mousePosition.X / World.gridSize, mousePosition.Y / World.gridSize);

            // Проверяем, какая кнопка мыши была нажата
            if (e.Button == MouseButtons.Left)
            {
                world.aStar.MoveTo(world.Target, point);
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // Получаем позицию мыши относительно окна
            Point mousePosition = e.Location;

            Point point = new Point(mousePosition.X / World.gridSize, mousePosition.Y / World.gridSize);

            // Проверяем, какая кнопка мыши была нажата
            if (e.Button == MouseButtons.Right)
            {
                world.AddObject(point, typeObject.Wall);
            }
        }
    }
}
