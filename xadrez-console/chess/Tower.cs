using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Color c, Board b) : base(c, b)
        {

        }
        public override string ToString()
        {
            return "T";
        }
    }
}