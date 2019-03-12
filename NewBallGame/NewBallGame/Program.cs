using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Timers;

namespace NewBallGame
{
    class Program
    {
        static int c = 0;
        //Creating field
        static GameField field1 = new GameField(GetInt(), GetInt());

        static void Main()
        {
            //Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.SetWindowSize(field1.Y * 5, field1.X + Convert.ToInt32(field1.X * 0.5));

            //creating field
            //GameField field1 = new GameField(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
            field1.CreateField();

            //setting timer

            //var timer = new System.Timers.Timer { Interval = 1500 };
            //timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e, field1);
            //timer.Start();
            //OR
            Timer t = new Timer(TimerCallback, null, 0, 1000);
            Timer v = new Timer(TimerCallback1, null, 0, 100);

            //exit statement
            for (; ; )
            {

                var ch = Console.ReadKey(true).Key;
                switch (ch)
                {
                    case ConsoleKey.LeftArrow:
                        field1.selector.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        field1.selector.MoveRight();
                        break;
                    case ConsoleKey.DownArrow:
                        field1.selector.MoveUp();
                        break;
                    case ConsoleKey.UpArrow:
                        field1.selector.MoveDown();
                        break;
                    case ConsoleKey.Z:
                        field1.selector.SetS(field1);
                        break;
                    case ConsoleKey.X:
                        field1.selector.SetBS(field1);
                        break;
                    case ConsoleKey.C:
                        field1.selector.SetC(field1);
                        //field1.RandomAdd(1, field1);
                        break;
                }
                //Console.ReadLine();
            }
        }

        private static int GetInt()//make square
        {
            if (c == 0) { Console.WriteLine("Set height of the play field"); c++; }
            else Console.WriteLine("Set width of the play field");
            return Convert.ToInt32(Console.ReadLine());
        }

        private static void TimerCallback(Object o)
        {
            //Case of all orbs absorbed
            if (field1.IsCleared())
            {
                field1.ClearField();
                field1.CreateField();
            }

            //moveball
            field1.ball1.Move(field1);

            //Forced garbage collect
            GC.Collect();
        }

        private static void TimerCallback1(Object o)
        {
            //visualize
            Console.Clear();
            Console.Write(field1.Visualize());

            // 0 - #, 1 - @, 2 - •, 3 - /, 4 - \, 5 - ₴, 6 - +, 7 - ;8 - ■
            Console.WriteLine("Orbs collected: " + field1.Orbs);

            //Console.WriteLine(field1.SearcherLeftTop(1)[0]+" "+field1.SearcherLeftTop(1)[1]);//SearchLeftTop
            
            Console.WriteLine("Controls: Z - /,X - \\, C - clear.\nUse arrows to move cursor.");
            //Console.WriteLine("Selector: "+field1.selector.X + " " + field1.selector.Y);
            //Console.WriteLine("BallD: " + field1.ball1.Dx + " " + field1.ball1.Dy);
            //Console.WriteLine("BallC: " + field1.ball1.X + " " + field1.ball1.Y);
        }

            //public static void MyElapsedMethod(object sender, ElapsedEventArgs e, GameField field1)
            //{

            //    Console.Clear();
            //    //read user actions
            //    //moveball
            //    field1.ball1.Move(field1);

            //    //visualize
            //    Console.Write(field1.Visualize());
            //    //Case of all orbs absorbed
            //    //field1.clearField();
            //    Console.WriteLine(field1.ball1.X);
            //}
        }
}
