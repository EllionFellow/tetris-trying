using MyTry.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTry
{

    public partial class Form1 : Form
    {
        
        int level = 0;
        float resizeMultiplierH, resizeMultiplierW;
        System.Windows.Forms.Timer timer1;
        Grid setka;
        Figure activeFigure;
        public Form1()
        {

            KeyPreview = true;
            InitializeComponent();
            setka = new Grid(14, 23);
            resizeMultiplierH = ((Height - 43)/ (setka.Height-3));
            resizeMultiplierW = ((Width -17 - 100) / (setka.Width-4));
            ShowTheForm();
        }

        private async void ShowTheForm()
        {
            while (Opacity<0.99)
            {
            await Task.Delay(5);
            Opacity += 0.04;
            }
            Opacity = 1;
        }

        private void CloseTheForm()
        {
            while (Opacity > 0.05)
            {
                Thread.Sleep(15);
                Opacity -= 0.04;
            }
            Opacity = 0;
        }


        

        private void Form1_Resize(object sender, EventArgs e)
        {
            resizeMultiplierH = (Height - 43) / (setka.Height - 3);
            resizeMultiplierW = (Width - 17 -100) / (setka.Width - 4);
            Invalidate();
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void Button2_Click(object sender, EventArgs e)
        {
            setka.GlobalLineCounter = 0;
            button2.Enabled = false;
            timer1?.Dispose();
            timer1 = new System.Windows.Forms.Timer
            {
                Interval = 250-level
            };
            activeFigure = null;
            timer1.Stop();
            setka.ClearGrid(setka);
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += FigureFall;
        }

        private void FigureFall(Object sender, EventArgs e)
        {
            if (activeFigure == null)
            {
                activeFigure = new Figure((setka.Width - 4)/2, 0);
                if (activeFigure.IsNextStepReal(activeFigure, setka, 0))
                {
                    activeFigure.FigureInGrid(setka, activeFigure);
                }
                else
                {
                    activeFigure.FigureInGrid(setka, activeFigure);
                    Invalidate();
                    timer1.Dispose();
                    MessageBox.Show("YOU LOOSE!", "Great", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    activeFigure = null;
                    setka.ClearGrid(setka);
                    button2.Enabled = true;
                }
            }
            else
            {
                activeFigure.ClearLastFigure(setka, activeFigure);
                if (activeFigure.IsNextStepReal(activeFigure, setka, 0))
                {
                    activeFigure.Y++;
                    activeFigure.FigureInGrid(setka, activeFigure);
                }
                else
                {
                    activeFigure.FigureInGrid(setka, activeFigure);
                    activeFigure = null;
                    setka.TestForClearing(setka);
                    timer1.Interval = 250-level;
                }
            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = setka.GlobalLineCounter.ToString();
            e.Graphics.DrawRectangle(Pens.Gray,0,0,Width-1,Height-1);
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseTheForm();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.S:
                    if (activeFigure != null)
                        timer1.Interval = 10;
                    break;
                case Keys.A:
                    if (activeFigure != null)
                    {
                        activeFigure.ClearLastFigure(setka, activeFigure);
                        if (activeFigure.IsNextStepReal(activeFigure, setka, 1))
                            activeFigure.X--;
                        activeFigure.FigureInGrid(setka, activeFigure);
                        Invalidate();
                    }
                    break;
                case Keys.D:
                    if (activeFigure != null)
                    {
                        activeFigure.ClearLastFigure(setka, activeFigure);
                        if (activeFigure.IsNextStepReal(activeFigure, setka, 2))
                            activeFigure.X++;
                        activeFigure.FigureInGrid(setka, activeFigure);
                        Invalidate();
                    }
                    break;
                case Keys.W:
                    if (activeFigure != null)
                    {
                        activeFigure.ClearLastFigure(setka, activeFigure);
                        activeFigure = activeFigure.Rotate(activeFigure, setka);
                        activeFigure.FigureInGrid(setka, activeFigure);
                        Invalidate();
                    }
                    break;
                case Keys.B:
                    break;
                default:
                    break;
            }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
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
