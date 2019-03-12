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
        field1.Table[X, Y] = new GameElement(7);

            //if (Dx != 0)//moving up/down
            //{
                if (field1.Table[X + Dx, Y + Dy].type == ' '|| field1.Table[X + Dx, Y + Dy].type == '₴')//move if free
                {
                    X += Dx;
                    Y += Dy;
                }
                else if (field1.Table[X + Dx, Y + Dy].type == '#')//move back if wall
                {
                    Dx = -Dx;
                    Dy = -Dy;
                }
                else if (field1.Table[X + Dx, Y + Dy].type == '@')//collect if orb
                {
                    X += Dx;
                    Y += Dy;
                    field1.Orbs++;
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

                    if (field1.Table[X + Dx, Y + Dy].type == ' ')
                    {
                        Y += Dy;
                        X += Dx;
                    }//add other gameelement cases
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

                if (field1.Table[X + Dx, Y + Dy].type == ' ')
                {
                    Y += Dy;
                    X += Dx;
                }//add other gameelement cases
            }
            //}
            //else//moving left/right
            //{
                
            //}

            field1.Table[X, Y] = this;
        }
    }
}
