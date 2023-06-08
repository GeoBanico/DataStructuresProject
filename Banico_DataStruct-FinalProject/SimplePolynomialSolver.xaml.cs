using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Banico_DataStruct_FinalProject.MainClass;
using Banico_DataStruct_FinalProject.NumericalEquations;
using Banico_DataStruct_FinalProject.XSimplePolynomialSolver;

namespace Banico_DataStruct_FinalProject
{
    /// <summary>
    /// Interaction logic for SimplePolynomialSolver.xaml
    /// </summary>
    public partial class SimplePolynomialSolver : Window
    {
        private MainClassMethods mainMethods = new MainClassMethods();
        private bool Eq1Checker = false;
        private bool Eq2Checker = false;
        private string ans = "";
        private int Cc = 0;

        const string postFileName = @"C:\Users\Geo\source\repos\Banico_DataStruct-FinalProject\EqTot.png";

        public SimplePolynomialSolver()
        {
            InitializeComponent();
        }

        private void TxtEq1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ImgPreEq1.Source = null;

            var formulaChecker = new DelimeterMatcher();
            var newEq1 = formulaChecker.ToSimplify(TxtEq1.Text);
            var checkEquation = formulaChecker.DelimiterMatcher(newEq1);

            if (!checkEquation || newEq1 == "")
            {
                LblFormat.Content = "Equation Error";
                LblFormat.Foreground = Brushes.Red;
                ImgPreEq1.Source = null;
                Eq1Checker = false;
            }
            else
            {
                var laTexEquation = $"({mainMethods.StringToTeX(newEq1)})";
                string fileName = @"C:\Users\Geo\source\repos\Banico_DataStruct-FinalProject\Eq1.png";

                ImgPreEq1.Source = mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(laTexEquation, fileName)));
                Eq1Checker = true;
            }

            if (Eq1Checker && Eq2Checker && CmbOperation.Text != "")
            {
                BtnSolve.IsEnabled = true;
                LblFormat.Content = "Checked Equation";
                LblFormat.Foreground = Brushes.Green;
            }
            else BtnSolve.IsEnabled = false;
        }

        private void TxtEq2_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ImgPreEq2.Source = null;

            var formulaChecker = new DelimeterMatcher();
            var newEq2 = formulaChecker.ToSimplify(TxtEq2.Text);
            var checkEquation = formulaChecker.DelimiterMatcher(newEq2);

            if (!checkEquation || newEq2 == "")
            {
                LblFormat.Content = "Equation Error";
                LblFormat.Foreground = Brushes.Red;
                ImgPreEq2.Source = null;
                Eq2Checker = false;
            }
            else
            {
                var laTexEquation = $"({mainMethods.StringToTeX(newEq2)})";
                string fileName = @"C:\Users\Geo\source\repos\Banico_DataStruct-FinalProject\Eq2.png";

                ImgPreEq2.Source =
                    mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(laTexEquation, fileName)));
                Eq2Checker = true;

            }

            if (Eq1Checker && Eq2Checker && CmbOperation.Text != "")
            {
                BtnSolve.IsEnabled = true;
                LblFormat.Content = "Checked Equation";
                LblFormat.Foreground = Brushes.Green;
            }
            else BtnSolve.IsEnabled = false;
        }

        private void CmbOperation_DropDownClosed(object sender, EventArgs e)
        {
            LblOp.Content = CmbOperation.Text;
            if (Eq1Checker && Eq2Checker && CmbOperation.Text != "")
            {
                BtnSolve.IsEnabled = true;
                LblFormat.Content = "Checked Equation";
                LblFormat.Foreground = Brushes.Green;
            }
            else BtnSolve.IsEnabled = false;
        }

        private void BtnSolve_Click(object sender, RoutedEventArgs e)
        {
            var LatexEquation = "";
            if (CmbOperation.Text == "*") BtnSwitch.IsEnabled = true;
            else if (CmbOperation.Text != "*") BtnSwitch.IsEnabled = false;

            var equation = $"({TxtEq1.Text}){CmbOperation.Text}({TxtEq2.Text})";

            if (mainMethods.IsPolynomial(equation))
            {
                ans = new PolynomialCompiler().SolvePolynomial(TxtEq1.Text, TxtEq2.Text, CmbOperation.Text);
                if (ans == "") ans = "0";
                
                LatexEquation = $"({mainMethods.StringToTeX(ans)})";
            }
            else
            {
                var compile = new ArithmeticCompiler().CompileArthmetic(equation);
                LatexEquation = $"({mainMethods.StringToTeX(compile.ToString())})";
            }

            ImgPost.Source = mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(LatexEquation, postFileName)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Shutdown the application.
            Application.Current.Shutdown();
            // OR You can Also go for below logic
            // Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var LatexEquation = "";
            if (Cc == 0)
            {
                var compile = $"({TxtEq1.Text})({TxtEq2.Text})";
                LatexEquation = $"({mainMethods.StringToTeX(compile)})";
                Cc++;
            }
            else
            {
                LatexEquation = $"({mainMethods.StringToTeX(ans)})";
                Cc = 0;
            }
            ImgPost.Source = mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(LatexEquation, postFileName)));

        }
    }
}
