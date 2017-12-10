using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5v2
{
    class Program
    {
        public static double[] deploy(double[] x)
        {
            double[] y = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = x[x.Length - i - 1];
            }
            return y;
        }

        static void Main(string[] args)
        {
            double a = -1.0;
            double b = 1.0;

            Console.Write("Enter \"n\" : ");
            int n = Convert.ToInt32(Console.ReadLine());
            int eps = 5;

            Console.WriteLine("[a; b] : [{0}; {1}] \n", a, b);
            Console.WriteLine("n : {0}", n);

            double[] x = new double[n];
            double[] y = new double[n];

            Console.WriteLine("Quality : {0}", eps);

            Console.Write("Enter x : ");
            string strx = Console.ReadLine();

            string[] line = strx.Split(' ');

            Console.Write("Your y : ");
            for (int i = 0; i < n; ++i) 
            {
                x[i] = Convert.ToDouble(line[i]);
                y[i] = Math.Round(Math.Pow(3.0,x[i]), eps);

                Console.Write(y[i] + " ");
            }
            Console.WriteLine();

            x = deploy(x);
            y = deploy(y);

            Console.WriteLine();
            Console.Write("Enter \"X0\" : ");
            double x0 = Convert.ToDouble(Console.ReadLine());
            
            double[][] rozd_rizn = new double[n][];
            rozd_rizn[0] = new double[n];

            for (int i = 0; i < n; ++i) 
            {
                rozd_rizn[0][i] = y[i];
            }

            for (int i = 1; i < n; ++i) 
            {
                rozd_rizn[i] = new double[n - i];
                for (int j = 0; j < n - i; ++j) 
                {
                    rozd_rizn[i][j] = (-rozd_rizn[i - 1][j] + rozd_rizn[i - 1][j + 1])/(x[j+i] - x[j]);
                }
            }
            
            double p = 0;
            for (int i = 0; i < n; ++i) 
            {
                double s = rozd_rizn[i][0];
                for (int j = 0; j < i; ++j) 
                {
                    s *= (x0 - x[j]); 
                }
                p += s;
            }

            Console.WriteLine();
            Console.WriteLine("Result : {0}", p);
            Console.WriteLine("Result real: {0}", Math.Pow(3.0, x0));
            Console.ReadKey();
        }
    }
}
