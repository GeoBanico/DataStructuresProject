using System;
using System.Collections.Generic;
using System.Text;

namespace Banico_DataStruct_FinalProject.NumericalEquations
{
    class NumericalPostfixEvaluator
    {
        public double Evaluate(Queue<string> expression)
        {
            var NumberStore = new Stack<double>();
            foreach (string data in expression)
            {
                if (ToTryParse(data)) //number store in stack
                {
                    NumberStore.Push(double.Parse(data));
                }
                else
                {
                    if (IsOperator(data))
                    {
                        double y = NumberStore.Pop();
                        double x = NumberStore.Pop();
                        NumberStore.Push(ToEvaluateOps(x, y, data));
                    }
                    else if (IsTrigo(data))
                    {
                        double x = NumberStore.Pop();
                        NumberStore.Push(ToEvaluateTrig(x, data));
                    }
                }
            }

            var result = NumberStore.Pop();

            if (result.ToString().Contains('.')) return Math.Round(result, 3);

            return result;
        }

        private double ToEvaluateTrig(double x, string trigs)
        {
            double ans = 0d;
            switch (trigs)
            {
                case "sin":
                    ans = Math.Sin(x);
                    break;
                case "cos":
                    ans = Math.Cos(x);
                    break;
                case "tan":
                    ans = Math.Tan(x);
                    break;
                case "cot":
                    ans = 1 / Math.Tan(x);
                    break;
                case "csc":
                    ans = 1 / Math.Sin(x);
                    break;
                case "sec":
                    ans = 1 / Math.Cos(x);
                    break;
                case "sqrt":
                    ans = Math.Sqrt(x);
                    break;
            }
            return ans;
        }

        private double ToEvaluateOps(double x, double y, string op)
        {
            double ans = 0d;
            switch (op)
            {
                case "+":
                    ans = x + y;
                    break;
                case "-":
                    ans = x - y;
                    break;
                case "/":
                    ans = x / y;
                    break;
                case "*":
                    ans = x * y;
                    break;
                case "^":
                    ans = Math.Pow(x, y);
                    break;
            }
            return ans;
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

        private bool IsTrigo(string data)
        {
            if (data == "sin") return true;
            if (data == "cos") return true;
            if (data == "tan") return true;
            if (data == "cot") return true;
            if (data == "csc") return true;
            if (data == "sec") return true;
            if (data == "sqrt") return true;
            return false;
        }
    }
}
