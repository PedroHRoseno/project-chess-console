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

        private HashSet<Piece> GamePieces;
        private HashSet<Piece> CapturedPieces;

        public ChessMatch()
        {
            this.MyBoard = new Board(8, 8);
            this.CurrentTurn = 1;
            this.CurrentPlayer = Color.Green;
            this.GamePieces = new HashSet<Piece>();
            this.CapturedPieces = new HashSet<Piece>();
            PlaceInBoard();

        }

        public void DoMovement(Position origin, Position destiny)
        {
            Piece p = MyBoard.removePiece(origin);
            p.increaseMovement();
            Piece capturedPiece = MyBoard.removePiece(destiny);
            MyBoard.placePiece(p, destiny);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }

        }

        public HashSet<Piece> capturedPiecesMethod(Color color)
        {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach (Piece x in this.CapturedPieces)
            {
                if(x.Color == color)
                {
                    temp.Add(x);
                }
            }
            return temp;
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

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> temp = new HashSet<Piece>();
            foreach(Piece x in GamePieces)
            {
                if (x.Color == color)
                {
                    temp.Add(x);
                }
            }
            temp.ExceptWith(capturedPiecesMethod(color));
            return temp;
        }


        public void setNewPiece(char column, int row, Piece piece)
        {
            MyBoard.placePiece(piece, new ChessPosition(column, row).toPosition());
            GamePieces.Add(piece);
        }

        public void PlaceInBoard()
        {
            setNewPiece('c', 1, new Tower(Color.Green, MyBoard));
            setNewPiece('c', 2, new Tower(Color.Green, MyBoard));
            setNewPiece('d', 1, new Tower(Color.Green, MyBoard));
            setNewPiece('e', 1, new Tower(Color.Green, MyBoard));
            setNewPiece('c', 8, new Tower(Color.Red, MyBoard));
            setNewPiece('d', 8, new King(Color.Red, MyBoard));

        }
    }
}
