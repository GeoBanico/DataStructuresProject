using System;
using System.Collections.Generic;
using System.Text;
using Banico_DataStruct_FinalProject.MainClass;

namespace Banico_DataStruct_FinalProject.NumericalEquations
{
    class NumericalDigitParse
    {
        public Queue<string> DigitParser(string toEval)
        {
            var mainClasses = new MainClassMethods();

            var result = new Queue<string>();
            var sb = new StringBuilder();
            var TrigoSB = new StringBuilder();
            
            char prev = '\0';
            var parStore = '\0';

            foreach (char ch in toEval)
            {
                if (mainClasses.IsDigit(ch))
                {
                    if (mainClasses.IsClosing(prev.ToString())) result.Enqueue("*");
                    sb.Append(ch);
                }
                else if (ch == '^')
                {
                    result.Enqueue(sb.ToString());
                    sb.Clear();
                    result.Enqueue(ch.ToString());
                }
                else if (ch == '-' && prev == '\0' || ch == '-' && mainClasses.IsOpening(prev.ToString()) || ch == '-' && mainClasses.IsOperator(prev.ToString())) sb.Append(ch);
                else if (mainClasses.IsOperator(ch.ToString()))
                {
                    if (sb.ToString() != "")
                    {
                        result.Enqueue(sb.ToString());
                        sb.Clear();
                    }

                    result.Enqueue(ch.ToString());
                }
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
                    if (sb.ToString() != "")
                        result.Enqueue(sb.ToString());
                    if (parStore != '\0')
                    {
                        result.Enqueue(")");
                        parStore = '\0';
                    }
                    result.Enqueue(ch.ToString());
                    sb.Clear();
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

            if (sb.ToString() != "")
                result.Enqueue(sb.ToString());
            if (TrigoSB.ToString() != "") { throw new Exception("Error"); }
            if (parStore != '\0') { result.Enqueue(")"); }
            return result;
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
    }
}
