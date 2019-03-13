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
            Table = new GameElement[X, Y];
        }

        public void CreateField()
        {
            //Creating walls
            for (int i = 0; i < X; i++)
            {
                Table[i, 0] = new GameElement(0);// 0 - #, 1 - @, 2 - •, 3 - /, 4 - \, 5 - ₴, 6 - +, 7 - ;8 - ■
                Table[i, Y - 1] = new GameElement(0);
                Table[i, 0].SetCoordinates(i, 0);
                Table[i, 0].SetCoordinates(i, Y - 1);
            }
            for (int i = 0; i < Y; i++)
            {
                Table[0, i] = new GameElement(0);
                Table[X - 1, i] = new GameElement(0);
                Table[0, i].SetCoordinates(0, i);
                Table[X - 1, i].SetCoordinates(X - 1, i);
            }

            //RANDOM generator
            Random random = new Random();
            int tempint = 0;
            for (int i = 1; i < X - 1; i++)
            {
                for (int x = 1; x < Y - 1; x++)
                {
                    if (i > 2 || x > 2)
                    {
                        tempint = random.Next(1, 100);//add some ↑
                        if (tempint < 61)
                        {
                            Table[i, x] = new GameElement(7);
                        }
                        else if (tempint > 60 && tempint < 76)
                        {
                            Table[i, x] = new GameElement(0);
                        }
                        else if (tempint > 75 && tempint < 88)
                        {
                            Table[i, x] = new GameElement(1);
                        }
                        else if (tempint > 87 && tempint < 96)
                        {
                            Table[i, x] = new GameElement(5);
                        }
                        else if (tempint > 95)
                        {
                            Table[i, x] = new GameElement(9);
                        }
                    }
                    else
                    {
                        Table[i, x] = new GameElement(7);
                    }
                    Table[i, x].SetCoordinates(i, x);
                }
            }

            //PATTERN generator+fill with random if not patterned
            if (X > 5)//playzone is bigger than 3
            {
                Pattern pattern;
                GameElement[,] array;
                int c = 0;
                for (int i = X - 2 / 3; i > 0; i--)//count of pattern structs
                {
                    //for (int z = 2; z < (X - 2) * (X - 2); z++)//count of elements
                    //{
                        for (int x = (X - 2) % 3 + 1; x < X - 3; x++)
                        {
                            for (int y = (X - 2) % 3 + 1; y < X - 3; y++)
                            {
                                if (this.AreaIsClear(this,new[]{ x,y},new[]{ x + 2,y + 2}))
                                {
                                pattern = new Pattern(random.Next(1,2));//3x3 structs
                                array = pattern.ReturnPattern();
                                for (int l = x; l < x + 3; l++) 
                                {
                                    for (int m = y; m < y + 3; m++) 
                                    {
                                        Table[l, m] = array[l - x, m - y];
                                    }
                                }
                                c++;
                                if (c == i) goto Foo;
                                }
                            }
                        }
                    //}
                }
            Foo:
                Nothing();
            }
            

            //Setting up ball
            ball1.SetCoordinates(1, 1);

            //Setting up layer table for selector
            selector = new Selector(8, this);
            selector.SetCoordinates(0, 0);
        }

        public void Nothing()
        {

        }

        //public string Visualize()
        public void Visualize()
        {
            //var s = "";
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                //if (i > 0) s += ',';
                //s += "\n";
                Console.Write("\n");
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    if (j > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        //s += ',';
                        Console.Write(',');
                    }
                    if (selector.X == i && selector.Y == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        //s += '■';
                        Console.Write('■');
                    }
                    else if (ball1.X == i && ball1.Y == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        //s += '•';
                        Console.Write('•');
                    }
                    else
                    {
                        if (Table[i, j].type == '#') Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        else if (Table[i, j].type == '/' || Table[i, j].type == '\\') Console.ForegroundColor = ConsoleColor.Green;
                        else if (Table[i, j].type == '@') Console.ForegroundColor = ConsoleColor.Blue;
                        else if (Table[i, j].type == '₴') Console.ForegroundColor = ConsoleColor.Cyan;
                        else if (Table[i, j].type == '¤') Console.ForegroundColor = ConsoleColor.DarkRed;
                        //s += Table[i, j].type;
                        Console.Write(Table[i, j].type);
                    }
                }
                //s += "\n";
            }
            //return s;
        }

        public bool AreaIsClear(GameField field1,int[] a1, int[] a2)//a= x,y
        {
            for (int x = a1[0]; x < a2[0] + 1; x++) 
            {
                for (int y = a1[1]; y < a2[1] + 1; y++)
                {
                    if (field1.Table[x, y].type == new GameElement(0).type || field1.Table[x, y].type == new GameElement(5).type) return false;
                }
            }
            return true;
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
        
        public bool IsCleared()
        {
            return CountOblast(1, new int[4] { 1, 1, X - 1, Y - 1 }) == 0;
        }


        //Lab tasks ===================================================================================================
        public int CountAll(int t)
        {
            int counter = 0;
            for (int i = 0; i < X; i++)
            {
                for (int x = 0; x < Y; x++)
                {
                    if (Table[i, x].type == new GameElement(t).type)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public int CountStroka(int t, int stroka)
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

        public int countdiagonal(int t, int diagtype)//diagtype = 1 - main diag | 2 - second diag
        {
            int counter = 0;
            if (diagtype == 0)
            {
                for (int i = 0; i < Y; i++)
                {
                    if (Table[i, i].type == new GameElement(t).type)
                    {
                        counter++;
                    }
                }
            }
            else
            {
                for (int i = Y - 1, x = 0; i > -1 && x < Y; i--, x++)
                {
                    if (Table[x, i].type == new GameElement(t).type)
                    {
                        counter++;
                    }
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

        public string VisualizeType(int t, int d)//d = 0 - only this element type | 1 - exclude
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

                    if (d == 0)
                    {
                        if (Table[i, j].type == new GameElement(t).type)
                        {
                            s += Table[i, j].type;
                        }
                    }
                    else
                    {
                        if (Table[i, j].type != new GameElement(t).type)
                        {
                            s += Table[i, j].type;
                        }
                    }
                }
                s += "\n";
            }
            return s;
        }

        public GameElement[] SearcherAll(int t)
        {
            GameElement[] array = new GameElement[CountAll(t)];
            int counter = 0;
            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    if (Table[x, y].type == new GameElement(t).type)
                    {
                        array[counter] = Table[x, y];
                    }
                }
            }
            return array;
        }

        public int[] SearcherLeftTop(int t)
        {
            for (int d = 1; d < X + Y; d++)
            {
                for (int x = d, y = 1; x > 0 && y < Y - 1; x--, y++)
                {
                    if (Table[x, y].type == new GameElement(t).type) return new int[] { x, y };
                }
            }
            return new int[] { -1, -1 };
        }

        public void RandomAdd(int t,GameField field)
        {
            Random random = new Random();
            int x = random.Next(field.X);
            int y = random.Next(field.Y);
            Table[x, y] = new GameElement(t);
            Table[x, y].SetCoordinates(x, y);
        }

        public void Replacer(int t1, int t2)
        {
            GameElement[] array = SearcherAll(t1);
            foreach (GameElement e in array)
            {
                Table[e.X, e.Y] = new GameElement(t2);
                Table[e.X, e.Y].SetCoordinates(e.X, e.Y);
            }
        }

    }
}
