using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame
{
    class GameField
    {
        public int X;
        public int Y;
        public GameElement[,] Table;
        public Ball ball1 = new Ball(2);
        public int Orbs = 0;//collected orbs count
        public Selector selector;

        public GameField(int x, int y)
        {
            X = x;
            Y = y;
            Table = new GameElement[X,Y];
        }

        public void CreateField()
        {
            //Creating walls
            for(int i = 0; i < X; i++)
            {
                Table[i, 0] = new GameElement(0);// 0 - #, 1 - @, 2 - •, 3 - /, 4 - \, 5 - ₴, 6 - +, 7 - ;8 - ■
                Table[i, Y - 1] = new GameElement(0);
                Table[i, 0].SetCoordinates(i, 0);
                Table[i, 0].SetCoordinates(i, Y - 1);
            }
            for (int i = 0;i < Y; i++)
            {
                Table[0, i] = new GameElement(0);
                Table[X - 1, i] = new GameElement(0);
                Table[0, i].SetCoordinates(0, i);
                Table[X - 1, i].SetCoordinates(X - 1, i);
            }

            //fill randomly with objects
            Random random = new Random();
            int tempint = 0;
            for (int i = 1; i < X - 1; i++)
            {
                for (int x = 1; x < Y - 1; x++)
                {
                    if (i > 2 || x > 2)
                    {
                        tempint = random.Next(1, 10);
                        if (tempint < 6)
                        {
                            Table[i, x] = new GameElement(7);
                        }
                        else if (tempint > 5 && tempint < 8)
                        {
                            Table[i, x] = new GameElement(1);
                        }
                        else if (tempint > 7)
                        {
                            Table[i, x] = new GameElement(0);
                        }
                    }
                    else
                    {
                        Table[i, x] = new GameElement(7);
                    }
                    Table[i, x].SetCoordinates(i, x);
                }
            }

            //Setting up ball
            Table[1, 1] = ball1;
            ball1.SetCoordinates(1, 1);

            //Setting up layer table for selector
            selector = new Selector(8, this);
            selector.SetCoordinates(0, 0);
        }

        public int CountAll(int t)
        {
            int counter = 0;
                    for(int i = 0; i < X; i++)
                    {
                        for(int x = 0; x < Y; x++)
                        {
                            if(Table[i,x].type==new GameElement(t).type)
                            {
                                counter++;
                            }
                        }
                    }
            return counter;
        }

        public int CountStroka(int t,int stroka)
        {
            int counter = 0;
            for (int i = 0; i < X; i++)
            {
                    if (Table[i, stroka].type == new GameElement(t).type)
                    {
                        counter++;
                    }
            }
            return counter;
        }

        public int CountStolbec(int t, int stolbec)
        {
            int counter = 0;
            for (int i = 0; i < Y; i++)
            {
                if (Table[stolbec, i].type == new GameElement(t).type)
                {
                    counter++;
                }
            }
            return counter;
        }

        public int countdiagonal(int t, int diagtype)
        {
            int counter = 0;
            for (int i = 0; i < Y; i++)
            {
                if (Table[i, i].type == new GameElement(t).type)
                {
                    counter++;
                }
            }
            return counter;
        }

        public int CountOblast(int t, int[] dot)//int[] = {x1,y1,x2,y2}
        {
            int counter = 0;
            for (int i = dot[0]; i < dot[2]; i++)
            {
                for (int x = dot[1]; x < dot[3]; x++)
                {
                    if (Table[i, x].type == new GameElement(t).type)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public string Visualize()
        {
            //return string.Join("",Table);
            var s = "";
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                //if (i > 0) s += ',';
                //s += "\n";
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (j > 0)
                    {
                        //Console.ForegroundColor = ConsoleColor.Black;
                        s += ',';
                    }
                    if (selector.X == i && selector.Y == j)
                    {
                        //Console.ForegroundColor = ConsoleColor.Magenta;
                        s += '■';
                    }
                    else
                    {
                        //if(Table[i, j].type=='#') Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        //else if (Table[i, j].type == '/'||Table[i,j].type == '\\') Console.ForegroundColor = ConsoleColor.Green;
                        //else if (Table[i, j].type == '@') Console.ForegroundColor = ConsoleColor.Blue;
                        s += Table[i, j].type;
                    }
                }
                s += "\n";
            }
            //Console.SetCursorPosition(selector.X, selector.Y);
            return s;
        }

        public string VisualizeType(int t)
        {
            //return string.Join("",Table);
            var s = "";
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                //if (i > 0) s += ',';
                //s += "\n";
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (j > 0) s += ',';

                    if (Table[i, j].type == new GameElement(t).type)
                    {
                        s += Table[i, j].type;
                    }
                }
                s += "\n";
            }
            return s;
        }

        public void ClearField()
        {
            for(int i = 0; i < X; i++)
            {
                for(int x = 0; x < Y; x++)
                {
                    Table[i,x] = new GameElement(7);
                    Table[i, x].SetCoordinates(i, x);
                }
            }
        }

        public int[] SearcherLeftTop(int t)
        {
            for (int d = 1; d < X + Y; d++)
            {
                for(int x = d, y = 1; x > 0&&y<Y-1; x--,y++)
                {
                    if (Table[x,y].type == new GameElement(t).type) return new int[] {x ,y };
                }
            }
            return new int[] {-1, -1 };
        }
        
        public bool IsCleared()
        {
            return CountOblast(1, new int[4] { 1, 1, X - 1, Y - 1 }) == 0;
        }

        public void RandomAdd(int t,GameField field)
        {
            Random random = new Random();
            int x = random.Next(field.X);
            int y = random.Next(field.Y);
            Table[x, y] = new GameElement(t);
            Table[x, y].SetCoordinates(x, y);
        }
    }
}
