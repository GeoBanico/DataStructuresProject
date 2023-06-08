using System;
using System.Collections.Generic;
using System.Text;
using Banico_DataStruct_FinalProject.PolynomialEquations;
using NUnit.Framework.Constraints;
using Banico_DataStruct_FinalProject.NumericalEquations;

namespace Banico_DataStruct_FinalProject.XSimplePolynomialSolver
{
    class SPSMethodClass
    {
        private PolynomialCompiler polyCom = new PolynomialCompiler();

        public List<Term> PutToList(string expression)
        {
            var result = new List<Term>();
            var TrigoSB = new StringBuilder();
            var parStack = new Stack<char>();

            var numSb = new StringBuilder();
            var varStore = '\0';
            var powerSb = new StringBuilder();
            var signStore = '\0';

            var powerDetect = false;
            var prev = '\0';
            //x^3+2x-1
            foreach (var data in expression)
            {
                if (IsDigit(data) && !powerDetect) numSb.Append(data); //coefficient
                else if (data == '-' && prev == '\0') signStore = data; //change sign
                else if (data == '-' && prev == '^')
                {
                    if(powerSb.Length == 0) powerSb.Append(data); //change sign
                }
                else if (IsDigit(data) && powerDetect) powerSb.Append(data); //power
                else if (IsVariable(data))
                {
                    if (parStack.Count > 0)
                    {
                        numSb.Remove(0, 1);
                        parStack.Pop();
                    }

                    if (prev == '*') numSb.Remove(numSb.Length - 1, 1);
                    else if (prev == '/')
                    {
                        numSb.Remove(numSb.Length - 1, 1);
                        powerSb.Append("-");
                    }
                    varStore = data; //variable
                    powerDetect = true;
                }
                else if ((data=='+' || data == '-') && parStack.Count == 0)
                {
                    result.Add(TermDeff(signStore, numSb, varStore, powerSb));

                    //Clear Everything
                    numSb = new StringBuilder(); //Coefficient
                    varStore = '\0'; //Variable
                    powerSb = new StringBuilder(); //Power
                    signStore = data;
                    powerDetect = false;
                }
                else if (IsOperator(data) && powerDetect)
                {
                    powerSb.Append(data);
                }
                else if (IsOperator(data) && !powerDetect)
                {
                    numSb.Append(data);
                }
                else if (!IsVariable(data) && char.IsLetter(data))
                {
                    TrigoSB.Append(char.ToLower(data));
                    if (IsTrigo(TrigoSB.ToString()) && powerDetect)
                    {
                        powerSb.Append(TrigoSB.ToString());
                        TrigoSB.Clear();
                    }
                    else if (IsTrigo(TrigoSB.ToString()) && !powerDetect)
                    {
                        numSb.Append(TrigoSB.ToString());
                        TrigoSB.Clear();
                    }
                }
                else if (data == '(' && powerDetect)
                {
                    powerSb.Append(data);
                    parStack.Push('(');
                }
                else if (data == '(' && !powerDetect)
                {
                    numSb.Append(data);
                    parStack.Push('(');
                }
                else if (data == ')' && powerDetect)
                {
                    if (parStack.Count > 0)
                    {
                        powerSb.Append(data);
                        parStack.Pop();
                    }
                }
                else if (data == ')' && !powerDetect)
                {
                    if (parStack.Count > 0)
                    {
                        numSb.Append(data);
                        parStack.Pop();
                    }
                }

                prev = data;
            }

            if (numSb.ToString() != "" || varStore != '\0')
            {
                result.Add(TermDeff(signStore, numSb, varStore, powerSb));
            }
            return result;
        }

        public List<Term> SortPolynomial(List<Term> expression)
        {
            for (int j = 0; j <= expression.Count - 2; j++)
            {
                for (int i = 0; i <= expression.Count - 2; i++)
                {
                    if (expression[i].Variable == expression[i+1].Variable)
                    {
                        if (ToTryParse(expression[i].Exponent) && int.Parse(expression[i].Exponent) < int.Parse(expression[i + 1].Exponent))
                        {
                            var temp = expression[i + 1];
                            expression[i + 1] = expression[i];
                            expression[i] = temp;
                        }
                    }
                    else if (expression[i].Variable > expression[i + 1].Variable)
                    {
                        var temp = expression[i + 1];
                        expression[i + 1] = expression[i];
                        expression[i] = temp;
                    }
                }
            }
            return expression;
        }

        // ------ Operation -----
        public List<Term> PolynomialAddition(List<Term> eq1)
        {
            var haveSameTerms = true;

            while (haveSameTerms)
            {
                if (eq1.Count > 1)
                {
                    var eqCount = 0;
                    for (int i = 0; i <= eq1.Count - 2; i++)
                    {
                        if (eq1[i].Variable == eq1[i + 1].Variable)
                        {
                            if (!(ToTryParse(eq1[i].Exponent) && ToTryParse(eq1[i+1].Exponent)) || int.Parse(eq1[i].Exponent) == int.Parse(eq1[i + 1].Exponent))
                            {
                                var total = int.Parse(eq1[i].Sign + eq1[i].Coefficient) +
                                            int.Parse(eq1[i + 1].Sign + eq1[i + 1].Coefficient);
                                var sign = '+';
                                if (total != 0)
                                {
                                    if (total < 0)
                                    {
                                        sign = '-';
                                        total = total * -1;
                                    }
                                    eq1[i] = new Term(sign, total.ToString(), eq1[i].Variable, eq1[i].Exponent);
                                    eq1.RemoveAt(i + 1);
                                }
                                else
                                {
                                    eq1.RemoveRange(i, 2);
                                }
                                break;
                            }
                            else eqCount++;
                        }
                        else eqCount++;
                    }
                    if (eqCount == eq1.Count-1) haveSameTerms = false;
                    eqCount = 0;
                }
                else haveSameTerms = false;
            }

            return eq1;
        }

        public List<Term> PolynomialSubtraction(List<Term> eq1, List<Term> eq2)
        {
            for (int i = 0; i < eq2.Count; i++)
            {
                if (eq2[i].Sign == '-') eq2[i].Sign = '+';
                else if (eq2[i].Sign == '+') eq2[i].Sign = '-';
            }

            eq1.AddRange(eq2);
            var result = PolynomialAddition(eq1);
            return result;
        }

        public string PolynomialMultiplication(List<Term> eq1, List<Term> eq2)
        {
            var result = "";
            var notSameVar = "";
            var isDigit = "";

            for (int j = 0; j < eq1.Count; j++)
            {
                for (int i = 0; i < eq2.Count; i++)
                {
                    var totalCoeff = double.Parse(eq1[j].Sign + eq1[j].Coefficient) *
                                     double.Parse(eq2[i].Sign + eq2[i].Coefficient);
                    if (totalCoeff != 0)
                    {
                        var sign = "+";
                        var num = "";

                        if (totalCoeff < 0)
                        {
                            sign = "-";
                            totalCoeff = totalCoeff * -1;
                        }
                        if (totalCoeff != 1) num = totalCoeff.ToString();

                        if ((eq1[j].Exponent == "i" && eq2[i].Exponent == "i") || eq1[j].Variable == eq2[i].Variable) //same variable
                        {
                            var totalExp = 0d;

                            //if both exp are real nums
                            if (ToTryParse(eq1[j].Exponent) && ToTryParse(eq2[i].Exponent))
                            {
                                totalExp = double.Parse(eq1[j].Exponent) +
                                           double.Parse(eq2[i].Exponent);
                            }
                            //if eq1 exp is i and eq2 exp is real
                            else if (!ToTryParse(eq1[j].Exponent) && ToTryParse(eq2[i].Exponent))
                            {
                                totalExp = double.Parse(eq2[i].Exponent);
                            }
                            //if eq1 exp is real and eq2 exp is i
                            else if (ToTryParse(eq1[j].Exponent) && !ToTryParse(eq2[i].Exponent))
                            {
                                totalExp = double.Parse(eq1[j].Exponent);
                            }

                            //if exp = i
                            if (totalExp == 0)
                            {
                                if (num == "") num = "1";
                                if (sign == "+" && isDigit == "") sign = "";
                                isDigit = isDigit + $"{sign}{num}";
                            }
                            // if exp is real num
                            else
                            {
                                //if first and +x
                                if (result == "" && sign == "+")
                                {
                                    sign = "";
                                }
                                // if x
                                if (totalExp == 1) 
                                {
                                    result = result + $"{sign}{num}{eq1[j].Variable}";
                                }
                                // if x^2
                                else if (totalExp != 1) 
                                {
                                    result = result + $"{sign}{num}{eq1[j].Variable}^{totalExp}";
                                }
                            }
                        }
                        else //not same variable
                        {
                            //if both are variables
                            if (ToTryParse(eq1[j].Exponent) && ToTryParse(eq2[i].Exponent))
                            {
                                var eq1Exp = double.Parse(eq1[j].Exponent);
                                var eq2Exp = double.Parse(eq2[i].Exponent);

                                if (eq1Exp == 1 && eq2Exp == 1)
                                {
                                    notSameVar = notSameVar + $"{sign}{num}{eq1[j].Variable}{eq2[i].Variable}";
                                }
                                else if (eq1Exp == 1 && eq2Exp != 1)
                                {
                                    notSameVar = notSameVar + $"{sign}{num}{eq1[j].Variable}{eq2[i].Variable}^{eq2Exp}";
                                }
                                else if (eq1Exp != 1 && eq2Exp == 1)
                                {
                                    notSameVar = notSameVar + $"{sign}{num}{eq1[j].Variable}^{eq1Exp}{eq2[i].Variable}";
                                }
                                else if (eq1Exp != 1 && eq2Exp != 1)
                                {
                                    notSameVar = notSameVar + $"{sign}{num}{eq1[j].Variable}^{eq1Exp}{eq2[i].Variable}^{eq2Exp}";
                                }
                            }
                            
                            //if eq1 exp is i and eq2 exp is real
                            else if (!ToTryParse(eq1[j].Exponent) && ToTryParse(eq2[i].Exponent))
                            {
                                if (result == "" && sign == "+") sign = "";
                                var eq2Exp = double.Parse(eq2[i].Exponent);
                                if (eq2Exp == 1)
                                {
                                    result = result + $"{sign}{num}{eq2[i].Variable}";
                                }
                                else if (eq2Exp != 1)
                                {
                                    result = result + $"{sign}{num}{eq2[i].Variable}^{eq2Exp}";
                                }
                            }
                            //if eq1 exp is real and eq2 exp is i
                            else if (ToTryParse(eq1[j].Exponent) && !ToTryParse(eq2[i].Exponent))
                            {
                                if (result == "" && sign == "+") sign = "";
                                var eq1Exp = double.Parse(eq1[j].Exponent);
                                if (eq1Exp == 1)
                                {
                                    result = result + $"{sign}{num}{eq1[j].Variable}";
                                }
                                else if (eq1Exp != 1)
                                {
                                    result = result + $"{sign}{num}{eq1[j].Variable}^{eq1Exp}";
                                }
                            }
                            
                        }
                    }
                }
            }

            if (result == "" && notSameVar != "") notSameVar = notSameVar.Remove(0,1);

            var resultPoly = "";
            var resultDigit = "";
            //adding same polynomials

            //adding same digits
            if (result != "")
            {
                var newList = PutToList(result);
                var newResultList = PolynomialAddition(SortPolynomial(newList));
                resultPoly = polyCom.TermListToString(newResultList);
            }
            if (isDigit != "")
            {
                var newDigit = PutToList(isDigit);
                var newResultDigit = PolynomialAddition(newDigit);
                resultDigit = polyCom.TermListToString(newResultDigit);
            }

            if (isDigit != "" && (double.Parse(resultDigit) > 0))
            {
                result = $"{resultPoly}{notSameVar}+{resultDigit}";
            }
            else return result = $"{resultPoly}{notSameVar}{resultDigit}";

            return result;
        }

        public string PolynomialDivision(List<Term> eq1, List<Term> eq2)
        {
            var result = "(";
            foreach (var data in eq1)
            {
                var sign = data.Sign.ToString();
                var coeff = data.Coefficient;
                var varr = data.Variable;
                var exp = data.Exponent;

                if (sign == "+" && result == "(") sign = "";
                if (double.Parse(coeff) == 1) coeff = "";
                if (exp == "i")
                {
                    if (coeff == "") coeff = "1";
                    result = result + $"{sign}{coeff}";
                }
                else if (exp == "1") result = result + $"{sign}{coeff}{varr}";
                else result = result + $"{sign}{coeff}{varr}^{exp}";
            }

            result = result + ")/(";
            foreach (var data in eq2)
            {
                var sign = data.Sign.ToString();
                var coeff = data.Coefficient;
                var varr = data.Variable;
                var exp = data.Exponent;

                if (sign == "+" && result[result.Length-1] == '(') sign = "";
                if (double.Parse(coeff) == 1) coeff = "";
                if (exp == "i")
                {
                    if (coeff == "") coeff = "1";
                    result = result + $"{sign}{coeff}";
                }
                else if (exp == "1") result = result + $"{sign}{coeff}{varr}";
                else result = result + $"{sign}{coeff}{varr}^{exp}";
            }

            return result+")";
        }

        // ------ Helping Methods
        public List<Term> SolveCoefficientInList(List<Term> term)
        {
            var numArith = new ArithmeticCompiler();
            for (int i = 0; i < term.Count; i++)
            {
                term[i].Coefficient = numArith.CompileArthmetic(term[i].Coefficient).ToString();
                if (term[i].Exponent != "i") term[i].Exponent = numArith.CompileArthmetic(term[i].Exponent).ToString();
            }
            return term;
        }

        private Term TermDeff(char signStore, StringBuilder numSb, char varStore, StringBuilder powerSb)
        {
            if (numSb.Length == 0) numSb.Append("1");
            if (varStore == '\0' && powerSb.Length == 0)
            {
                varStore = '~';
                powerSb.Append("i");
            }
            if (powerSb.Length == 0 && varStore != '\0') powerSb.Append("1");
            if (signStore == '\0') signStore = '+';

            var x = new Term(signStore, numSb.ToString(), varStore, powerSb.ToString());

            return x;
        }

        public bool ToTryParse(string data)
        {
            return double.TryParse(data, out double result);
        }

        private bool IsTrigo(string trigs)
        {
            switch (trigs)
            {
                case "sin":
                case "cos":
                case "tan":
                case "csc":
                case "cot":
                case "sec":
                case "sqrt":
                    return true;
                default:
                    return false;
            }
        }

        private bool IsOperator(char data)
        {
            switch (data)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                    return true;
                default:
                    return false;
            }
        }

        private bool IsVariable(char varr)
        {
            if (varr == 'x' || varr == 'y' || varr == 'z') return true;
            return false;
        }

        private bool IsDigit(char digits)
        {
            if (char.IsDigit(digits)) return true;
            if (digits == ',') return true;
            if (digits == '.') return true;
            return false;
        }
    }
}
