using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board Chessboard) : base(color, Chessboard)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        public bool thereEnemy(Position pos)
        {
            Piece p = Chessboard.piece(pos);
            return p != null && p.Color != Color;
        }
        
        public bool free(Position pos)
        {
            return Chessboard.piece(pos) == null;
        }
 
        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[Chessboard.Row, Chessboard.Column];
            Position pos = new Position(0, 0);

            if (Color == Color.Green)
            {
                pos.setValues(Position.Row - 1, Position.Column);
                if (Chessboard.validPosition(pos) && free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            
                pos.setValues(Position.Row - 2, Position.Column);
                if (Chessboard.validPosition(pos) && free(pos) && QtMovements == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
            
                pos.setValues(Position.Row - 1, Position.Column - 1);
                if (Chessboard.validPosition(pos) && thereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            
                pos.setValues(Position.Row - 1, Position.Column + 1);
                if (Chessboard.validPosition(pos) && thereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
            }
            else
            {
                pos.setValues(Position.Row + 1, Position.Column);
                if (Chessboard.validPosition(pos) && free(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.setValues(Position.Row + 2, Position.Column);
                if (Chessboard.validPosition(pos) && free(pos) && QtMovements == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.setValues(Position.Row + 1, Position.Column - 1);
                if (Chessboard.validPosition(pos) && thereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                pos.setValues(Position.Row + 1, Position.Column + 1);
                if (Chessboard.validPosition(pos) && thereEnemy(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

            }
            return mat;
        }
    }
}
