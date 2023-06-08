using System;
using System.Collections.Generic;
using System.Text;
using Banico_DataStruct_FinalProject.XSimplePolynomialSolver;
using NUnit.Framework;
using FluentAssertions;

namespace Banico_DataStruct_FinalProject.Tests
{
    class SPSTest
    {
        [TestCase("1+3x^2+2x")]
        [TestCase("3x^2-1+2x")]
        [TestCase("-2x+3x^2+1")]
        [TestCase("-2x-1-3x^2")]
        [TestCase("-2x-1-3x^2-2y-1-3y^2")]
        public void StringToList_ThreeTerms_Test(string expression)
        {
            var x = new SPSMethodClass();
            var y = x.PutToList(expression);
            var z = x.SortPolynomial(y);

            foreach (var data in z)
            {
                Console.WriteLine(data.ToString());
            }
        }

        [TestCase("+012345")]
        public void IntConverter(string num)
        {
            var strData = num.Replace("+", "");
            Console.WriteLine(strData);
        }

        [TestCase("(x^2+1)", "(y^2+x^3+1)")]
        [TestCase("x^3-1", "x^4+3y^3-1")]
        [TestCase("(2+1)x^3-1", "x^4+(3*5)y^3-1")]
        [TestCase("(1*3)x^(3-1)", "(8-5)x^(5-3)")]

        public void polynomialAddition_Test(string eq1, string eq2)
        {
            var x = new SPSMethodClass();
            var a = x.PutToList(eq1);
            var b = x.PutToList(eq2);
            a.AddRange(b);
            a = x.SolveCoefficientInList(a);
            var y = x.PolynomialAddition(a);


            foreach (var VARIABLE in y)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        [TestCase("x^2+1", "x^2+x^3-1")]
        [TestCase("x^3-1", "x^4+3x^3+1")]
        public void PolynomialSubtraction_Test(string eq1, string eq2)
        {
            var x = new SPSMethodClass();
            var a = x.PutToList(eq1);
            var b = x.PutToList(eq2);
            var y = x.PolynomialSubtraction(a, b);

            foreach (var VARIABLE in y)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        [TestCase("2x^2+1", "x^2-1")]
        [TestCase("2x-y", "x+2y")]
        [TestCase("x+2y","2x+y")]
        [TestCase("x-y", "x-y")]
        [TestCase("2x-y+1", "x+2y-1")]
        [TestCase("x^2-1", "y^2-1")]
        public void PolynomialMultiplication_Test(string eq1, string eq2)
        {
            var x = new SPSMethodClass();
            var a = x.SortPolynomial(x.PutToList(eq1));
            var b = x.SortPolynomial(x.PutToList(eq2));
            var y = x.PolynomialMultiplication(a, b);

            Console.WriteLine(y);
        }

        [TestCase("x^3-1", "x^4+3x^3+1")]
        [TestCase("x-1", "x-1")]
        public void PolynomialDivision_Test(string eq1, string eq2)
        {
            var x = new SPSMethodClass();
            var a = x.SortPolynomial(x.PutToList(eq1));
            var b = x.SortPolynomial(x.PutToList(eq2));
            var y = x.PolynomialDivision(a, b);

            Console.WriteLine(y);
        }
    }
}
