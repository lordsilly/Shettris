using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace csharpshettris
{
    public partial class Form1 : Form
    {
        private new const int Width = 10;
        private new const int Height = 20;

        private char[,] canvas;                     //game playing field 2D array
        private char[,] shape;                      //shape 2D array

        private int x = 0;
        private int y = 0;

        private int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Run();
        }

        private void Run()
        {
            canvas = new char[Height, Width];        //Initialize Canvas
            for (var i = 0; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    canvas[i, j] = ' ';
            Shaper();                               //Initialize Shape
            InsertShape(x,y);
        }


        private bool IsColliding(int x, int y)
        {
            for(var i = 0; i < shape.GetLength(0); i++)
            for (var j = 0; j < shape.GetLength(1); j++)
            {
                if (shape[i, j] == '#')
                {
                    if (i + y > canvas.GetLength(0) - 1) return true;
                    if (i + y < 0) return true;
                    if (j + x > canvas.GetLength(1) - 1) return true;
                    if (j + x < 0) return true;
                    if (canvas[i + y, j + x] == '#') return true;
                }
            }
            return false;
        }

        private void PrintCanvas()
        {
            var s = "";
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++) s = s + canvas[i, j];
                s = s + Environment.NewLine;
            }
            textBox1.Text = s;
        }

        private void InsertShape(int x, int y)
        {
            for (var i = 0; i < shape.GetLength(0); i++)
                for (var j = 0; j < shape.GetLength(1); j++)
                {
                    if(shape[i,j] == '#') canvas[i + y, j + x] = '#';
                }
        }

        private void RemoveShape(int x, int y)
        {
            for (var i = 0; i < shape.GetLength(0); i++)
                for (var j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] == '#') canvas[i + y, j + x] = ' ';
                }
        }

        //*****Piece Methods*****
        private void Shaper()
        {
            var rnd = new Random();
            var ident = rnd.Next(0, 7);
            switch (ident)
            {
                default:
                    shape = new char[1, 1];
                    shape[0, 0] = '!';
                    break;
                case 0:                       //line
                    shape = new char[4, 4];
                    shape[0, 1] = '#';
                    shape[1, 1] = '#';
                    shape[2, 1] = '#';
                    shape[3, 1] = '#';
                    break;
                case 1:                       //L
                    shape = new char[3, 3];
                    shape[0, 1] = '#';
                    shape[1, 1] = '#';
                    shape[2, 1] = '#';
                    shape[2, 2] = '#';
                    break;
                case 2:                       //J
                    shape = new char[3, 3];
                    shape[0, 1] = '#';
                    shape[1, 1] = '#';
                    shape[2, 1] = '#';
                    shape[2, 0] = '#';
                    break;
                case 3:                       //Z
                    shape = new char[3, 3];
                    shape[1, 0] = '#';
                    shape[1, 1] = '#';
                    shape[2, 1] = '#';
                    shape[2, 2] = '#';
                    break;
                case 4:                       //S
                    shape = new char[3, 3];
                    shape[1, 1] = '#';
                    shape[1, 2] = '#';
                    shape[2, 1] = '#';
                    shape[2, 0] = '#';
                    break;
                case 5:                       //T
                    shape = new char[3, 3];
                    shape[1, 0] = '#';
                    shape[1, 1] = '#';
                    shape[1, 2] = '#';
                    shape[2, 1] = '#';
                    break;
                case 6:                       //Square
                    shape = new char[2, 2];
                    shape[0, 0] = '#';
                    shape[0, 1] = '#';
                    shape[1, 0] = '#';
                    shape[1, 1] = '#';
                    break;
            }
        }

        private void ShapeRotateLeft()
        {
            Transpose();
            FlipV();
        }

        private void ShapeRotateRight()
        {
            Transpose();
            FlipH();
        }

        private void Transpose()
        {
            for (var i = 0; i < shape.GetLength(0) - 1; i++)
                for (var j = i + 1; j < shape.GetLength(1); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[j, i];
                    shape[j, i] = c;
                }
        }
        private void FlipH()
        {
            for (var i = 0; i < shape.GetLength(0); i++)
                for (var j = 0; j < (shape.GetLength(1) / 2); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[i, shape.GetLength(1) - j - 1];
                    shape[i, shape.GetLength(1) - j - 1] = c;
                }
        }
        private void FlipV()
        {
            for (var i = 0; i < shape.GetLength(0) / 2; i++)
                for (var j = 0; j < (shape.GetLength(1)); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[shape.GetLength(1) - i - 1, j];
                    shape[shape.GetLength(1) - i - 1, j] = c;
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RemoveShape(x, y);
            if (!IsColliding(x, y + 1))
            {
                y++;
                InsertShape(x, y);
                PrintCanvas();
            }
            else
            {
                InsertShape(x, y);
                Score();
                PrintCanvas();
                Shaper();
                y = 0;
                x = 0;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            RemoveShape(x,y);
            if (e.KeyCode == Keys.A) if (!IsColliding(x - 1, y)) x--;
            if (e.KeyCode == Keys.D) if (!IsColliding(x + 1, y)) x++;
            if (e.KeyCode == Keys.W)
            {
                ShapeRotateRight();
                if (IsColliding(x, y)) ShapeRotateLeft();
            }
            if (e.KeyCode == Keys.S)
            {
                if (!IsColliding(x, y + 1))
                {
                    y++;
                }
                else
                {
                    InsertShape(x, y);
                    PrintCanvas();
                    Shaper();
                    y = 0;
                    x = 0;
                }
            }
            InsertShape(x,y);
            PrintCanvas();
        }

        private void Score()
        {
            var lines = 0;
            for (var i = 0; i < canvas.GetLength(0); i++)
            {
                var line = true;
                for (var j = 0; j < canvas.GetLength(1); j++)
                {
                    if (canvas[i, j] != '#')
                    {
                        line = false;
                        break;
                    }
                }
                if (line)
                {
                    lines++;
                    for (var j = 0; j < canvas.GetLength(1); j++) canvas[i, j] = ' ';
                    for (var j = i; j > 0; j--)
                    {
                        for (var k = 0; k < canvas.GetLength(1); k++)
                        {
                            canvas[j, k] = canvas[j - 1, k];
                            canvas[j - 1, k] = ' ';
                        }
                    }
                }
            }
            switch (lines)
            {
                case 0:
                    break;
                case 1:
                    score += 1000;
                    break;
                case 2:
                    score += 4000;
                    break;
                case 3:
                    score += 8000;
                    break;
                case 4:
                    score += 16000;
                    break;
            }
            textBox2.Text = score.ToString();
        }
    }
}
