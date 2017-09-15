using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace csharpshettris
{
    class Shape
    {
        public char[,] shape;

        public void RotateLeft()
        {
            Transpose();
            FlipV();
        }

        public void RotateRight()
        {
            Transpose();
            FlipH();
        }

        public void Transpose()
        {
            for (var i = 0; i < shape.GetLength(0) - 1; i++)
                for (var j = i + 1; j < shape.GetLength(1); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[j, i];
                    shape[j, i] = c;
                }
        }
        public void FlipH()
        {
            for (var i = 0; i < shape.GetLength(0); i++)
                for (var j = 0; j < (shape.GetLength(1) / 2); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[i, shape.GetLength(1) - j - 1];
                    shape[i, shape.GetLength(1) - j - 1] = c;
                }
        }
        public void FlipV()
        {
            for (var i = 0; i < shape.GetLength(0) / 2; i++)
                for (var j = 0; j < (shape.GetLength(1)); j++)
                {
                    var c = shape[i, j];
                    shape[i, j] = shape[shape.GetLength(1) - i - 1, j];
                    shape[shape.GetLength(1) - i - 1, j] = c;
                }
        }
    }

    class LineShape : Shape
    {
        public LineShape()
        {
            shape = new char[4, 4];
            shape[0, 1] = '#';
            shape[1, 1] = '#';
            shape[2, 1] = '#';
            shape[3, 1] = '#';
        }
    }

    class LShape : Shape
    {
        public LShape()
        {
            shape = new char[3, 3];
            shape[0, 1] = '#';
            shape[1, 1] = '#';
            shape[2, 1] = '#';
            shape[2, 2] = '#';
        }
    }

    class JShape : Shape
    {
        public JShape()
        {
            shape = new char[3, 3];
            shape[0, 1] = '#';
            shape[1, 1] = '#';
            shape[2, 1] = '#';
            shape[2, 0] = '#';
        }
    }

    class ZShape : Shape
    {
        public ZShape()
        {
            shape = new char[3, 3];
            shape[1, 0] = '#';
            shape[1, 1] = '#';
            shape[2, 1] = '#';
            shape[2, 2] = '#';
        }
    }

    class SShape : Shape
    {
        public SShape()
        {
            shape = new char[3, 3];
            shape[1, 1] = '#';
            shape[1, 2] = '#';
            shape[2, 1] = '#';
            shape[2, 0] = '#';
        }
    }

    class TShape : Shape
    {
        public TShape()
        {
            shape = new char[3, 3];
            shape[1, 0] = '#';
            shape[1, 1] = '#';
            shape[1, 2] = '#';
            shape[2, 1] = '#';
        }
    }

    class SquareShape : Shape
    {
        public SquareShape()
        {
            shape = new char[2, 2];
            shape[0, 0] = '#';
            shape[0, 1] = '#';
            shape[1, 0] = '#';
            shape[1, 1] = '#';
        }
    }
}
