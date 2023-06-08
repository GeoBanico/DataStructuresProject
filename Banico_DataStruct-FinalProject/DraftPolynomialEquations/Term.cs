using System;
using System.Collections.Generic;
using System.Text;

namespace Banico_DataStruct_FinalProject.PolynomialEquations
{
    public class Term
    {
        public char Sign;
        public string Coefficient;
        public char Variable;
        public string Exponent;

        public Term()
        {
        }

        public Term(char sign, string coeff, char varr, string exp)
        {
            Sign = sign;
            Coefficient = coeff;
            Variable = varr;
            Exponent = exp;
        }

        public override string ToString()
        {
            // digit
            if (Exponent == "i") return $"{Sign}{Coefficient}";

            // x
            if (Coefficient == "1" && Exponent == "1") return $"{Sign}{Variable}";

            // 2x
            if (Coefficient != "1" && Exponent == "1") return $"{Sign}{Coefficient}{Variable}";

            // x^2
            if (Coefficient == "1" && Exponent != "1") return $"{Sign}{Variable}^{Exponent}";

            // 2x^2
            return $"{Sign}{Coefficient}{Variable}^{Exponent}";
        }
    }
}
