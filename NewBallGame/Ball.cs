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

            if (field1.Table[X + Dx, Y + Dy].type == ' '|| field1.Table[X + Dx, Y + Dy].type == '₴')//move if free
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
            //else if (field1.Table[X + Dx, Y + Dy].type == '¤')//move if free
            //{
            //    Program.GameOver();
            //}
        }

        public bool NextTrap(GameField field1)
        {
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
