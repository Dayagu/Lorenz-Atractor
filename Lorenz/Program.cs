//#define BUFFER

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lorenz
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double x0 = 0.1, y0 = 0, z0 = 0;
            double t = 0.01;
            //lorenz
            double a = 10.0;
            double b = 28.0;
            double c = 8.0 / 3.0;

            int count = 0;

            char[,] bufer = new char[300, 300];

            Console.Clear();
            //Console.SetCursorPosition(0, 0);
            Console.SetBufferSize(500, 500);
            Console.CursorVisible = false;



#if BUFFER
            for (var i = 0; i < 1000; i++)
#else
            Queue<Coords> cola = new Queue<Coords>();
            while (true)
#endif
            {
                try
                {
                    // lorenz
                    var x1 = x0 + t * a * (y0 - x0);
                    var y1 = y0 + t * (x0 * (b - z0) - y0);
                    var z1 = z0 + t * (x0 * y0 - c * z0);

                    x0 = x1;
                    y0 = y1;
                    z0 = z1;


                    count++;

                    //Console.SetCursorPosition(0, 0);
                    //Console.WriteLine("{0}", y0);

                    int x00 = Convert.ToInt32(Math.Round(100 + 5 * x0));
                    int z00 = Convert.ToInt32(Math.Round(20 + 5 * z0));

                    var adonde = safecords(x00, z00);
                    x00 = adonde.X;
                    z00 = adonde.Y;

#if BUFFER
                //bufer[x00, z00] = '+';
                //var adonde = safecords(x00, z00);
                bufer[adonde.X,adonde.Y] = '+';

#else
                    cola.Enqueue(new Coords(x00, z00));

                    if (cola.Count > 60)
                    {
                        var remove = cola.Dequeue();
                        Console.SetCursorPosition(remove.X, remove.Y);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(' ');

                    }
                    var colores = cola.ToArray();

                    if (colores.Length > 30)
                    {
                        Console.SetCursorPosition(colores[1].X, colores[1].Y);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('●');//◊☻

                        for (var i = 2; i < 15; i++)
                        {
                            Console.SetCursorPosition(colores[i].X, colores[i].Y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('░');//◦·
                        }
                        for (var i = 15; i < 25; i++)
                        {
                            Console.SetCursorPosition(colores[i].X, colores[i].Y);
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write('▒');//◌○*
                        }
                        for (var i = 25; i < 30; i++)
                        {
                            Console.SetCursorPosition(colores[i].X, colores[i].Y);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('▓');//o
                        }

                    }

                    Thread.Sleep(20);

                    Console.SetCursorPosition(x00, z00);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write('█');//●x
                    Console.ResetColor();

                    Console.CursorVisible = false;
               
#endif

                }
                catch { }
            }

#if BUFFER
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
#endif

            Console.ReadKey();
        }
        private static Coords safecords(int x, int y)
        {

#if BUFFER
            const int ancho = 132;
            const int alto = 40;
#else
            int ancho = Console.WindowWidth - 1;
            int alto = Console.WindowHeight - 1;
#endif
            x = (x * ancho) / 300;
            y = (y * alto) / 300;

            return new Coords(x, y);

        }
    }
    public struct Coords
    {
        public Coords(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public override string ToString() => $"({X}, {Y})";
    }

}

