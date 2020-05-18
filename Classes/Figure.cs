using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTry.Classes
{
    class Figure
    {
        private static readonly bool[,] figureI =
        {
            {false, false, false, true},
            {false, false, false, true},
            {false, false, false, true},
            {false, false, false, true}
        };

        private static readonly bool[,] figureI1 =
        {
            {false, false, false, false},
            {true, true, true, true},
            {false, false, false, false},
            {false, false, false, false}
        };

        private bool[,] figure;
        private bool figureDropped;

        public bool[,] Table
        {
            get => figure;
            set
            {
                int counter = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (value[i, j])
                            counter++;
                    }
                }

                if (counter == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            figure[i, j] = value[j, i];
                        }

                    }
                }

            }
        }

        private int x, y;

        public int X
        {
            get => x;
            set
            {
                if (value >= 0)
                    x = value;
            }
        }

        public int Y
        {
            get => y;
            set
            {
                if (value >= 0)
                    y = value;
            }
        }

        public void ClearLastFigure(Grid setka, Figure activeFigure)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    setka.Table[i + activeFigure.X, j + activeFigure.Y] = false;
                }
            }
        }

        public Figure(int left, int top)
        {
            figure = new bool[4, 4];
            Random random = new Random();
            switch (random.Next(2))
            {
                case 0:
                    figure = figureI;
                    break;
                case 1:
                    figure = figureI1;
                    break;
            }

            x = left;
            y = top;
        }

        public void FigureInGrid(Grid grid, Figure fig)
        {
                for (int i = 0; i < 4; i++)
                {
                    if (!figureDropped)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (10 < i + fig.X || 20 < j + fig.Y)
                            {
                                figureDropped = true;
                                break;
                            }
                            else
                            {

                                if (!grid.Table[i + fig.X, j + fig.Y])
                                {
                                    if (fig.Table[i, j])
                                    {
                                        grid.Table[i + fig.X, j + fig.Y] = true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (figureDropped)
                {
                    fig = null;
                }
        }
    }
}

