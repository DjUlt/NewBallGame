using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NewBallGame
{
    class Program
    {
        static public bool endgame = false;
        static public bool startgame = true;

        static int c = 0;
        //Creating field
        static GameField field1 = new GameField(GetInt(), GetInt());

        static void Main()
        {
            //Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //Console.SetWindowSize(field1.Y * 5, field1.X + Convert.ToInt32(field1.X * 0.5));

            //game setup
            field1.CreateField();
            //Timer t = new Timer(TimerCallback, null, 0, 1000);
            //Timer v = new Timer(TimerCallback1, null, 0, 100);
            startgame = false;
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += new ElapsedEventHandler(TimerT);
            t.Interval = 1000;
            t.Start();
            System.Timers.Timer v = new System.Timers.Timer();
            v.Elapsed += new ElapsedEventHandler(TimerV);
            v.Interval = 100;
            v.Start();

            //Timer with additional arguments
            //var timer = new System.Timers.Timer { Interval = 1500 };
            //timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e, field1);
            //timer.Start();


            //exit statement
            for (; ; )
            {
                if (field1.ball1.NextTrap(field1))
                {
                    endgame = true;
                }
                if (!endgame)
                {
                    if (startgame)
                    {
                        field1.CreateField();
                        t.Start();
                        v.Start();
                        startgame = false;
                    }
                    else
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
                                break;
                            case ConsoleKey.Escape:
                                Environment.Exit(0);
                                break;
                        }
                        //Console.ReadLine();
                    }
                }
                else
                {
                    GameEndField(t, v);
                }
            }
        }

        private static void GameEndField(System.Timers.Timer t, System.Timers.Timer v)
        {
            t.Stop();
            v.Stop();
            field1.ClearField();
            Console.Clear();
            Console.WriteLine("\n\n\n                      Game Over");
            Console.WriteLine("                          :(\n");
            Console.WriteLine("                    press R to retry");
            Console.WriteLine("                M to return to main menu");
            Console.WriteLine("                   E to exit the game");
            var ch = Console.ReadKey(true).Key;
            //var ch = Console.Read();
            switch (ch)
            {
                case ConsoleKey.R:
                //case 'r':
                    startgame = true;
                    field1.Orbs = 0;
                    endgame = false;
                    Console.Clear();
                    break;
                case ConsoleKey.M:
                //case 'm':
                    Console.WriteLine("                      Currently no Menu/n                   Press Enter to exit");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                case ConsoleKey.E:
                //case 'e':
                    Environment.Exit(0);
                    break;
            }
            //endgame = false;
            //Console.Clear();
        }

        private static int GetInt()//make square for diag
        {
            if (c == 0) { Console.WriteLine("Set height of the play field"); c++; }
            else Console.WriteLine("Set width of the play field");
            return Convert.ToInt32(Console.ReadLine());
        }

        //private static void TimerCallback(Object o)
        private static void TimerT(object source, ElapsedEventArgs e)
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

        //private static void TimerCallback1(Object o)
        private static void TimerV(object source, ElapsedEventArgs e)
        {
            //visualize
            Console.Clear();
            Console.Write(field1.Visualize());

            // 0 - #, 1 - @, 2 - •, 3 - /, 4 - \, 5 - ₴, 6 - +, 7 - ;8 - ■
            Console.WriteLine("Orbs collected: " + field1.Orbs);

            //Console.WriteLine(field1.SearcherLeftTop(1)[0]+" "+field1.SearcherLeftTop(1)[1]);//SearchLeftTop
            
            Console.WriteLine("Controls: \nZ - /, X - \\, C - clear.\nUse arrows to move cursor.\nPress ESC to exit.");
            //Console.WriteLine("Selector: "+field1.selector.X + " " + field1.selector.Y);
            //Console.WriteLine("BallD: " + field1.ball1.Dx + " " + field1.ball1.Dy);
            //Console.WriteLine("BallC: " + field1.ball1.X + " " + field1.ball1.Y);
            
            //if (endgame)
            //{
            //    Console.Clear();
            //    Console.WriteLine("Press Enter to proceed");
            //}

            //Forced garbage collect
            GC.Collect();
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
