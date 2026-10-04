using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNPTPZ1;

namespace NNPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class CplxTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber a = new ComplexNumber()
            {
                RealNumber = 10,
                ImaginaryNumber = 20
            };
            ComplexNumber b = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryNumber = 2
            };

            ComplexNumber actual = a.Add(b);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                RealNumber = 11,
                ImaginaryNumber = 22
            };

            Assert.AreEqual(shouldBe, actual);

            var e2 = "(10 + 20i)";
            var r2 = a.ToString();
            Assert.AreEqual(e2, r2);
            e2 = "(1 + 2i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);

            a = new ComplexNumber()
            {
                RealNumber = 1,
                ImaginaryNumber = -1
            };
            b = new ComplexNumber() { RealNumber = 0, ImaginaryNumber = 0 };
            shouldBe = new ComplexNumber() { RealNumber = 1, ImaginaryNumber = -1 };
            actual = a.Add(b);
            Assert.AreEqual(shouldBe, actual);

            e2 = "(1 + -1i)";
            r2 = a.ToString();
            Assert.AreEqual(e2, r2);

            e2 = "(0 + 0i)";
            r2 = b.ToString();
            Assert.AreEqual(e2, r2);
        }

        [TestMethod()]
        public void AddTestPolynome()
        {
            Polynome poly = new Mathematics.Polynome();
            poly.Coefficient.Add(new ComplexNumber() { RealNumber = 1, ImaginaryNumber = 0 });
            poly.Coefficient.Add(new ComplexNumber() { RealNumber = 0, ImaginaryNumber = 0 });
            poly.Coefficient.Add(new ComplexNumber() { RealNumber = 1, ImaginaryNumber = 0 });
            ComplexNumber result = poly.Eval(new ComplexNumber() { RealNumber = 0, ImaginaryNumber = 0 });
            var expected = new ComplexNumber() { RealNumber = 1, ImaginaryNumber = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealNumber = 1, ImaginaryNumber = 0 });
            expected = new ComplexNumber() { RealNumber = 2, ImaginaryNumber = 0 };
            Assert.AreEqual(expected, result);
            result = poly.Eval(new ComplexNumber() { RealNumber = 2, ImaginaryNumber = 0 });
            expected = new ComplexNumber() { RealNumber = 5.0000000000, ImaginaryNumber = 0 };
            Assert.AreEqual(expected, result);

            var r2 = poly.ToString();
            var e2 = "(1 + 0i) + (0 + 0i)x + (1 + 0i)xx";
            Assert.AreEqual(e2, r2);
        }
    }
}


