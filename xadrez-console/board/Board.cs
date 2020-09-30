using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        private Piece[,] pieces;

        public Board(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.pieces = new Piece[row, column];
        }
        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }
    }

}
