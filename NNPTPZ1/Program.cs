using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int width;
            int height;
            Bitmap bitmap;
            double xMin;
            double yMin;
            double xMax;
            double yMax;
            double xStep;
            double yStep;
            Polynome polynomeP;
            Polynome polynomePD;
            Color[] colors;

            width = int.Parse(args[0]);
            height = int.Parse(args[1]);

            bitmap = new Bitmap(width, height);

            xMin = double.Parse(args[2]);
            xMax = double.Parse(args[3]);
            yMin = double.Parse(args[4]);
            yMax = double.Parse(args[5]);

            xStep = (xMax - xMin) / width;
            yStep = (yMax - yMin) / height;

            polynomeP = CreatePolynome();
            polynomePD = polynomeP.Derive();
            colors = CreateColors();
            string output = args[6];





            ComputeNewtonFractal(
                width, height, bitmap,
                xMin, yMin, xMax, yMax, xStep, yStep,
                polynomeP, polynomePD, colors);
            SaveImageResult(output, bitmap);


        }

        private static Color[] CreateColors()
        {
            return new Color[]
            {
            Color.Red,

            Color.Blue,
            Color.Green,
            Color.Yellow,
            Color.Orange,
            Color.Fuchsia,
            Color.Gold,
            Color.Cyan,
            Color.Magenta
            };

        }

        private static Polynome CreatePolynome()
        {
            Polynome polynome = new Polynome();

            polynome.Coefficient.Add(new ComplexNumber() { RealNumber = 1 });
            polynome.Coefficient.Add(ComplexNumber.Zero);
            polynome.Coefficient.Add(ComplexNumber.Zero);
            polynome.Coefficient.Add(new ComplexNumber() { RealNumber = 1 });
            return polynome;
        }

        private static Color ComputePixelColor(double x, double y, Polynome polynomial, Polynome derivative, List<ComplexNumber> roots, Color[] colors)
        {
            ComplexNumber complexNumber = new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryNumber = (float)(y)
            };

            if (complexNumber.RealNumber == 0)
                complexNumber.RealNumber = 0.0001;
            if (complexNumber.ImaginaryNumber == 0)
                complexNumber.ImaginaryNumber = 0.0001f;

            // find solution of equation using newton's iteration
            float it = 0;
            for (int i = 0; i < 30; i++)
            {
                var diff = polynomial.Eval(complexNumber).Divide(derivative.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealNumber, 2) +
                    Math.Pow(diff.ImaginaryNumber, 2) >= 0.5)
                {
                    i--;
                }
                it++;
            }

            var known = false;
            var id = 0;

            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(complexNumber.RealNumber - roots[i].RealNumber, 2) +
                    Math.Pow(complexNumber.ImaginaryNumber - roots[i].ImaginaryNumber, 2) <= 0.01)
                {
                    known = true;
                    id = i;
                }
            }
            if (!known)
            {
                roots.Add(complexNumber);
                id = roots.Count;
            }

            var vv = colors[id % colors.Length];
            vv = Color.FromArgb(vv.R, vv.G, vv.B);
            vv = Color.FromArgb(
                Math.Min(Math.Max(0, vv.R - (int)it * 2), 255),
                Math.Min(Math.Max(0, vv.G - (int)it * 2), 255),
                Math.Min(Math.Max(0, vv.B - (int)it * 2), 255));

            return vv;

        }

        private static void ComputeNewtonFractal(
            int width, int height, Bitmap bitmap,
            double xMin, double yMin, double xMax,
            double yMax, double xStep, double yStep,
            Polynome polynomeP, Polynome polynomePD,
            Color[] colors)
        {



            double xmin = xMin;
            double xmax = xMax;
            double ymin = yMin;
            double ymax = yMax;

            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Polynome polynome = new Polynome();
            polynome.Coefficient.Add(new ComplexNumber() { RealNumber = 1 });
            polynome.Coefficient.Add(ComplexNumber.Zero);
            polynome.Coefficient.Add(ComplexNumber.Zero);
            polynome.Coefficient.Add(new ComplexNumber() { RealNumber = 1 });
            Polynome ptmp = polynome;
            Polynome derivative = polynome.Derive();

            Console.WriteLine(polynome);
            Console.WriteLine(derivative);



            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    var color = ComputePixelColor(
                        x, y, polynome, derivative, roots, colors);
                    bitmap.SetPixel(j, i, color);

                }
            }

        }

        private static void SaveImageResult(string output, Bitmap bitmap)
        {
            bitmap.Save(output ?? "../../../out.png");
        }
    }

}
