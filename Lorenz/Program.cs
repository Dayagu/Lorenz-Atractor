using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorenz
{
    class Program
    {
        static void Main(string[] args)
        {
            double minx = 0, miny = 0;


            double x0 = 0.1, y0 = 0, z0 = 0;
            double t = 0.01;
            double a = 10.0;
            double b = 28.0;
            double c = 8.0 / 3.0;
            int count = 0;

            char[,] bufer = new char[300, 300];

            Console.SetCursorPosition(0, 0);
            Console.SetBufferSize(1000, 1000);

            for (var i = 0; i < 5000; i++)
            {
                var x1 = x0 + t * a * (y0 - x0);
                var y1 = y0 + t * (x0 * (b - z0) - y0);
                var z1 = z0 + t * (x0 * y0 - c * z0);
                x0 = x1;
                y0 = y1;
                z0 = z1;
                count++;

                int x00 = Convert.ToInt32(Math.Round(100 + 5 * x0));
                int z00 = Convert.ToInt32(Math.Round(20 + 5 * z0));

                bufer[x00, z00] = '+';


                //Console.WriteLine("{0},{1}", x00,z00 );
                //Console.SetCursorPosition(x00, z00);
                //Console.Write('·');
            }

            StringBuilder strout = new StringBuilder();
            for (var i = 0; i < 300; i++)
            {
                for (var j = 0; j < 300; j++)
                {
                    strout.Append(bufer[j, i]);
                }
                strout.Append('\n');
            }

            Console.Write(strout);
            Console.ReadKey();
        }



    }
}

