﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBallGame
{
    class GameElement
    {
        public char type;
        public int X;
        public int Y;

        public GameElement(int T)// 0 - # | 1 - @ | 2 - • | 3 - / | 4 - \ | 5 - ₴ | 6 - + | 7 - ' ' | 8 - ■ | 9 - ¤ |
        {
            switch (T)
            {
                case 0:
                    type = '#';
                    break;
                case 1:
                    type = '@';
                    break;
                case 2:
                    type = '•';
                    break;
                case 3:
                    type = '/';
                    break;
                case 4:
                    type = '\\';
                    break;
                case 5:
                    type = '₴';
                    break;
                case 6:
                    type = '+';
                    break;
                case 7:
                    type = ' ';
                    break;
                case 8:
                    type = '■';
                    break;
                case 9:
                    type = '¤';
                    break;
            }
        }

        public void SetCoordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}
