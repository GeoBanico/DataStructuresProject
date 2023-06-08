using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using Banico_DataStruct_FinalProject.MainClass;
using Banico_DataStruct_FinalProject.NumericalEquations;

namespace Banico_DataStruct_FinalProject.Tests
{
    public class TeXTest
    {
        /*[TestCase ("(2x+3)/(3)")]
        [TestCase("2x+3/3")]
        [TestCase("(2x+3)/3")]
        [TestCase("2x+3/(x+3)")]
        [TestCase("2x+3/x+3")]
        [TestCase("2x+3/x+3/(x+3)*(x+2)/3-(x-1)/(x+2)")]
        [TestCase("1/1/1")]*/
        [TestCase("sqrt(3*sqrt(4x-y^2/5))")]

        /*[TestCase("sqrt(2)", @"\sqrt{(2)}")]
        [TestCase("sqrt(2+sqrt(2))", @"\sqrt{(2+\sqrt{(2)})}")]
        [TestCase("sqrt(2)sqrt(2)", @"\sqrt{(2)}\sqrt{(2)}")]
        [TestCase("sqrt(2sqrt(2))", @"\sqrt{(2\sqrt{(2)})}")]*/

        [TestCase ("(100/5)")]
        public void LaTeXTestConverter_simpleDiv_Division(string x/*, string ans*/)
        {
            var classes = new MainClassMethods();
            string y = classes.StringToTeX(x);

            Console.Write(y);
        }

        [TestCase("1,000")]
        public void convertNumberToDouble(string num)
        {
            var ans = double.Parse(num);
            Console.Write(ans);
        }


    }
}
