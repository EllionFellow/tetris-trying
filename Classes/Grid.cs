using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyTry.Properties;

namespace MyTry.Classes
{
    class Grid
    {
        #region //container
        private int widthGrid;
        private int heightGrid;
        public int GlobalLineCounter=0;
        public bool gamePaused;
        public bool easyGraphicsMode;
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

        
        public bool TestForClearing(Grid setka)
        {
            bool toReturn = false;
            int counter = 0;
            for (int j = 4; j < setka.Height; j++)
            {
                for (int i = 2; i <= setka.Width - 2; i++)
                {
                    if (setka.Table[i, j])
                        counter++;
                }

                if (counter == setka.Width - 4)
                {
                    toReturn = true;
                    GlobalLineCounter++;
                    for (int i = 0; i < setka.Width-4; i++)
                    {
                        setka.Table[i + 2, j] = false;
                    }
                    for (int o = j; o > 0 ; o--)
                    {
                        for (int k = 2; k < setka.Width-2; k++)
                        {
                            setka.Table[k, o] = setka.Table[k, o - 1];
                        }
                        
                    }
                }

                counter = 0;
            }
            return toReturn;
        }

        public int Height
        {
            get => heightGrid;
            set
            {
                if (value > 0 && value < 100)
                {
                    heightGrid = value;
                }
            }
        }
        public int Space { get; }
        public bool[,] Table { get; }

        #region //ctor
        public Grid(int width, int height)
        {
            widthGrid = width;
            heightGrid = height;
            Space = widthGrid * heightGrid;
            Table = new bool[width, height];
            gamePaused = false;
            easyGraphicsMode = false;
        }
        #endregion

        public void DrawGrid(PaintEventArgs e, Grid grid, float reW,float reH)
        {
            for (int x = 2; x < grid.Width -1; x++)
            {
                e.Graphics.DrawLine(Pens.Gray, x * reW + 3 - 2*reW, 24, x * reW + 3 - 2*reW, (grid.Height -3) * reH + 24);
            }
            for (int y = 3; y < grid.Height + 1; y++)
            {
                e.Graphics.DrawLine(Pens.Gray, 3, y * reH + 24 - 3*reH, (grid.Width-4) * reW + 3, y * reH + 24 - 3*reH);
            }
            for (int x = 0; x < grid.Width-2; x++)
            {
                for (int y = 3; y < grid.Height; y++)
                {
                    if (grid.Table[x + 2, y] == true)
                    {
                        


                        if (gamePaused)
                        {
                                e.Graphics.DrawString("PAUSED", new Font("Tahoma", 32.0f), Brushes.Gray, 10, ((grid.Height / 2 - 1) * reW)-3);
                        }
                        else
                        {
                            if (easyGraphicsMode)
                            {
                                e.Graphics.DrawRectangle(Pens.Red, x * reW + 5, y * reH + 26 - 3 * reH, reW - 4, reH - 4);
                            }
                            else
                            {
                                Rectangle rect = new Rectangle(x * (int) reW + 5, y * (int) reH + 26 - 3 * (int) reH, (int) reW - 2, (int) reH - 2);
                                e.Graphics.DrawIcon(Resources._1, rect);
                            }
                        }
                    }
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
