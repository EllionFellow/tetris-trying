using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTry.Classes
{
    class Figure
    {
        private int typeOfFigure { get; set; }
        private bool[,] figureI1 =
        {
            {false, false, false, false},
            {true, true, true, true},
            {false, false, false, false},
            {false, false, false, false}
        };
        private bool[,] figureI2 =
        {
            {false, false, false, true},
            {false, false, false, true},
            {false, false, false, true},
            {false, false, false, true}
        };
        private bool[,] figureL3 =
        {
            {false, false, false, false},
            {false, true, true, true},
            {false, false, false, true},
            {false, false, false, false}
        };
        private bool[,] figureL4 =
        {
            {false, false, false, false},
            {false, false, true, true},
            {false, false, true, false},
            {false, false, true, false}
        };
        private bool[,] figureL5 =
{
            {false, false, false, false},
            {false, true, false, false},
            {false, true, true, true},
            {false, false, false, false}
        };
        private bool[,] figureL6 =
        {
            {false, false, false, true},
            {false, false, false, true},
            {false, false, true, true},
            {false, false, false, false}
        };


        private bool[,] figure;
        public bool figureDropped;

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
                    if (activeFigure.Table[i, j] == true)
                    {
                        setka.Table[i + activeFigure.X, j + activeFigure.Y] = false;
                    }
                }
            }
        }

        public Figure(int left, int top)
        {
            figure = new bool[4, 4];
            Random random = new Random();
            switch (random.Next(0,6))
            {
                case 0:
                    СhangeTheInnerForm(this, figureI1);
                    typeOfFigure = 1;
                    break;
                case 1:
                    СhangeTheInnerForm(this, figureI2);
                    typeOfFigure = 2;
                    break;
                case 2:
                    СhangeTheInnerForm(this, figureL3);
                    typeOfFigure = 3;
                    break;
                case 3:
                    СhangeTheInnerForm(this, figureL4);
                    typeOfFigure = 4;
                    break;
                case 4:
                    СhangeTheInnerForm(this, figureL5);
                    typeOfFigure = 5;
                    break;
                case 5:
                    СhangeTheInnerForm(this, figureL6);
                    typeOfFigure = 6;
                    break;
            }

            x = left;
            y = top;
        }

        public void FigureInGrid(Grid grid, Figure fig)
        {
            if (!figureDropped)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (fig.Y + j >= grid.Height)
                        {
                            figureDropped = true;
                            break;
                        }
                        else
                        {
                            if (fig.Table[i, j])
                                grid.Table[i + fig.X, j + fig.Y] = true;
                        }
                    }
                }
            }
        }

        public void СhangeTheInnerForm(Figure xFig, bool[,] nextForm)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    xFig.Table[i, j] = nextForm[i, j];
                }
            }
        }

        public Figure Rotate(Figure xfig, Grid grid)
        {
            bool real=true;
            Figure temp = xfig;
            switch (temp.typeOfFigure)
            {
                case 1:
                     СhangeTheInnerForm(temp, figureI2);
                    temp.typeOfFigure = 2;
                    break;
                case 2:
                    СhangeTheInnerForm(temp, figureI1);
                    temp.typeOfFigure = 1;
                    break;
                case 3:
                    СhangeTheInnerForm(temp, figureL4);
                    temp.typeOfFigure = 4;
                    break;
                case 4:
                    СhangeTheInnerForm(temp, figureL5);
                    temp.typeOfFigure = 5;
                    break;
                case 5:
                    СhangeTheInnerForm(temp, figureL6);
                    temp.typeOfFigure = 6;
                    break;
                case 6:
                    СhangeTheInnerForm(temp, figureL3);
                    temp.typeOfFigure = 3;
                    break;
                default:
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                if (real)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (temp.Table[i, j] && grid.Table[i + xfig.X, j + xfig.Y])
                        {
                            real = false;
                            break;
                        }
                    }
                }
            }
            if (real)
            {
                return temp;
            }

            return xfig;
        }

        public bool IsNextStepReal(Figure xfig, Grid grid, int moveArrow)
        {
            bool nextStepReal = true;
            int[] l = new int[4];
            int[] t = new int[4];
            int counter=0;

            // Locating coordinates of TRUE in figure
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (xfig.Table[i, j] == true)
                    {
                        l[counter] = i;
                        t[counter] = j;
                        counter++;
                    }
                }
            }

            // Move Down
            if (moveArrow == 0)
            {
                // Testing if max value of grid Y is lower then figure
                if (xfig.Y + 5 > grid.Height)
                {
                    nextStepReal = false;
                }
                else
                {
                    // Testing for TRUE in Grid lower then true in figure
                    for (int i = 0; i < 4; i++)
                    {
                            if (grid.Table[xfig.X + l[i], t[i] + 1 + xfig.Y] == false)
                            {
                                nextStepReal = true;
                            }
                            else
                            {
                                nextStepReal = false;
                                break;
                            }
                        
                    }
                }
            }

            // Move Left
            if (moveArrow == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (grid.Table[xfig.X + l[i] - 1, t[i] + xfig.Y] == false && xfig.X + l[i] > 2)
                    {
                        nextStepReal = true;
                    }
                    else
                    {
                        nextStepReal = false;
                        break;
                    }


                }
            }
            // Move Right
            if (moveArrow == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (grid.Table[xfig.X + l[i] + 1, t[i] + xfig.Y] == false && xfig.X + l[i] < (grid.Width-3))
                    {
                        nextStepReal = true;
                    }
                    else
                    {
                        nextStepReal = false;
                        break;
                    }


                }
            }
            return nextStepReal;
            
        }
    }
}

