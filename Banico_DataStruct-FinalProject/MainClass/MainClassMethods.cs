using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfMath;

namespace Banico_DataStruct_FinalProject.MainClass
{
    public class MainClassMethods
    {
        public string GetPngEquation(string equation, string fileName)
        {
            var parser = new TexFormulaParser();

            var formula = parser.Parse(equation);
            var pngBytes = formula.RenderToPng(40.0, 0.0, 0.0, "Arial");
            File.WriteAllBytes(fileName, pngBytes);
            return fileName;
        }

        public ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.EndInit();
            return bitmap;
        }

        public string StringToTeX(string expression)
        {
            string convertedValue = expression;
            if (expression.Contains("sqrt"))
            {
                convertedValue = SqrtLateX(convertedValue);
            }
            if (expression.Contains("/"))
            {
                convertedValue = DivLateX(convertedValue);
            }
            return convertedValue;
        }

        private string SqrtLateX(string expression)
        {
            var copyExpression = new StringBuilder(expression);
            var parHolder = new Stack<string>();
            copyExpression.Replace("sqrt", @"\sqrt#");
            while (copyExpression.ToString().Contains('#'))
            {
                var symIndex = copyExpression.ToString().LastIndexOf('#');
                for (int i = symIndex; i < copyExpression.Length; i++)
                {
                    if (copyExpression[i] == '#') copyExpression[i] = '{';
                    else if (copyExpression[i] == '(') parHolder.Push("(");
                    else if (copyExpression[i] == ')')
                    {
                        parHolder.Pop();
                        if (parHolder.Count == 0)
                        {
                            copyExpression.Replace(copyExpression[i].ToString(), copyExpression[i] + "}", i, 1);
                            break;
                        }
                    }
                }
            }

            return copyExpression.ToString();
        }

        private string DivLateX(string expression)
        {
            var copyExpression = new StringBuilder(expression);
            var opened = false;
            var parHolder = new Stack<string>();

            var parOpen = 0;
            var parClose = 0;
            copyExpression.Replace("/", "#/#");
            while (copyExpression.ToString().Contains("#"))
            {
                var divLocation = copyExpression.ToString().LastIndexOf("#/#", StringComparison.Ordinal);
                //rightside (Location is +2)
                for (int i = divLocation + 2; i < copyExpression.Length; i++)
                {
                    if (copyExpression[i] == '#' && opened == false)
                    {
                        copyExpression[i] = '{';
                        opened = true;
                    }
                    else if (parHolder.Count == 0 && IsOperator(copyExpression[i].ToString()) && copyExpression[i] != '/')
                    {
                        copyExpression.Replace(copyExpression[i].ToString(), copyExpression[i].ToString() + "}", i, 1);
                        opened = false;
                        break;
                    }
                    else if (IsOpening(copyExpression[i].ToString()))
                    {
                        parHolder.Push("(");
                        parOpen++;
                    }
                    else if (IsClosing(copyExpression[i].ToString()) && i != copyExpression.Length - 1 && copyExpression[i + 1].ToString() == "/")
                    {
                        parHolder.Pop();
                        parClose++;
                    }
                    else if (IsClosing(copyExpression[i].ToString()) && i != copyExpression.Length - 1 && copyExpression[i + 1].ToString() != "/")
                    {
                        if (parHolder.Count != 0)
                        {
                            parHolder.Pop();
                            if (parHolder.Count == 0)
                            {
                                copyExpression.Replace(copyExpression[i].ToString(), copyExpression[i] + "}", i, 1);
                                opened = false;
                                break;
                            }
                        }
                        else if (parHolder.Count == 0 && copyExpression[i] == ')')
                        {
                            copyExpression.Replace(copyExpression[i].ToString(), "}" + copyExpression[i], i, 1);
                            opened = false;
                            break;
                        }

                        parClose++;
                    }
                    else if (i == copyExpression.Length - 1)
                    {
                        if (IsClosing(copyExpression[i].ToString())) parClose++;
                        if (parClose != parOpen) copyExpression.Replace(copyExpression[i].ToString(), "}" + copyExpression[i], i, 1);
                        else copyExpression.Append("}");
                        
                        opened = false;
                        parHolder.Clear();
                        break;
                    }
                }

                //leftside
                parOpen = 0;
                parClose = 0;
                for (int i = divLocation; i >= 0; i--)
                {
                    if (copyExpression[i] == '#' && opened == false)
                    {
                        copyExpression[i] = '}';
                        opened = true;
                    }
                    else if (copyExpression[i] == '#' && opened == true)
                    {
                        copyExpression.Replace(copyExpression[i].ToString(), @"#\frac{", i, 1);
                        opened = false;
                        break;
                    }
                    else if (IsClosing(copyExpression[i].ToString()))
                    {
                        parHolder.Push(")");
                        parClose++;
                    }
                    else if (IsOpening(copyExpression[i].ToString()))
                    {
                        parOpen++;
                        if (parOpen == parClose)
                        {
                            parHolder.Pop();
                            if (parHolder.Count == 0)
                            {
                                copyExpression.Replace(copyExpression[i].ToString(), @"\frac{" + copyExpression[i], i, 1);
                                opened = false;
                                break;
                            }
                        }
                        else
                        {
                            copyExpression.Replace(copyExpression[i].ToString(), copyExpression[i] + @"\frac{", i, 1);
                            opened = false;
                            break;
                        }

                    }
                    else if (i == 0)
                    {
                        copyExpression = new StringBuilder(@"\frac{" + copyExpression);
                        opened = false;
                        break;
                    }
                    else if (parHolder.Count == 0 && IsOperator(copyExpression[i].ToString()))
                    {
                        copyExpression.Replace(copyExpression[i].ToString(), copyExpression[i] + @"\frac{", i, 1);
                        opened = false;
                        break;
                    }
                }
            }
            copyExpression.Replace("/", "");
            return copyExpression.ToString();
        }

        public bool IsDigit(char digits)
        {
            if (char.IsDigit(digits)) return true;
            if (digits == ',') return true;
            if (digits == '.') return true;
            return false;
        }

        public bool IsPolynomial(string eq)
        {
            eq = eq.ToLower();
            if (eq.Contains("z") || eq.Contains("y") || eq.Contains("x")) return true;
            return false;
        }

        public bool IsVariable(string eq)
        {
            eq = eq.ToLower();
            var exps = new Char[] { 'x', 'y', 'z' };
            return eq.IndexOfAny(exps) >= 0;
        }

        public bool IsOperator(string expression)
        {
            var exps = new Char[] { '+', '*', '/', '-' };
            return expression.IndexOfAny(exps) >= 0;
        }

        public bool IsOpening(string op)
        {
            var ops = new char[] { '(', '{', '[' };
            return op.IndexOfAny(ops) >= 0;
        }

        public bool IsClosing(string cl)
        {
            var cls = new char[] { ')', '}', ']' };
            return cl.IndexOfAny(cls) >= 0;
        }
    }
}
