using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Banico_DataStruct_FinalProject.MainClass;

namespace Banico_DataStruct_FinalProject.NumericalEquations
{
    public class ArithmeticCompiler
    {
        public double CompileArthmetic(string expression)
        {
            var ans = 0d;
            try
            {
                var checkExpression = new DelimeterMatcher();

                if (checkExpression.DelimiterMatcher(expression))
                {
                    var deliMete = new DelimeterMatcher();
                    var stringDigit = deliMete.ToSimplify(expression);

                    var digitSetter = new NumericalDigitParse();
                    var queuedDigit = digitSetter.DigitParser(stringDigit);

                    var inFix = new NumericalInfixToPostfix();
                    var postfix = inFix.ToPostFix(queuedDigit);

                    var PostFixEval = new NumericalPostfixEvaluator();
                    ans = PostFixEval.Evaluate(postfix);
                }
                else
                {
                    Console.WriteLine("Incorrect Expression");
                }
            }
            catch
            {
                MessageBox.Show("Equation Error");
            }
            return ans;
        }
    }
}
