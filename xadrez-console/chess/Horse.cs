using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Horse : Piece
    {
        public Horse(Color color, Board Chessboard) : base(color, Chessboard)
        {

        }

        public override string ToString()
        {
            return "H";
        }

        private bool canMove(Position pos)
        {
            Piece p = Chessboard.piece(pos);
            return p == null || p.Color != this.Color;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[Chessboard.Row, Chessboard.Column];
            Position pos = new Position(0, 0);

           
            pos.setValues(Position.Row - 1, Position.Column - 2);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            
            pos.setValues(Position.Row - 2, Position.Column - 1);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.setValues(Position.Row - 2, Position.Column + 1);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.setValues(Position.Row - 1, Position.Column + 2);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.setValues(Position.Row + 1, Position.Column + 2);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.setValues(Position.Row + 2, Position.Column + 1);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.setValues(Position.Row + 2, Position.Column - 1);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.setValues(Position.Row + 1, Position.Column - 2);
            if (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            return mat;
        }
    }
}
