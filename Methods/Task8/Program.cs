using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Your the values : \n");

            double a = 0;
            double b = 1;
            double h = 0.1;
            double y0 = 1;

            Console.WriteLine("\t Your \"a\" : {0}", a);
            Console.WriteLine("\t Your  \"b\" : {0}", b);
            Console.WriteLine("\t Your \"h\" : {0}", h);
            Console.WriteLine("\t Your \"y0\" : {0}", y0);

            Console.WriteLine();

            Console.WriteLine("The results are : \n");

            Console.WriteLine("\t Method of Eiler : \n");

            int n = 11;

            double[,] eresult = Eiler(a, b, h, y0, n);

            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine("\t {0} \t| {1}", eresult[i, 0], eresult[i, 1]);
            }

            Console.WriteLine();

            Console.WriteLine("\t Method of Runhe-Cutta : \n");

            double[,] rresult = Runhe_Cutta(a, b, h, y0, n);

            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine("\t {0} \t| {1}", rresult[i, 0], rresult[i, 1]);
            }

            Console.WriteLine();

            Console.WriteLine("\t Exact value : \n");

            double[,] exresult = Exact(a, b, h, y0, n);

            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine("\t {0} \t| {1}", exresult[i, 0], exresult[i, 1]);
            }


            Console.WriteLine();

            Console.WriteLine("\t Adams value : \n");

            double[,] adresult = Adams(a, b, h, y0, n);

            for (int i = 0; i < n; ++i)
            {
                Console.WriteLine("\t {0} \t| {1}", adresult[i, 0], adresult[i, 1]);
            }

            Console.ReadLine();
        }

        /*  Method of Eiler */
        public static double[,] Eiler(double a, double b, double h, double y0, int n)
        {
            double[,] result = new double[n, 2];

            result[0, 0] = a;
            result[0, 1] = y0;

            double xn = a + h;

            for(int i = 1; i < n; ++i)
            {
                result[i, 0] = result[i - 1, 0] + h;
                result[i, 1] = result[i - 1, 1] + h * function(result[i-1, 0], result[i-1, 1]);
            }

            return result;
        }

        /*  Runhe-Cutta */
        public static double[,] Runhe_Cutta(double a, double b, double h, double y0, int n)
        {
            double[,] result = new double[n, 2];

            result[0, 0] = a;
            result[0, 1] = y0;

            double k1 = 0;
            double k2 = 0;

             for(int i = 1; i < n; ++i)
            {
                k1 = h * function(result[i - 1, 0], result[i - 1, 1]);
                k2 = h * function(result[i - 1, 0] + (h / 2), result[i - 1, 1] + (k1 / 2));

                result[i, 0] = result[i - 1, 0] + h;
                result[i, 1] = result[i - 1, 1] + k2;
            }

            return result;
        }

        /*  Exact value*/
        public static double[,] Exact(double a, double b, double h, double y0, int n)
        {
            double[,] result = new double[n, 2];

            result[0, 0] = a;
            result[0, 1] = exact_function(a);

            for (int i = 1; i < n; ++i)
            {
                result[i, 0] = result[i - 1, 0] + h;
                result[i, 1] = exact_function(result[i, 0]);
            }

            return result;
        }

        /*  Function    */
        public static double function(double x, double y)
        {
            return 5 - Math.Pow(x, 2) - Math.Pow(y, 2) + 2 * x * y;
        }

        /*  Exact function    */
        public static double exact_function(double x)
        {
            return x + 2 - (4 / (3 * Math.Pow(Math.E, 4 * x) + 1));
        }

        /*  Adams mrthod    */
        public static double[,] Adams(double a, double b, double h, double y0, int n)
        {
            double[,] result = new double[n, 2];

            result[0, 0] = a;
            result[0, 1] = exact_function(a);

            result[1, 0] = a + h;
            result[1, 1] = exact_function(result[1, 0]);

            result[2, 0] = a + 2 * h;
            result[2, 1] = exact_function(result[2, 0]);

            for (int i = 2; i < n - 1; ++i)
            {
                double k1 = function(result[i, 0], result[i, 1]) * h;
                double k2 = function(result[i - 1, 0], result[i - 1, 1]) * h;
                double k3 = function(result[i - 2, 0], result[i - 2, 1]) * h;

                double delta_y = (23 * k1 - 16 * k2 + 5 * k3) / 12;
                
                result[i + 1, 0] = result[i, 0] + h;
                result[i + 1, 1] = result[i, 1] + delta_y;
            }

            return result;
        }
    }
}
