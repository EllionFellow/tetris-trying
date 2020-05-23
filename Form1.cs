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
        private bool gamePaused = false;
        public Form form2 = new Form();
        Button exit = new Button();
        Label f2Label = new Label();

        public Form1()
        {

            KeyPreview = true;
            InitializeComponent();
            setka = new Grid(14, 23);
            resizeMultiplierH = ((Height - 43)/ (setka.Height-3));
            resizeMultiplierW = ((Width -17 - 100) / (setka.Width-4));
            ShowTheForm(this);
            form2.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form2.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            form2.ClientSize = new System.Drawing.Size(200, 200);
            form2.ControlBox = false;
            form2.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form2.Name = "Form2";
            form2.Opacity = 0D;
            form2.Text = "";
            form2.ResumeLayout(false);
            form2.PerformLayout();
            exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            exit.ForeColor = System.Drawing.Color.Snow;
            exit.Location = new System.Drawing.Point(form2.Width - 22, 1);
            exit.Margin = new System.Windows.Forms.Padding(0);
            exit.Name = "button2";
            exit.Size = new System.Drawing.Size(21, 21);
            exit.TabIndex = 0;
            exit.Text = "X";
            exit.UseVisualStyleBackColor = false;
            exit.Click += new System.EventHandler(this.Exit_Click);
            f2Label.Text = "\n \n - Press A or D to move \n - Press W to rotate \n - Press ESC to Pause \n \n - Press G to change Graphics mode";
            f2Label.AutoSize = true;
            f2Label.ForeColor = System.Drawing.Color.Snow;
            f2Label.Location = new System.Drawing.Point(1, 1);
            f2Label.Name = "f2label";
            f2Label.Size = new System.Drawing.Size(0, 13);
            form2.Controls.Add(exit);
            form2.Controls.Add(f2Label);
            form2.Paint += (sender, args) =>
            {
                args.Graphics.DrawRectangle(Pens.Gray,0,0,form2.Width-1,form2.Height-1);
            };

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            form2.Hide();

        }

        private async void ShowTheForm(Form xForm)
        {
            while (xForm.Opacity<0.99)
            {
            await Task.Delay(5);
            xForm.Opacity += 0.04;
            }
            xForm.Opacity = 1;
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
            label2.Text = $"Level {level}";
            button2.Enabled = false;
            timer1?.Dispose();
            timer1 = new System.Windows.Forms.Timer
            {
                Interval = 350-level
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
                    MessageBox.Show($"YOU LOOSE! {setka.GlobalLineCounter} Lines!", "Great", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    activeFigure = null;
                    setka.ClearGrid(setka);
                    button2.Enabled = true;
                    level = 0;
                    label2.Text = "";
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
                    if (setka.TestForClearing(setka))
                    {
                        level++;
                        label2.Text = $"Level {level}";
                    }
                    timer1.Interval = 350-level;
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
                case Keys.G:
                    setka.easyGraphicsMode = !setka.easyGraphicsMode;
                    break;
                case Keys.Escape:
                    if (activeFigure != null)
                    {
                        if (!gamePaused)
                        {
                            timer1.Stop();
                        }
                        else
                        {
                            timer1.Start();
                        }

                        gamePaused = !gamePaused;
                        setka.gamePaused = gamePaused;
                        Invalidate();
                    }

                    break;
                case Keys.B:
                    break;
            }

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            form2.Show();
            ShowTheForm(form2);
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
