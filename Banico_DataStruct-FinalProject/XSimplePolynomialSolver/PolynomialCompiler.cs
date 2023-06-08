using System;
using System.Collections.Generic;
using System.Text;
using Banico_DataStruct_FinalProject.MainClass;
using System.Windows;
using Banico_DataStruct_FinalProject.PolynomialEquations;

namespace Banico_DataStruct_FinalProject.XSimplePolynomialSolver
{
    class PolynomialCompiler
    {
        public string SolvePolynomial(string eq1, string eq2, string op)
        {
            string result = "";
            try
            {
                var formulaChecker = new DelimeterMatcher();
                var polyMethods = new SPSMethodClass();

                var eq1List = polyMethods.PutToList(formulaChecker.ToSimplify((eq1)));
                var eq2List = polyMethods.PutToList(formulaChecker.ToSimplify((eq2)));

                eq1List = polyMethods.SortPolynomial(polyMethods.SolveCoefficientInList(eq1List));
                eq2List = polyMethods.SortPolynomial(polyMethods.SolveCoefficientInList(eq2List));

                if (op == "+" || op == "*") //switchable
                {
                    
                    if (polyMethods.ToTryParse(eq1List[0].Exponent) && polyMethods.ToTryParse(eq2List[0].Exponent))
                    {
                        if (double.Parse(eq1List[0].Exponent) < double.Parse(eq2List[0].Exponent))
                        {
                            var temp = eq1List;
                            eq1List = eq2List;
                            eq2List = temp;
                        }
                    }
                    else if (!polyMethods.ToTryParse(eq1List[0].Exponent) && polyMethods.ToTryParse(eq2List[0].Exponent))
                    {
                        var temp = eq1List;
                        eq1List = eq2List;
                        eq2List = temp;
                    }
                }

                var opAns = new List<Term>();
                switch (op)
                {
                    case "+":
                        eq1List.AddRange(eq2List);
                        opAns = polyMethods.PolynomialAddition(eq1List);
                        result = TermListToString(opAns);
                        break;
                    case "-":
                        opAns = polyMethods.PolynomialSubtraction(eq1List, eq2List);
                        result = TermListToString(opAns);
                        break;
                    case "*":
                        result = polyMethods.PolynomialMultiplication(eq1List, eq2List);
                        break;
                    case "/":
                        result = polyMethods.PolynomialDivision(eq1List, eq2List);
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Equation Error");
            }

            return result;
        }

        public string TermListToString(List<Term> expression)
        {
            var result = "";

            foreach (var data in expression)
            {
                if (result == "")
                {
                    //first and positive
                    if (data.Sign == '+')
                    {
                        var strData = data.ToString().Replace("+","");
                        result = result + strData;
                    }
                    else result = result + data;
                }
                else result = result + data;
            }

            return result;
        }
    }
}
