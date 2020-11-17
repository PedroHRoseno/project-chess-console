using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class King : Piece 
    {
        public King (Color Color, Board Chessboard ) : base(Color, Chessboard)
        {

        }
        public override string ToString()
        {
            return "K";
        }


        private bool canIMove (Position pos)
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
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // NorthEast
            pos.setValues(Position.Row - 1, Position.Column + 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // East
            pos.setValues(Position.Row, Position.Column + 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // SouthEast
            pos.setValues(Position.Row + 1, Position.Column + 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // Below
            pos.setValues(Position.Row + 1 , Position.Column);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // SouthWest
            pos.setValues(Position.Row + 1, Position.Column - 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // West
            pos.setValues(Position.Row, Position.Column - 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            // Northwest
            pos.setValues(Position.Row - 1, Position.Column - 1);
            if (Chessboard.validPosition(pos) && canIMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
            }
            return possibilityMatrix;
        }
    }
}
