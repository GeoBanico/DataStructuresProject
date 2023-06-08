using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Automation.Text;
using NUnit.Framework;
using Banico_DataStruct_FinalProject.NumericalEquations;
using Banico_DataStruct_FinalProject.PolynomialEquations;
using FluentAssertions;

namespace Banico_DataStruct_FinalProject.Tests
{
    class NumTest
    {
        [TestCase("-(8+5)",-13)]
        [TestCase("2^(2+2)", 16)]
        public void NumExpressionTest(string expression, double ans)
        {
            var NumCompiler = new ArithmeticCompiler();
            var expAns = NumCompiler.CompileArthmetic(expression);

            expAns.Should().Be(ans);
        }

        [TestCase("x+3")]
        [TestCase("3x^2+2x+1")]
        [TestCase("3x^2+2x-1")]
        [TestCase("-1+2x-3x^2")]
        [TestCase("-1*2x/3x^2")]
        [TestCase("-1*2x/3x^2-1+2x-3x^2")]
        [TestCase("(-1-1+2x)*(2x-3x^2)/3x^2")]
        public void PolyExpressionTest(string expression)
        {
            var polyComp = new PolynomialDigitParse();
            var ans = polyComp.PolynomialSolver(expression);

            while (ans.Count > 0)
            {
                Console.Write(ans.Dequeue());
            }
        }
    }
}
