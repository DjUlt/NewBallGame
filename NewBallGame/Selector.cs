using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame
{
    class Selector: GameElement//8 - ■
    {
        protected int MaxX;
        protected int MaxY;

        public Selector(int T,GameField field1): base(T)
        {
            MaxX = field1.X;
            MaxY = field1.Y;
        }

        public void MoveRight()
        {
            if (Y + 1 < MaxY) Y++;
        }

        public void MoveLeft()
        {
            if (Y + 1 > 1) Y--;
        }

        public void MoveUp()
        {
            if (X + 1 < MaxX) X++;
        }

        public void MoveDown()
        {
            if (X + 1 > 1) X--;
        }

        public void SetS(GameField field1)//3 - /
        {
            if (field1.Table[X, Y].type == ' '|| field1.Table[X, Y].type == '\\')
            {
                field1.Table[X, Y] = new GameElement(3);
                field1.Table[X, Y].SetCoordinates(X, Y);
            }
        }

        public void SetBS(GameField field1)//4 - \
        {
            if (field1.Table[X, Y].type == ' '|| field1.Table[X, Y].type == '/')
            {
                field1.Table[X, Y] = new GameElement(4);
                field1.Table[X, Y].SetCoordinates(X, Y);
            }
        }

        public void SetC(GameField field1)//4 - \
        {
            if (field1.Table[X, Y].type == '/' || field1.Table[X, Y].type == '\\')
            {
                field1.Table[X, Y] = new GameElement(7);
                field1.Table[X, Y].SetCoordinates(X, Y);
            }
        }
    }
}
