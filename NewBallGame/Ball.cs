using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame
{
    class Ball: GameElement//2 - •
    {
        public int Dx = 1;
        public int Dy = 0;

        public Ball(int T) : base(T)
        {

        }

        public void Move(GameField field1)
        {
            if (X+Dx>field1.X-1)
            {
                X = 0;
            }
            else if (X + Dx < 0)
            {
                X = field1.X - 1;
            }
            else if (Y+Dy>field1.Y-1)
            {
                Y = 0;
            }
            else if (Y + Dy < 0)
            {
                Y = field1.Y - 1;
            }
            else if (field1.Table[X + Dx, Y + Dy].type == ' '|| field1.Table[X + Dx, Y + Dy].type == '₴')//move if free
            {
                X += Dx;
                Y += Dy;
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '#')//move back if wall
            {
                Dx = -Dx;
                Dy = -Dy;
                Console.Beep(300,150);//beep 2
            }
            else if (field1.Table[X+Dx,Y+Dy].type== '+')
            {
                Random rand = new Random();
                while (true)
                {
                    int tempX = rand.Next(0,field1.X);
                    int tempY = rand.Next(0,field1.Y);
                    if(field1.Table[tempX,tempY].type==' ')
                    {
                        X = tempX;
                        Y = tempY;
                        break;
                    }
                }
            }
            else if (field1.Table[X + Dx, Y + Dy].type == 'æ')
            {
                linkedTP tempTP = (linkedTP)field1.Table[X + Dx, Y + Dy];
                X = tempTP.X1;
                Y = tempTP.Y1;
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '↑')//move back if wall
            {
                X += Dx;
                Y += Dy;
                Dx = -1;
                Dy = 0;
                Console.Beep(500, 200);//beep 2
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '←')//move back if wall
            {
                X += Dx;
                Y += Dy;
                Dx = 0;
                Dy = -1;
                Console.Beep(500, 200);//beep 2
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '→')//move back if wall
            {
                X += Dx;
                Y += Dy;
                Dx = 0;
                Dy = 1;
                Console.Beep(500, 200);//beep 2
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '↓')//move back if wall
            {
                X += Dx;
                Y += Dy;
                Dx = 1;
                Dy = 0;
                Console.Beep(500, 200);//beep 2
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '@')//collect if orb
            {
                    X += Dx;
                    Y += Dy;
                    field1.Orbs++;
                    field1.Table[X, Y] = new GameElement(7);
                    field1.Table[X, Y].SetCoordinates(X, Y);
                Console.Beep(600, 100);
                //beep wow
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '/')//bounce if /
            {
                if (Dx != 0)
                {
                    X += Dx;

                    if (Dx > 0)
                    {
                        Dy = -1;
                    }
                    else
                    {
                        Dy = 1;
                    }
                        Dx = 0;
                }
                else
                {
                    Y += Dy;
                    if (Dy > 0)
                    {
                        Dx = -1;
                    }
                    else
                    {
                        Dx = 1;
                    }
                    Dy = 0;
                }

                Console.Beep(300, 150);//beep like wall
            }
            else if(field1.Table[X + Dx, Y + Dy].type == '\\')// bounce if \
            {
            if (Dx != 0)
            {
                X += Dx;
                if (Dx > 0)
                {
                    Dy = 1;
                }
                else
                {
                    Dy = -1;
                }
                Dx = 0;
            }
            else
            {
                Y += Dy;
                if (Dy > 0)
                {
                    Dx = 1;
                }
                else
                {
                    Dx = -1;
                }
                Dy = 0;
            }
                Console.Beep(300, 150);//beep like wall
            }
            else if (field1.Table[X + Dx, Y + Dy].type == '↑')//move up if arrow
            {
                Dx = 0;
                Dy = -Dy;
            }
        }

        public bool NextTrap(GameField field1)
        {
            if (X + Dx > field1.X - 1 || X + Dx < 0 || Y + Dy > field1.Y - 1 || Y + Dy < 0) return false;
            if (field1.Table[X + Dx, Y + Dy].type == '¤')
            {
                Console.Beep(415, 500);
                Console.Beep(400, 600);
                Console.Beep(385, 500);
                Console.Beep(355, 900);
                return true;
            }
            return false;
        }
    }
}
