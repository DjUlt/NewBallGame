using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.IO;

namespace NewBallGame
{
    class Program
    {
        static public bool endgame = false;
        static public bool startgame = false;
        static public bool mainmenu = true;
        public static int selector = 1;
        public static int totaltime = 0;
        public static int clearedfields = 0;
        public static string output="";

        static System.Timers.Timer t = new System.Timers.Timer();
        static System.Timers.Timer v = new System.Timers.Timer();
        static System.Timers.Timer s = new System.Timers.Timer();

        //Creating field
        static GameField field1;

        static void Main()
        {
            MainMenu test = new MainMenu();
            test.ShowDialog();
            //Console.BackgroundColor = ConsoleColor.Black; Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Title = "New Ball Game";
            //Console.SetWindowSize(field1.Y * 5, field1.X + Convert.ToInt32(field1.X * 0.5));
            int c;
            //game setup
            //Timer t = new Timer(TimerCallback, null, 0, 1000);
            //Timer v = new Timer(TimerCallback1, null, 0, 100);
            t.Elapsed += new ElapsedEventHandler(TimerT);
            t.Interval = 1000;
            v.Elapsed += new ElapsedEventHandler(TimerV);
            v.Interval = 200;
            //s.Elapsed += new ElapsedEventHandler(TimerS);
            //s.Interval = 200;
            //s.Start();

            //Timer with additional arguments
            //var timer = new System.Timers.Timer { Interval = 1500 };
            //timer.Elapsed += (sender, e) => MyElapsedMethod(sender, e, field1);
            //timer.Start();


            //exit statement
            for (; ; )
            {
                if (!mainmenu)
                {
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
                        if (field1.ball1.NextTrap(field1))
                        {
                          endgame = true;
                        }
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
                            case ConsoleKey.R:
                                    v.Stop();
                                    t.Stop();
                                    totaltime = 0;
                                    clearedfields = 0;
                                    endgame = false;
                                    startgame = false;
                                    mainmenu = true;
                                    break;
                            }
                        //Console.ReadLine();
                    }
                }
                else
                {
                    totaltime += field1.time;
                    GameEndField(t, v);
                }
                }
                else
                {
                    t.Stop();
                    v.Stop();
                    Console.Clear();
                    
                    for (bool lol=false;!lol ; )
                    {
                        Console.Clear();
                        //mainmenu text
                        Console.WriteLine("\n\n\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("          MAIN MENU\n\n\n          ");
                        if(selector == 1) Console.BackgroundColor = ConsoleColor.Green;
                        else Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Start game\n\n");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("          ");
                        if (selector == 2) Console.BackgroundColor = ConsoleColor.Green;
                        else Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Hall of fame\n\n");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("          ");
                        if (selector == 3) Console.BackgroundColor = ConsoleColor.Green;
                        else Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("Exit game\n\n");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("Use arrows to choose, enter to select");
                        s.Start();

                        var ch = Console.ReadKey(true).Key;
                        switch (ch)
                        {
                            case ConsoleKey.DownArrow:
                                if (selector == 1) selector = 2;
                                else if (selector == 2) selector = 3;
                                else if (selector == 3) selector = 1;
                                break;
                            case ConsoleKey.UpArrow:
                                if (selector == 2) selector = 1;
                                else if (selector == 1) selector = 3;
                                else if (selector == 3) selector = 2;
                                break;
                            case ConsoleKey.Enter:
                                if (selector == 1)
                                {
                                    startgame = true;
                                    Console.Clear();
                                    c = GetInt();
                                    field1 = new GameField(c, c);
                                    mainmenu = false;
                                    s.Stop();
                                    lol = true;
                                }
                                else if(selector == 3)
                                {
                                    Environment.Exit(0);
                                }
                                else if(selector == 2)
                                {
                                    Form1 form = new Form1();
                                    form.ShowDialog();
                                }
                                break;
                            case ConsoleKey.Escape:
                                Environment.Exit(0);
                                break;
                        }
                    }
                    }
            }
        }

        private static void GameEndField(System.Timers.Timer t, System.Timers.Timer v)
        {
            v.Stop();
            t.Stop();
            field1.ClearField();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("\n\n\n                      Game Over");
            Console.WriteLine("                          :(\n");
            Console.WriteLine("                time spent(m.s.ms): " + (int)(totaltime / 60000) + "." + (int)((totaltime / 1000) % 60) + "." + totaltime % 1000 + "\n");
            Console.WriteLine("                orbs collected: " + field1.Orbs + "\n");
            Console.WriteLine("                total points: " + (int)(field1.Orbs*233+60000*clearedfields/Math.Sqrt(totaltime)) + "\n\n");
            Console.WriteLine("                    press N to add your score to the Hall of fame");
            Console.WriteLine("                    press R to retry");
            Console.WriteLine("                M to return to main menu");
            Console.WriteLine("                   ESC to exit the game");
            clearedfields = 0;
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
                    endgame = false;
                    startgame = false;
                    mainmenu = true;
                    break;
                case ConsoleKey.N:
                    //System.IO.StreamWriter file = new System.IO.StreamWriter(@"Records.txt");
                    //FileStream stream = new FileStream(@"Records.txt", FileMode.Open, FileAccess.Write, FileShare.None);
                    string line = InputBox.Show();
                    //TextWriter writer = new StreamWriter(stream);
                    line += " - " + (int)(field1.Orbs * 233 + 60000 * clearedfields / Math.Sqrt(totaltime))+"\n";
                    File.AppendAllText("Records.txt", line);
                    //writer.Write(line);
                    //writer.Close();
                    endgame = false;
                    startgame = false;
                    mainmenu = true;
                    break;
                case ConsoleKey.Escape:
                //case 'e':
                    Environment.Exit(0);
                    break;
            }
            //endgame = false;
            //Console.Clear();
        }

        //private static void Song()
        //{
        //    s.Interval = 25000;
        //    Console.Beep(440, 500);
        //    Console.Beep(440, 500);
        //    Console.Beep(440, 500);
        //    Console.Beep(349, 350);
        //    Console.Beep(523, 150);
        //    Console.Beep(440, 500);
        //    Console.Beep(349, 350);
        //    Console.Beep(523, 150);
        //    Console.Beep(440, 1000);
        //    Console.Beep(659, 500);
        //    Console.Beep(659, 500);
        //    Console.Beep(659, 500);
        //    Console.Beep(698, 350);
        //    Console.Beep(523, 150);
        //    Console.Beep(415, 500);
        //    Console.Beep(349, 350);
        //    Console.Beep(523, 150);
        //    Console.Beep(440, 1000);
        //    Console.Beep(880, 500);
        //    Console.Beep(440, 350);
        //    Console.Beep(440, 150);
        //    Console.Beep(880, 500);
        //    Console.Beep(830, 250);
        //    Console.Beep(784, 250);
        //    Console.Beep(740, 125);
        //    Console.Beep(698, 125);
        //    Console.Beep(740, 250);
        //    Thread.Sleep(250); // Delay 250 milliseconds !!!! 
        //    Console.Beep(455, 250);
        //    Console.Beep(622, 500);
        //    Console.Beep(587, 250);
        //    Console.Beep(554, 250);
        //    Console.Beep(523, 125);
        //    Console.Beep(466, 125);
        //    Console.Beep(523, 250);
        //    Thread.Sleep(250); // Delay 250 milliseconds !!!! 
        //    Console.Beep(349, 125);
        //    Console.Beep(415, 500);
        //    Console.Beep(349, 375);
        //    Console.Beep(440, 125);
        //    Console.Beep(523, 500);
        //    Console.Beep(440, 375);
        //    Console.Beep(523, 125);
        //    Console.Beep(659, 1000);
        //    Console.Beep(880, 500);
        //    Console.Beep(440, 350);
        //    Console.Beep(440, 150);
        //    Console.Beep(880, 500);
        //    Console.Beep(830, 250);
        //    Console.Beep(784, 250);
        //    Console.Beep(740, 125);
        //    Console.Beep(698, 125);
        //    Console.Beep(740, 250);
        //    Thread.Sleep(250);
        //    Console.Beep(455, 250);
        //    Console.Beep(622, 500);
        //    Console.Beep(587, 250);
        //    Console.Beep(554, 250);
        //    Console.Beep(523, 125);
        //    Console.Beep(466, 125);
        //    Console.Beep(523, 250);
        //    Thread.Sleep(250);
        //    Console.Beep(349, 250);
        //    Console.Beep(415, 500);
        //    Console.Beep(349, 375);
        //    Console.Beep(523, 125);
        //    Console.Beep(440, 500);
        //    Console.Beep(349, 375);
        //    Console.Beep(261, 125);
        //    Console.Beep(440, 1000);
        //    Thread.Sleep(100);
        //}

        private static int GetInt()//make square for diag
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n\n\n   Set height and width of the play field\n\n                   ");
            return Convert.ToInt32(Console.ReadLine());
        }

        //private static void TimerS(object source, ElapsedEventArgs e)
        //{
        //    //Song();
        //}

            //private static void TimerCallback(Object o)
            private static void TimerT(object source, ElapsedEventArgs e)
        {
            //Case of all orbs absorbed
            if (field1.IsCleared())
            {
                clearedfields++;
                totaltime += field1.time;
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
            //Console.WriteLine(field1.Visualize());
            field1.Visualize();

            // 0 - #, 1 - @, 2 - •, 3 - /, 4 - \, 5 - ₴, 6 - +, 7 - ;8 - ■
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nOrbs collected: " + field1.Orbs);
            field1.time+=200;
            Console.WriteLine("\nTime spent(m.s.ms): " + (int)(field1.time / 60000) + "." + (int)((field1.time / 1000) % 60) + "." + field1.time % 1000);
            Console.WriteLine("\nTime left(m.s.ms): " + (int)((60000*field1.X/2 - field1.time) / 60000) + "." + (int)(((60000 * field1.X / 2 - field1.time) /1000) %60) + "." + (60000 * field1.X / 2 - field1.time) % 1000);
            if (60000 * field1.X / 2 - field1.time < 1) endgame = true;
            //Console.WriteLine(field1.SearcherLeftTop(1)[0]+" "+field1.SearcherLeftTop(1)[1]);//SearchLeftTop

            Console.WriteLine("\nControls: \nZ - /, X - \\, C - clear,\nR - to quit to main menu.\nUse arrows to move cursor.\nPress ESC to exit.");
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
