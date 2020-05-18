using MyTry.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTry
{
    public partial class Form1 : Form
    {
        float resizeMultiplierH, resizeMultiplierW;

        Grid setka;
        public Form1()
        {
            InitializeComponent();
            setka = CreateGrid(20, 10);
            resizeMultiplierH = ((Height - 43)/ setka.Height);
            resizeMultiplierW = ((Width -17 - 100) / setka.Width);
        }

        private Grid CreateGrid(int h, int w)
        {
            Grid grid = new Grid(w, h);
            return grid;
        }

        

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeMultiplierH = ((Height - 43) / setka.Height);
            resizeMultiplierW = ((Width - 17 -100) / setka.Width);
            Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        Figure activeFigure;
        private void Button2_Click(object sender, EventArgs e)
        {
            setka.ClearGrid(setka);
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s,x) =>
                {
                    if (activeFigure==null)
                    {
                        activeFigure = new Figure(3, 0);
                        activeFigure.FigureInGrid(setka, activeFigure);
                    }
                    else
                    {
                        activeFigure.ClearLastFigure(setka,activeFigure);
                        activeFigure.Y++;
                        activeFigure.FigureInGrid(setka, activeFigure);
                    }
                    Invalidate();
                    
                };
        }

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            setka.DrawGrid(e, setka, resizeMultiplierW, resizeMultiplierH);
        }



        #region //moveForm

        private int x, y;
        private bool isMouseDown=false;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            isMouseDown = true;
        }



        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                this.Location = new Point(Location.X + e.X - x, Location.Y + e.Y - y);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }
        

        #endregion

    }
}
