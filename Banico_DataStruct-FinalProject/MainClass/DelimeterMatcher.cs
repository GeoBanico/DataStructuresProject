using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic;

namespace Banico_DataStruct_FinalProject.MainClass
{
    public class DelimeterMatcher
    {
        private MainClassMethods MainMethods = new MainClassMethods();

        public bool DelimiterMatcher(string toEvaluate)
        {
            var storeOP = new Stack<char>();
            var prev = '\0';
            var trigoSB = new StringBuilder();
            var trigoStore = "";

            if (restrictedInputs(toEvaluate)) return false;
            if (MainMethods.IsOperator(toEvaluate[^1].ToString())) return false;
            if (toEvaluate[^1].ToString() == "." || toEvaluate[^1].ToString() == "," || toEvaluate[^1].ToString() == "^") return false;

            foreach (char data in toEvaluate)
            {
                if (trigoStore != "" && !MainMethods.IsOpening(data.ToString())) return false;
                if (MainMethods.IsOpening(data.ToString()))
                {
                    trigoStore = "";
                    storeOP.Push(data);
                }
                else if (MainMethods.IsOperator(data.ToString()) && MainMethods.IsOpening(prev.ToString())) return false;
                else if (MainMethods.IsClosing(prev.ToString()) && MainMethods.IsDigit(data)) return false;
                else if (MainMethods.IsClosing(data.ToString()))
                {
                    if (MainMethods.IsOperator(prev.ToString())) return false;
                    if (storeOP.Count == 0) return false;
                    else
                    {
                        var OP = storeOP.Pop();
                        if (!(OP == '(' && data == ')' || OP == '{' && data == '}' || OP == '[' && data == ']'))
                        {
                            return false;
                        }
                    }
                }
                else if ((prev == '.' && !char.IsDigit(data)) || (prev == ',' && !char.IsDigit(data))) return false;
                else if ((!char.IsDigit(prev) && data == ',') || (!char.IsDigit(prev) && data == '.')) return false;
                else if (char.IsLetter(data) && !MainMethods.IsPolynomial(data.ToString()))
                {
                    trigoSB.Append(data);
                    if (IsTrigo(trigoSB.ToString()))
                    {
                        trigoStore = trigoSB.ToString();
                        trigoSB.Clear();
                    }
                }
                prev = data;
            }

            if (storeOP.Count != 0) return false;
            if (MainMethods.IsOperator(prev.ToString())) return false;
            if (trigoSB.Length > 0 || trigoStore != "") return false;
            return true;
        }

        private bool restrictedInputs(string toEvaluate)
        {
            if (toEvaluate == "") return true;
            else if (toEvaluate.Contains("()")) return true;
            else if (toEvaluate.Contains("**")) return true;
            else if (toEvaluate.Contains("//")) return true;
            else if (toEvaluate.Contains("^^")) return true;
            else if (toEvaluate.Contains("*/")) return true;
            else if (toEvaluate.Contains("/*")) return true;
            else if (toEvaluate.Contains("^*")) return true;
            else if (toEvaluate.Contains("^*")) return true;
            else if (toEvaluate.Contains("^/")) return true;
            else if (toEvaluate.Contains("/^")) return true;

            else if (toEvaluate.Contains("!")) return true;
            else if (toEvaluate.Contains("@")) return true;
            else if (toEvaluate.Contains("#")) return true;
            else if (toEvaluate.Contains("%")) return true;
            else if (toEvaluate.Contains("$")) return true;
            else if (toEvaluate.Contains("&")) return true;

            else if (toEvaluate.Contains(">")) return true;
            else if (toEvaluate.Contains("<")) return true;
            else if (toEvaluate.Contains("=")) return true;

            else if (toEvaluate.Contains("|")) return true;
            else if (toEvaluate.Contains(@"\")) return true;
            else if (toEvaluate.Contains("|")) return true;
            else if (toEvaluate.Contains("\"")) return true;
            else if (toEvaluate.Contains("\'")) return true;
            else if (toEvaluate.Contains(";")) return true;
            else if (toEvaluate.Contains(":")) return true;
            return false;
        }

        private bool HasTrigo(string toEvaluate)
        {
            if (toEvaluate.Contains("sin") && !toEvaluate.Contains("sin(")) return false;
            else if (toEvaluate.Contains("cos") && !toEvaluate.Contains("cos(")) return false;
            else if (toEvaluate.Contains("sec") && !toEvaluate.Contains("sec(")) return false;
            else if (toEvaluate.Contains("csc") && !toEvaluate.Contains("csc(")) return false;
            else if (toEvaluate.Contains("tan") && !toEvaluate.Contains("tan(")) return false;
            else if (toEvaluate.Contains("cot") && !toEvaluate.Contains("cot(")) return false;
            else if (toEvaluate.Contains("sqrt") && !toEvaluate.Contains("sqrt(")) return false;
            return true;
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

        public string ToSimplify(string expression)
        {
            var newSb = new StringBuilder(expression.ToLower());

            newSb.Replace(" ", string.Empty);
            newSb.Replace("--", "+");
            newSb.Replace("{", "(");
            newSb.Replace("[", "(");
            newSb.Replace("}", ")");
            newSb.Replace("[", ")");

            if (newSb.ToString().Contains("-(")) newSb.Replace("-(", "-1*(");
            // change 8sin to 8*sin
            var prev = '\0';
            for (int i = expression.Length-1; i >= 0; i--)
            {
                if (prev == 'c' && char.IsDigit(expression[i])) //cos, csc, cot
                {
                    newSb.Replace(expression[i].ToString(), expression[i] + "*", i, 1);
                }
                else if (prev == 's' && char.IsDigit(expression[i])) //srqt, sec, sin
                {
                    newSb.Replace(expression[i].ToString(), expression[i] + "*", i, 1);
                }
                else if (prev == 't' && char.IsDigit(expression[i])) //tan
                {
                    newSb.Replace(expression[i].ToString(), expression[i] + "*", i, 1);
                }

                prev = expression[i];
            }

            //change ()() to (()*())
            int keep = 1;
            while (newSb.ToString().Contains(")("))
            {
                int fclosing = newSb.ToString().IndexOf(")(", StringComparison.Ordinal);

                for (int iCl = fclosing - 1; iCl >= 0; iCl--) //search for first opening
                {
                    if (newSb[iCl].ToString().Contains(")"))
                    {
                        keep++;
                    }
                    else if (keep != 1 && newSb[iCl].ToString().Contains("("))
                    {
                        keep--;
                    }
                    else if (keep == 1 && newSb[iCl].ToString().Contains("("))
                    {
                        newSb.Replace("(", "((", iCl, 1);
                        break;
                    }
                }

                keep = 1;

                int fopening = newSb.ToString().IndexOf(")(") + 1;

                for (int iOp = fopening + 1; iOp <= newSb.Length - 1; iOp++) //search for last closing
                {
                    if (newSb[iOp].ToString().Contains("("))
                    {
                        keep++;
                    }
                    else if (keep != 1 && newSb[iOp].ToString().Contains(")"))
                    {
                        keep--;
                    }
                    else if (keep == 1 && newSb[iOp].ToString().Contains(")"))
                    {
                        newSb.Replace(")", "))", iOp, 1);
                        break;
                    }
                }

                newSb.Replace(")(", ")*(", fclosing, 3);
                keep = 1;
            }

            return newSb.ToString();
        }

    }
}
