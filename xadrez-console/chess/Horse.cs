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
            return p == null || p.Color != Color;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[Chessboard.Row, Chessboard.Column];
            Position pos = new Position(0, 0);

            //Northwest 
            pos.setValues(Position.Row - 1, Position.Column - 2);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
      
            }
            //Northeast
            pos.setValues(Position.Row - 2, Position.Column - 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row - 2, pos.Column + 1);
            }
            // Southeast
            pos.setValues(Position.Row - 1, Position.Column + 2);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            //Southwest
            pos.setValues(Position.Row + 1, Position.Column + 2);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
            pos.setValues(Position.Row + 2, Position.Column + 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
            pos.setValues(Position.Row + 2, Position.Column - 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat; 
            pos.setValues(Position.Row + 1, Position.Column - 2);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }
            return mat;
        }
    }
}
