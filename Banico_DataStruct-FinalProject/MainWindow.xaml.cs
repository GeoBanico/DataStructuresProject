using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Accessibility;
using Banico_DataStruct_FinalProject.MainClass;
using Banico_DataStruct_FinalProject.NumericalEquations;
using Brushes = System.Windows.Media.Brushes;

namespace Banico_DataStruct_FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainClassMethods mainMethods = new MainClassMethods();

        public MainWindow()
        {
            InitializeComponent();
            txtEq.Text = "sqrt(3*sqrt(4x-y^2/5))";
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            if (mainMethods.IsPolynomial(txtEq.Text))
            {
                MessageBox.Show("Polynomial Equation");
            }
            else
            {
                var compile = new ArithmeticCompiler().CompileArthmetic(txtEq.Text);
                var fileName = @"C:\Users\Geo\Desktop\Programming\WPFMathSample\Sample.png";
                imgEndEqDisplay.Source = mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(compile.ToString(),fileName)));
            }
        }

        private void txtEq_SelectionChanged(object sender, RoutedEventArgs e)
        {
            imgEndEqDisplay.Source = null;

            var formulaChecker = new DelimeterMatcher();
            var newEq = formulaChecker.ToSimplify(txtEq.Text);
            var CheckEquation = formulaChecker.DelimiterMatcher(newEq);

            if (!CheckEquation || (newEq == " " || newEq == ""))
            {
                lblFormat.Content = "Incorrect Equation";
                lblFormat.Foreground = Brushes.Red;

                imgEqDisplay.Source = null;
                btnSolve.IsEnabled = false;
            }
            else
            {
                var laTexEquation = mainMethods.StringToTeX(newEq);
                string fileName = @"C:\Users\Geo\Desktop\Programming\WPFMathSample\Sample.png";
                imgEqDisplay.Source = mainMethods.BitmapFromUri(new Uri(mainMethods.GetPngEquation(laTexEquation, fileName)));
                btnSolve.IsEnabled = true;
                lblFormat.Content = "Checked Equation";
                lblFormat.Foreground = Brushes.Green;
            }
        }
    }
}
