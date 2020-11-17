using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class ChessMatch
    {
        public Board MyBoard { get; set; }
        public int CurrentTurn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool MatchEnded { get; set; }

        public ChessMatch()
        {
            this.MyBoard = new Board(8, 8);
            this.CurrentTurn = 1;
            this.CurrentPlayer = Color.Green;
            PlaceInBoard();

        }

        public void DoMovement(Position origin, Position destiny)
        {
            Piece p = MyBoard.removePiece(origin);
            p.increaseMovement();
            Piece CapturedPiece = MyBoard.removePiece(destiny);
            MyBoard.placePiece(p, destiny);

        }

        public void makeMove(Position origin, Position destiny)
        {
            this.DoMovement(origin, destiny);
            CurrentTurn++;
            changePlayer();

        }

        public void originPositionValidation(Position pos)
        {
            if (MyBoard.piece(pos) == null)
            {
                throw new boardException("Não existe peça na posição de origem!");
            }
            if (CurrentPlayer != MyBoard.piece(pos).Color)
            {
                throw new boardException("Você escolheu uma peça do adversário!");
            }
            if (!MyBoard.piece(pos).therePossibleMovements())
            {
                throw new boardException("Não há movimentos possíveis para essa peça!");
            }

        }

        public void destinyPositionValidation(Position origin, Position destiny)
        {
            if (!MyBoard.piece(origin).canMoveTo(destiny))
            {
                throw new boardException("Posição de destino inválida!");
            }
        }

        private void changePlayer()
        {
            if (CurrentPlayer == Color.Green)
            {
                CurrentPlayer = Color.Red;
            }
            else
            {
                CurrentPlayer = Color.Green;
            }
        }

        public void PlaceInBoard()
        {
            MyBoard.placePiece(new Tower(Color.Green, this.MyBoard), new ChessPosition('c', 1).toPosition());
            MyBoard.placePiece(new Tower(Color.Green, this.MyBoard), new ChessPosition('b', 1).toPosition());
            MyBoard.placePiece(new Tower(Color.Green, this.MyBoard), new ChessPosition('d', 1).toPosition());
            MyBoard.placePiece(new Tower(Color.Green, this.MyBoard), new ChessPosition('c', 2).toPosition());
            MyBoard.placePiece(new Tower(Color.Red, this.MyBoard), new ChessPosition('c', 7).toPosition());
        }
    }
}
