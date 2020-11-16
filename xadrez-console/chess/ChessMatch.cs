using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board MyBoard { get; private set; }
        private int CurrentTurn { get; set; }
        private Color CurrentPlayer { get; set; }
        public bool MatchEnded { get; set; }

        public ChessMatch()
        {
            this.MyBoard = new Board(8, 8);
            this.CurrentTurn = 1;
            this.CurrentPlayer = Color.White;
            PlaceInBoard();

        }

        public void DoMovement(Position origin, Position destiny)
        {
            Piece p = MyBoard.removePiece(origin);
            p.increaseMovement();
            Piece CapturedPiece = MyBoard.removePiece(destiny);
            MyBoard.placePiece(p, destiny);

        }

        public void PlaceInBoard()
        {
            MyBoard.placePiece(new Tower(Color.Black, this.MyBoard), new ChessPosition('c', 1).toPosition());
        }
    }
}
