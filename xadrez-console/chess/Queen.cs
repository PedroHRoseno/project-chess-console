using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Color color, Board Chessboard) : base(color, Chessboard)
        {

        }

        public override string ToString()
        {
            return "Q";
        }

        private bool canMove(Position pos)
        {
            Piece p = Chessboard.piece(pos);
            return p == null || p.Color != Color;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] possibilityMatrix = new bool[Chessboard.Row, Chessboard.Column];
            Position pos = new Position(0, 0);

            // Above
            pos.setValues(Position.Row - 1, Position.Column);
            while (Chessboard.validPosition(pos) && canMove(pos))
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
            while (Chessboard.validPosition(pos) && canMove(pos))
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
            while (Chessboard.validPosition(pos) && canMove(pos))
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
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column = pos.Column - 1;
            }
            //Northwest 
            pos.setValues(Position.Row - 1, Position.Column - 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row - 1, pos.Column - 1);
            }
            //Northeast
            pos.setValues(Position.Row - 1, Position.Column + 1);
            while (Chessboard.validPosition(pos) && canMove(pos))
            {
                possibilityMatrix[pos.Row, pos.Column] = true;
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
                possibilityMatrix[pos.Row, pos.Column] = true;
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
                possibilityMatrix[pos.Row, pos.Column] = true;
                if (Chessboard.piece(pos) != null && Chessboard.piece(pos).Color != Color)
                {
                    break;
                }
                pos.setValues(pos.Row + 1, pos.Column - 1);
            }
            return possibilityMatrix;
        }
    }
}
