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
            ComplexNumber complexNumber = CreateComplexNumber(x, y);

            var (result, iterations) = FindSolutionOfEquation(complexNumber, polynomial, derivative, roots);
            int id = FindRootId(result, roots);






            return CalculatePixelColor(id, iterations, colors);

        }

        private static int FindRootId(ComplexNumber complexNumber, List<ComplexNumber> roots)
        {
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
            return id;

        }

        private static (ComplexNumber result, float iteration) FindSolutionOfEquation(ComplexNumber complexNumber, Polynome polynomial, Polynome derivative, List<ComplexNumber> roots)
        {
            // find solution of equation using newton's iteration
            const int requiredIterations = 30;
            float iteration = 0;
            for (int i = 0; i < requiredIterations; i++)
            {
                var diff = polynomial.Eval(complexNumber).Divide(derivative.Eval(complexNumber));
                complexNumber = complexNumber.Subtract(diff);

                if (Math.Pow(diff.RealNumber, 2) +
                    Math.Pow(diff.ImaginaryNumber, 2) >= 0.5)
                {
                    i--;
                }
                iteration++;
            }
            return (complexNumber, iteration);


        }



        private static ComplexNumber CreateComplexNumber(double x, double y)
        {
            ComplexNumber complexNumber = new ComplexNumber()
            {
                RealNumber = x,
                ImaginaryNumber = (float)y
            };

            if (complexNumber.RealNumber == 0)
                complexNumber.RealNumber = 0.0001;
            if (complexNumber.ImaginaryNumber == 0)
                complexNumber.ImaginaryNumber = 0.0001f;

            return complexNumber;
        }

        private static void ComputeNewtonFractal(
            int width, int height, Bitmap bitmap,
            double xMin, double yMin, double xMax,
            double yMax, double xStep, double yStep,
            Polynome polynomeP, Polynome polynomePD,
            Color[] colors)
        {

            List<ComplexNumber> roots = new List<ComplexNumber>();

            Console.WriteLine(polynomeP);
            Console.WriteLine(polynomePD);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = yMin + i * yStep;
                    double x = xMin + j * xStep;

                    var color = ComputePixelColor(
                        x, y, polynomeP, polynomePD, roots, colors);
                    bitmap.SetPixel(j, i, color);

                }
            }

        }

        private static Color CalculatePixelColor(int rootId, float iterations, Color[] colors)
        {
            var pixelColor = colors[rootId % colors.Length];
            pixelColor = Color.FromArgb(pixelColor.R, pixelColor.G, pixelColor.B);
            pixelColor = Color.FromArgb(
                Math.Min(Math.Max(0, pixelColor.R - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, pixelColor.G - (int)iterations * 2), 255),
                Math.Min(Math.Max(0, pixelColor.B - (int)iterations * 2), 255));

            return pixelColor;
        }

        private static void SaveImageResult(string output, Bitmap bitmap)
        {
            bitmap.Save(output ?? "../../../out.png");
        }
    }

}
