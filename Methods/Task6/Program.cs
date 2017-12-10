using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  Real result */
            double real_result = 0.195098;

            /* Info for Gause with 4 points */
            int gause4points = 4;

            double[] gause4points_t = new double[gause4points];
            double[] gause4points_c = new double[gause4points];

            gause4points_t[0] = -0.861136;
            gause4points_t[1] = -0.339981;
            gause4points_t[2] = 0.339981;
            gause4points_t[3] = 0.861136;

            gause4points_c[0] = 0.347855;
            gause4points_c[1] = 0.652145;
            gause4points_c[2] = 0.652145;
            gause4points_c[3] = 0.347855;

            /* Info for Gause with 5 points */
            int gause5points = 5;

            double[] gause5points_t = new double[gause5points];
            double[] gause5points_c = new double[gause5points];

            gause5points_t[0] = -0.90618;
            gause5points_t[1] = -0.538469;
            gause5points_t[2] = 0;
            gause5points_t[3] = 0.538469;
            gause5points_t[4] = 0.90618;

            gause5points_c[0] = 0.23693;
            gause5points_c[1] = 0.47863;
            gause5points_c[2] = 0.56889;
            gause5points_c[3] = 0.47863;
            gause5points_c[4] = 0.23693;

            /*  Info from my variant    */
            double a = 3;
            double b = 4;
            int eps;

            /* Program  */
            Console.WriteLine("Enter the folowing information : \n");

            Console.Write("\t Enter accuracy of epsilon : ");
            eps = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n");

            Console.WriteLine("Calculating of the integral : \n");

            Console.WriteLine("\t Method of the rectangles : {0}", methodOfRectangles(a,b,eps));
            Console.WriteLine("\t Method of the trapezes : {0}", methodOfTrapezes(a, b, eps));
            Console.WriteLine("\t Method of the parabols : {0}", methodOfParabols(a, b, eps));
            Console.WriteLine("\t Method of Gause (4 points) : {0}", methodOfGause(a, b, gause4points, gause4points_t, gause4points_c));
            Console.WriteLine("\t Method of Gause (5 points) : {0}", methodOfGause(a, b, gause5points, gause5points_t, gause5points_c));

            Console.WriteLine("\n\n\t Real result : {0}", real_result);
            Console.WriteLine();

            double k;
            Console.WriteLine("The orders of convergence of the methods : \n");

            k = Math.Log((methodOfRectangles(a, b, eps) - methodOfRectangles(a, b, eps / 2)) / (methodOfRectangles(a, b, eps / 2) - methodOfRectangles(a, b, eps / 4))) / Math.Log(2);
            Console.WriteLine("\t Method of the rectangles : {0}", k);

            k = Math.Log((methodOfTrapezes(a, b, eps) - methodOfTrapezes(a, b, eps / 2)) / (methodOfTrapezes(a, b, eps / 2) - methodOfTrapezes(a, b, eps / 4))) / Math.Log(2);
            Console.WriteLine("\t Method of the trapezes : {0}", k);

            k = Math.Log((methodOfParabols(a, b, eps) - methodOfParabols(a, b, eps / 2)) / (methodOfParabols(a, b, eps / 2) - methodOfParabols(a, b, eps / 4))) / Math.Log(2);
            Console.WriteLine("\t Method of the parabols : {0}", k);
            Console.WriteLine();

            Console.WriteLine("Comparison of results : \n");

            double compute_result = derivative(b) - derivative(a);

            Console.WriteLine("\t Real result : {0}", real_result);
            Console.WriteLine("\t Compute result : {0}", compute_result);

            Console.ReadLine();
        }

        /*  Method of rectangles    */
        public static double methodOfRectangles(double a, double b, int n)
        {
            double s = 0;
            double h = (b - a) / n;
            double x = a;

            while (x <= b)
            {
                double F = function(x);
                s += F;
                x += h;
            }

            s *= h;
            return s;
        }

        /*  Method of trapazes  */
        public static double methodOfTrapezes(double a, double b, int n)
        {
            double s = 0;
            double h = (b - a) / n;
            double x = a + h;

            while (x <= b)
            {
                double F = function(x);
                s += F;
                x += h;
            }

            double Fa = function(a);
            double Fb = function(b);

            s = (h / 2) * (Fa + Fb + 2 * s);
            return s;
        }

        /*  Method of parabols  */
        public static double methodOfParabols(double a, double b, int n)
        {
            double h = (b - a) / n;

            double[] x = new double[n];
            double[] y = new double[n];

            x[0] = a;
            y[0] = function(x[0]);

            for (int i = 1; i < n; i++)
            {
                x[i] = x[i - 1] + h;
                y[i] = function(x[i]);
            }

            double s1 = y[0] + y[n - 1];
            double s2 = 0;
            double s3 = 0;

            for (int i = 1; i < n - 1; i++)
            {
                if (i % 2 != 0)
                {
                    s2 += y[i];
                }
                else
                {
                    s3 += y[i];
                }
            }

            double s = (h / 3) * (s1 + 4 * s2 + 2 * s3);
            return s;
        }

        /*  Method of Gause */
        public static double methodOfGause(double a, double b, int n, double[] t, double[] c)
        {
            double[] x = new double[n];

            for (int i = 0; i < n; i++)
            {
                x[i] = ((b + a) / 2) + ((b - a) / 2) * t[i];
            }

            double s = 0;

            for (int i = 0; i < n; i++)
            {
                s += c[i] * function(x[i]);
            }

            s *= (b - a) / 2;
            return s;
        }

        /*  Function    */
        public static double function(double x)
        {
            return Math.Pow(Math.E, x) / (4 + 5 * Math.Pow(Math.E, x));
        }

        /*  Derivative    */
        public static double derivative(double x)
        {
            return Math.Log(4 + 5 * Math.Pow(Math.E, x))/ 5;
        }
    }
}
