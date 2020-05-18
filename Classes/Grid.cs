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
            for (int x = 0; x < grid.Width + 1; x++)
            {
                e.Graphics.DrawLine(Pens.Black, x * reW + 1, 0.0f + 1+23, x * reW + 1, grid.Height * reH + 1+23);
            }
            for (int y = 0; y < grid.Height + 1; y++)
            {
                e.Graphics.DrawLine(Pens.Black, 0.0f + 1, y * reH + 1+23, grid.Width * reW + 1, y * reH + 1+23);
            }
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (grid.table[x, y] == true)
                        e.Graphics.DrawRectangle(Pens.Red, x * reW + 3, y * reH + 26, reW-4, reH -4);
                }
            }
        }

    }
}
