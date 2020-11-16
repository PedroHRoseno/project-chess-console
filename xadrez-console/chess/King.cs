using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class King : Piece 
    {
        public King (Color c, Board b ) : base(c, b)
        {

        }
        public override string ToString()
        {
            return "K";
        }
    }
}
