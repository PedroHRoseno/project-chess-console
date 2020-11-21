using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Color color, Board Chessboard): base(color, Chessboard)
        {

        }

        public override string ToString()
        {
            return "B";
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
            pos.setValues(Position.Row - 1, Position.Column - 1);
            while(Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if(Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row - 1, pos.Column - 1);
            }
            //Northeast
            pos.setValues(Position.Row - 1, Position.Column + 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row - 1, pos.Column + 1);
            }
            // Southeast
            pos.setValues(Position.Row + 1, Position.Column + 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row + 1, pos.Column + 1);
            }
            //Southwest
            pos.setValues(Position.Row + 1, Position.Column - 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row + 1, pos.Column - 1);
            }
            return mat;
        } 
    }
}
