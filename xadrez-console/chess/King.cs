using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class King : Piece 
    {
        private ChessMatch ChessmatchKing;
        public King (Color Color, Board Chessboard, ChessMatch chessmatchKing ) : base(Color, Chessboard)
        {
            this.ChessmatchKing = chessmatchKing;

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

        private bool testToCastling(Position pos)
        {
            Piece p = Chessboard.piece(pos);
            return p != null && p is Tower && p.Color == Color && p.QtMovements == 0;
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

            // Special moves - Castling

            if (QtMovements == 0 && !ChessmatchKing.Check)
            {
                // Little castling
                Position towerPosition = new Position(Position.Row, Position.Column + 3);
                if (testToCastling(towerPosition))
                {
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Chessboard.piece(p1) == null && Chessboard.piece(p2) == null)
                    {
                        possibilityMatrix[Position.Row, Position.Column + 2] = true;
                    }
                }
                // Big castling
                Position towerPosition2 = new Position(Position.Row, Position.Column - 4);
                if (testToCastling(towerPosition2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Chessboard.piece(p1) == null && Chessboard.piece(p2) == null && Chessboard.piece(p2) == null)
                    {
                        possibilityMatrix[Position.Row, Position.Column - 2] = true;
                    }
                }
            }   



            return possibilityMatrix;
        }
    }
}
