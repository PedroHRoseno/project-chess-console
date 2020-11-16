using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Row { get; set; }

        public ChessPosition(char column, int row)
        {
            this.Column = column;
            this.Row = row;
        }
        public Position toPosition()
        {
            return new Position(8 - Row, Column - 'a');
        }
        public override string ToString()
        {
            return "" + this.Column + this.Row;
        }
    }
}
