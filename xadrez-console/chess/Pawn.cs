using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch chessMatchPawn;
        public Pawn(Color color, Board Chessboard, ChessMatch chessmatchpawn) : base(color, Chessboard)
        {
            this.chessMatchPawn = chessmatchpawn;
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

                //Special move - En Passant
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Chessboard.validPosition(left) && thereEnemy(left) && Chessboard.piece(left) == chessMatchPawn.dangerEnPassant)
                    {
                        mat[left.Row - 1, left.Column] = true; 
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Chessboard.validPosition(right) && thereEnemy(right) && Chessboard.piece(right) == chessMatchPawn.dangerEnPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
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
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    if (Chessboard.validPosition(left) && thereEnemy(left) && Chessboard.piece(left) == chessMatchPawn.dangerEnPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Row, Position.Column + 1);
                    if (Chessboard.validPosition(right) && thereEnemy(right) && Chessboard.piece(right) == chessMatchPawn.dangerEnPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }

            }
            return mat;
        }
    }
}
