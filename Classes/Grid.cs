using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTry.Classes
{
    class Grid
    {
        #region //container
        private int widthGrid;
        private int heightGrid;
        private bool[,] table;
        #endregion

        public int Width
        {
            get { return widthGrid; }
            set
            {
                if (value > 0 && value < 100)
                {
                    widthGrid = value;
                }
            }
        }

        public int Height
        {
            get { return heightGrid; }
            set
            {
                if (value > 0 && value < 100)
                {
                    heightGrid = value;
                }
            }
        }
        public int Space { get; }
        public bool[,] Table { get { return table; }}

        #region //ctor
        public Grid(int width, int height)
        {
            widthGrid = width;
            heightGrid = height;
            Space = widthGrid * heightGrid;
            table = new bool[width, height];
        }
        #endregion

        public void DrawGrid(PaintEventArgs e, Grid grid, float reW,float reH)
        {
            for (int x = 2; x < grid.Width -1; x++)
            {
                e.Graphics.DrawLine(Pens.Black, x * reW + 1 - 2*reW, 24, x * reW + 1 - 2*reW, (grid.Height -3) * reH + 24);
            }
            for (int y = 3; y < grid.Height + 1; y++)
            {
                e.Graphics.DrawLine(Pens.Black, 1, y * reH + 24 - 3*reH, (grid.Width-4) * reW + 1, y * reH + 24 - 3*reH);
            }
            for (int x = 0; x < grid.Width-2; x++)
            {
                for (int y = 3; y < grid.Height; y++)
                {
                    if (grid.table[x+2, y] == true)
                        e.Graphics.DrawRectangle(Pens.Red, x * reW + 3, y * reH + 26 - 3*reH, reW-4, reH -4);
                }
            }
        }

        public void ClearGrid(Grid sender)
        {
            for (int i = 0; i < sender.Width; i++)
            {
                for (int j = 0; j < sender.Height; j++)
                {
                    Table[i, j] = false;

                }
            }
        }
    }
}
