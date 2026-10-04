using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    namespace Mathematics
    {
        public class Polynome
        {

            public List<ComplexNumber> Coefficient { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public Polynome() => Coefficient = new List<ComplexNumber>();

            public void Add(ComplexNumber coe) =>
                Coefficient.Add(coe);

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynome Derive()
            {
                Polynome derivative = new Polynome();
                for (int i = 1; i < Coefficient.Count; i++)
                {
                    derivative.Coefficient.Add(Coefficient[i].Multiply(new ComplexNumber() { RealNumber = i }));
                }

                return derivative;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(double x)
            {
                var y = Eval(new ComplexNumber() { RealNumber = x, ImaginaryNumber = 0 });
                return y;
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="value">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Eval(ComplexNumber value)
            {
                ComplexNumber result = ComplexNumber.Zero;
                for (int i = 0; i < Coefficient.Count; i++)
                {
                    ComplexNumber coefficient = Coefficient[i];
                    ComplexNumber poweredValue = value;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            poweredValue = poweredValue.Multiply(value);

                        coefficient = coefficient.Multiply(poweredValue);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string s = "";
                int i = 0;
                for (; i < Coefficient.Count; i++)
                {
                    s += Coefficient[i];
                    if (i > 0)
                    {
                        int j = 0;
                        for (; j < i; j++)
                        {
                            s += "x";
                        }
                    }
                    if (i + 1 < Coefficient.Count)
                        s += " + ";
                }
                return s;
            }
        }

    }

}
