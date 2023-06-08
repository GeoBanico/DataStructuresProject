using System;
using System.Collections.Generic;
using System.Text;

namespace Banico_DataStruct_FinalProject.PolynomialEquations
{
    class PolynomialInfixToPostfix
    {
        public Queue<string> PolyInfixToPostFix(Queue<string> result) 
        {
            var PostFix = new Queue<string>();
            var store = new Stack<string>();

            foreach (string data in result)
            {
                if (ToTryParse(data)) PostFix.Enqueue(data); //Number
                else if (IsOperator(data))  //Operator
                {
                    if (store.Count == 0 || IsOpening(store.Peek())) store.Push(data);
                    else if (store.Count != 0 && IsOperator(store.Peek()))
                    {
                        if (IsTrigo(data)) { store.Push(data); }
                        else if (Precedence(store.Peek()) >= Precedence(data))
                        {
                            PostFix.Enqueue(store.Pop());
                            store.Push(data);
                        }
                        else if (Precedence(store.Peek()) < Precedence(data))
                        {
                            store.Push(data);
                        }
                    }
                }
                else if (IsOpening(data.ToString())) store.Push(data); //Opening
                else if (IsClosing(data.ToString())) //Closing
                {
                    while (!IsOpening(store.Peek()))
                    {
                        PostFix.Enqueue(store.Pop());
                    }
                    store.Pop();
                }
            }

            while (store.Count != 0)
            {
                PostFix.Enqueue(store.Pop());
            }
            return PostFix;
        }

        private bool IsOpening(string ch)
        {
            var chs = new char[] { '(', '{', '[' };
            return ch.IndexOfAny(chs) >= 0;
        }

        private bool IsClosing(string ch)
        {
            var chs = new char[] { '}', ')', ']' };
            return ch.IndexOfAny(chs) >= 0;
        }

        private bool ToTryParse(string data)
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

        private int Precedence(string data)
        {
            if (data == "^") return 6;
            if (data == "sqrt") return 6;
            if (data == "sin") return 5;
            if (data == "cos") return 5;
            if (data == "tan") return 5;
            if (data == "cot") return 5;
            if (data == "csc") return 5;
            if (data == "sec") return 5;
            if (data == "*") return 2;
            if (data == "/") return 2;
            if (data == "+") return 1;
            if (data == "-") return 1;
            return 0;
        }

        private bool IsOperator(string data)
        {
            if (data == "^") return true;
            if (data == "sqrt") return true;
            if (data == "*") return true;
            if (data == "/") return true;
            if (data == "sin") return true;
            if (data == "cos") return true;
            if (data == "tan") return true;
            if (data == "cot") return true;
            if (data == "csc") return true;
            if (data == "sec") return true;
            if (data == "+") return true;
            if (data == "-") return true;
            return false;
        }
    }
}
