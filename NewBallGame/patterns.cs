using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame
{
    class Pattern
    {
        public int p;

        public Pattern(int P)
        {
            p = P;
        }

        public GameElement[,] ReturnPattern()
        {
            GameElement[,] array=new GameElement[3,3];
            switch (p)
            {
                //case 0://2x2
                //    array = new GameElement[2, 2];
                //    array[0, 0] = new GameElement(1);
                //    array[1, 1] = new GameElement(1);
                //    array[1, 0] = new GameElement(5);
                //    array[0, 1] = new GameElement(5);
                //    break;
                case 1://3x3
                    array = new GameElement[3, 3];
                    array[0, 0] = new GameElement(7);
                    array[0, 1] = new GameElement(1);
                    array[0, 2] = new GameElement(5);
                    array[1, 0] = new GameElement(1);
                    array[1, 1] = new GameElement(9);
                    array[1, 2] = new GameElement(1);
                    array[2, 0] = new GameElement(5);
                    array[2, 1] = new GameElement(1);
                    array[2, 2] = new GameElement(7);
                    break;
                case 2://3x3
                    array = new GameElement[3, 3];
                    array[0, 0] = new GameElement(7);
                    array[0, 1] = new GameElement(0);
                    array[0, 2] = new GameElement(7);
                    array[1, 1] = new GameElement(7);
                    array[1, 1] = new GameElement(5);
                    array[1, 2] = new GameElement(7);
                    array[2, 0] = new GameElement(7);
                    array[2, 1] = new GameElement(0);
                    array[2, 2] = new GameElement(7);
                    break;
                //case 3://2x2
                //    array = new GameElement[2, 2];
                //    array[0, 0] = new GameElement(7);
                //    array[0, 1] = new GameElement(1);
                //    array[1, 0] = new GameElement(1);
                //    array[1, 1] = new GameElement(7);
                //    break;
            }
            return array;
        }
    }
}
