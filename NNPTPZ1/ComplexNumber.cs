using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class ComplexNumber
    {

        public double RealNumber { get; set; }
        public float ImaginaryNumber { get; set; } //is float correct?

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.RealNumber == RealNumber && x.ImaginaryNumber == ImaginaryNumber;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealNumber = 0,
            ImaginaryNumber = 0
        };


        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealNumber * RealNumber + ImaginaryNumber * ImaginaryNumber);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealNumber = a.RealNumber + b.RealNumber,
                ImaginaryNumber = a.ImaginaryNumber + b.ImaginaryNumber
            };
        }

        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealNumber = a.RealNumber - b.RealNumber,
                ImaginaryNumber = a.ImaginaryNumber - b.ImaginaryNumber
            };
        }

        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealNumber = a.RealNumber * b.RealNumber - a.ImaginaryNumber * b.ImaginaryNumber,
                ImaginaryNumber = (float)(a.RealNumber * b.ImaginaryNumber + a.ImaginaryNumber * b.RealNumber)
            };
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            var tmp = this.Multiply(new ComplexNumber() { RealNumber = b.RealNumber, ImaginaryNumber = -b.ImaginaryNumber });
            var denominator = b.RealNumber * b.RealNumber + b.ImaginaryNumber * b.ImaginaryNumber;

            return new ComplexNumber()
            {
                RealNumber = tmp.RealNumber / denominator,
                ImaginaryNumber = (float)(tmp.ImaginaryNumber / denominator)
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryNumber / RealNumber);
        }


        public override string ToString()
        {
            return $"({RealNumber} + {ImaginaryNumber}i)";
        }


    }


}

