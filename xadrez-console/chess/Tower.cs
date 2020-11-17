using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Color Color, Board Chessboard) : base(Color, Chessboard)
        {

        }
        public override string ToString()
        {
            return "T";
        }
        private bool canIMove(Position pos)
        {
            Piece p = Chessboard.piece(pos);
            return p == null || p.Color != this.Color;
        }
        public override bool[,] possibleMovements()
        {
            bool[,] possibilityMatrix = new bool[Chessboard.Row, Chessboard.Column];
            Position pos = new Position(0, 0);
            // Above
            pos.setValues(Position.Row - 1, Position.Column);
            while (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Row = pos.Row - 1;
            }
            // Below
            pos.setValues(Position.Row + 1, Position.Column);
            while (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Row = pos.Row + 1;
            }
            // Right
            pos.setValues(Position.Row, Position.Column + 1);
            while (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column = pos.Column + 1;
            }
            //Left
            pos.setValues(Position.Row, Position.Column - 1);
            while (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return possibilityMatrix;
        }

    }
}