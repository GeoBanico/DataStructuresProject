using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Banico_DataStruct_FinalProject.MainClass;

namespace Banico_DataStruct_FinalProject.PolynomialEquations
{
    public class PolynomialDigitParse
    {
        public Queue<string> PolynomialSolver(string toEval)
        {
            var mainClasses = new MainClassMethods();

            var result = new Queue<string>();
            var termStore = new List<Term>(); //stores the separate polynomial operators

            var sb = new StringBuilder(); //Coefficient
            var varStore = '\0'; //Variable
            var powerSB = new StringBuilder(); //Power
            var signStore = '\0';

            var TrigoSB = new StringBuilder();
            var numCcIndex = 0;

            char prev = '\0';
            var parStore = '\0';
            var powerForm = false;

            foreach (char ch in toEval)
            {
                //3x^2+2x+1
                if (prev == varStore && ch == '^') powerForm = true;
                else if (powerForm && char.IsDigit(ch)) powerSB.Append(ch.ToString());
                else if (powerForm && !char.IsDigit(ch)) powerForm = false;

                if (mainClasses.IsDigit(ch) && !powerForm)
                {
                    if (mainClasses.IsClosing(prev.ToString())) result.Enqueue("*");
                    sb.Append(ch);
                }
                else if (ch == '^' && prev != varStore)
                {
                    result.Enqueue(sb.ToString());
                    sb.Clear();
                    result.Enqueue(ch.ToString());
                }
                else if (mainClasses.IsVariable(ch.ToString())) varStore = ch;
                else if (ch == '-' && prev == '\0' || ch == '-' && mainClasses.IsOpening(prev.ToString()) || ch == '-' && mainClasses.IsOperator(prev.ToString())) signStore = '-';
                else if (mainClasses.IsOperator(ch.ToString()) && !mainClasses.IsClosing(prev.ToString()))
                {
                    termStore.Add(TermDeff(signStore, sb.ToString(), varStore, powerSB.ToString(), ch));
                    result.Enqueue(numCcIndex.ToString());
                    numCcIndex++;
                    sb = new StringBuilder(); //Coefficient
                    varStore = '\0'; //Variable
                    powerSB = new StringBuilder(); //Power
                    signStore = '\0';

                    if (ch == '-')
                    {
                        result.Enqueue("+");
                        signStore = '-';
                    }
                    else result.Enqueue(ch.ToString());
                }
                else if (mainClasses.IsOperator(ch.ToString()) && mainClasses.IsClosing(prev.ToString())) result.Enqueue(ch.ToString());
                else if (mainClasses.IsOpening(ch.ToString()))
                {
                    if (mainClasses.IsDigit(prev))
                    {
                        if (sb.ToString() != "")
                        {
                            result.Enqueue(ch.ToString());
                            parStore = '(';

                            result.Enqueue(sb.ToString());
                            sb.Clear();
                        }
                        else if (sb.ToString() == "" && result.Count > 0)
                        {
                            var keep = result.Dequeue();
                            result.Enqueue(ch.ToString());
                            parStore = '(';
                            result.Enqueue(keep);
                        }
                        result.Enqueue("*");
                    }
                    result.Enqueue(ch.ToString());
                }
                else if (mainClasses.IsClosing(ch.ToString()))
                {
                    termStore.Add(TermDeff(signStore, sb.ToString(), varStore, powerSB.ToString(), ch));
                    result.Enqueue(numCcIndex.ToString());
                    numCcIndex++;
                    sb = new StringBuilder(); //Coefficient
                    varStore = '\0'; //Variable
                    powerSB = new StringBuilder(); //Power
                    signStore = '\0';

                    if (ch == '-')
                    {
                        result.Enqueue("+");
                        signStore = '-';
                    }
                    else result.Enqueue(ch.ToString());

                    if (parStore != '\0')
                    {
                        result.Enqueue(")");
                        parStore = '\0';
                    }
                }

                else if (IsChar(ch))
                {
                    if (mainClasses.IsDigit(prev))
                    {
                        result.Enqueue(sb.ToString());
                        sb.Clear();
                        result.Enqueue("*");
                    }

                    TrigoSB.Append(char.ToLower(ch));
                    if (IsTrigo(TrigoSB.ToString()))
                    {
                        result.Enqueue(TrigoSB.ToString());
                        TrigoSB.Clear();
                    }
                }

                prev = ch;
            }

            if (sb.ToString() != "" || varStore != '\0')
            {
                termStore.Add(TermDeff(signStore, sb.ToString(), varStore, powerSB.ToString(), signStore));
                result.Enqueue(numCcIndex.ToString());
                sb.Clear();
            }

            result.Enqueue(sb.ToString());
            if (TrigoSB.ToString() != "") { throw new Exception("Error"); }
            if (parStore != '\0') { result.Enqueue(")"); }

            //Infix To PostInfix ----------------------------
            var infixToPostfix = new PolynomialInfixToPostfix();
            var infixedExpression = infixToPostfix.PolyInfixToPostFix(result);

            //PostFix Evaluator------------------------------
            //01+
            var equationStore = new Stack<string>();
            var CcIndex = 0;
            foreach (var dataCh in infixedExpression)
            {
                if (ToTryParse(dataCh)) //number store in stack
                {
                    equationStore.Push(dataCh);
                }
                else
                {
                    if (IsOperator(dataCh))
                    {
                        string y = equationStore.Pop();
                        string x = equationStore.Pop();

                        var ans = ToEvaluateOps1(x, y, dataCh, termStore);
                        equationStore.Push($"{CcIndex.ToString()}!");
                        CcIndex++;
                    }
                }
            }

            return result;
        }

        private string ToEvaluateOps1(string x, string y, string op, List<Term> termStore)
        {
            var ans = "";
            /*
            switch (op)
            {
                case "+":
                    ans = PolynomialAddition1(string x, string y, List<Term> termStore);
                    break;
                case "/":
                    ans = x / y;
                    break;
                case "*":
                    ans = x * y;
                    break;
            }
            */
            return ans;
        }

        private string PolynomialAddition1(string x, string y, List<Term> termStore)
        {
            var num1 = termStore[int.Parse(x)];
            var num2 = termStore[int.Parse(y)];

            if (num1.Variable == num2.Variable && int.Parse(num1.Exponent) > int.Parse(num2.Exponent))
            {
                return ($"{x}+{y}");
            }
            else if(num1.Variable == num2.Variable && int.Parse(num1.Exponent) > int.Parse(num2.Exponent))
            {
                return ($"{y}+{x}");
            }
            else if (num1.Variable == num2.Variable && int.Parse(num1.Exponent) == int.Parse(num2.Exponent))
            {
                var coeffCom = int.Parse(num1.Sign + num1.Coefficient) + int.Parse(num2.Sign + num2.Coefficient);
                var sign = '\0';

                if (coeffCom < 0)
                {
                    sign = '-';
                    coeffCom = coeffCom * -1;
                }
                else sign = '+';
                termStore[int.Parse(x)] = new Term(sign, coeffCom.ToString(),num1.Variable, num1.Exponent);
                termStore.RemoveAt(int.Parse(y));

                return x;
            }
            else if (num1.Variable != num2.Variable)
            {
                if(num1.Variable < num2.Variable) return ($"{x}+{y}");
                else return ($"{y}+{x}");
            }
            return "";
        }

        private string PolynomialMultiplication1(string x, string y, List<Term> termStore)
        {
            var num1 = termStore[int.Parse(x)];
            var num2 = termStore[int.Parse(y)];

            if (num1.Variable == num2.Variable)
            {
                var coeffCom = double.Parse(num1.Sign + num1.Coefficient) * double.Parse(num2.Sign + num2.Coefficient);
                var powerCom = double.Parse(num1.Exponent) * double.Parse(num2.Exponent);
                var sign = '\0';
                if (coeffCom < 0)
                {
                    sign = '-';
                    coeffCom = coeffCom * -1;
                }
                else sign = '+';
                termStore[int.Parse(x)] = new Term(sign, coeffCom.ToString(), num1.Variable, powerCom.ToString());
                termStore.RemoveAt(int.Parse(y));
                return (x);
            }
            else 
            {
                if (num1.Variable < num2.Variable) return ($"{x}*{y}");
                else return ($"{y}*{x}");
            }
        }

        private string PolynomialDivision1(string x, string y, List<Term> termStore)
        {
            var num1 = termStore[int.Parse(x)];
            var num2 = termStore[int.Parse(y)];

            if (num1.Variable == num2.Variable && double.Parse(num1.Exponent) > double.Parse(num2.Exponent))
            {
                var coeffCom = double.Parse(num1.Sign + num1.Coefficient) / double.Parse(num2.Sign + num2.Coefficient);
                var powerCom = double.Parse(num1.Exponent) - double.Parse(num2.Exponent);
                var sign = '\0';
                if (coeffCom < 0)
                {
                    sign = '-';
                    coeffCom = coeffCom * -1;
                }
                else sign = '+';
                termStore[int.Parse(x)] = new Term(sign, coeffCom.ToString(), num1.Variable, powerCom.ToString());
                termStore.RemoveAt(int.Parse(y));
                return (x);
            }
            else if (num1.Variable == num2.Variable && double.Parse(num1.Exponent) < double.Parse(num2.Exponent))
            {
                
            }
            else
            {
                if (num1.Variable < num2.Variable) return ($"{x}*{y}");
                else return ($"{y}*{x}");
            }
            return "";
        }



        private bool ToTryParse(string data)
        {
            return double.TryParse(data, out double result);
        }

        private bool IsOperator(string data)
        {
            if (data == "^") return true;
            if (data == "*") return true;
            if (data == "/") return true;
            if (data == "+") return true;
            if (data == "-") return true;
            return false;
        }

        private bool IsChar(char letts)
        {
            if (char.IsLetter(letts)) { return true; }
            return false;
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

        private Term TermDeff(char signStore, string digitSB, char varStore, string powerSB, char ch)
        {
            if (digitSB == "") digitSB = "1";
            if (varStore == '\0')
            {
                varStore = 'x';
                powerSB = "0";
            }
            if (powerSB == "" && varStore != '\0') powerSB = "1";
            if (ch == '-' && signStore == '\0') signStore = '-';
            if (ch == '+' && signStore == '\0') signStore = '+';

            var x = new Term(signStore, digitSB, varStore, powerSB);

            return x;
        }


    }
}
