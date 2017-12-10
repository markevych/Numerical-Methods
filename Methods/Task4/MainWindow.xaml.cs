using System;
using System.Collections.Generic;
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

namespace Task4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public double Lagrange(double[] x, double[] y, int n, double x_find)
        {
            double S = 0;
            double d1;
            double d2;


            listo.Items.Clear();

            for (int i = 0; i < n; i++)
            {
                d1 = 1;
                d2 = 1;

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        d1 = d1 * (x_find - x[j]);
                        d2 = d2 * (x[i] - x[j]);
                    }
                }

                S = S + (d1 / d2) * y[i];

                ListBoxItem item = new ListBoxItem();
                item.Content = S.ToString();
                listo.Items.Add(item);
            }

            return S;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int n = 5;

            double[] x = new double[n];
            double[] y = new double[n]; 

            double x_find = 9;
            
            string str_x = "6 7,2 8,4 9,9 10,3";
            string str_y = "-9,1 -8,3 -2,4 1,2 3,4";

            string[] x_el = str_x.Split(' ');
            string[] y_el = str_y.Split(' ');

            for (int i = 0; i < n; i++)
            {
                x[i] = Convert.ToDouble(x_el[i]);
                y[i] = Convert.ToDouble(y_el[i]);
            }

            result_box.Text = Lagrange(x, y, n, x_find).ToString();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
