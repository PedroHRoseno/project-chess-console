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

        public bool Check { get; private set; }

        public ChessMatch()
        {
            this.MyBoard = new Board(8, 8);
            this.Check = false;
            this.CurrentTurn = 1;
            this.CurrentPlayer = Color.Green;
            this.GamePieces = new HashSet<Piece>();
            this.CapturedPieces = new HashSet<Piece>();
            PlaceInBoard();

        }

        private Color enemy(Color color)
        {
            if (color == Color.Green)
            {
                return Color.Red;
            }
            else
            {
                return Color.Green;
            }

        }

        private Piece king(Color color)
        {
            foreach(Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
                
            }
            return null;
        }

        public bool isOnCheck(Color color)
        {
            Piece k = king(color);
            if (k == null)
            {
                throw new boardException("Não tem rei da cor " + color + "no tabuleiro!");
            }
            foreach (Piece x in piecesInGame(enemy(color)))
            {
                bool[,] matrix = x.possibleMovements();
                if (matrix[k.Position.Row, k.Position.Column])
                {
                    return true;
                }

            }
            return false;
        }

        public Piece DoMovement(Position origin, Position destiny)
        {
            Piece p = MyBoard.removePiece(origin);
            p.increaseMovement();
            Piece capturedPiece = MyBoard.removePiece(destiny);
            MyBoard.placePiece(p, destiny);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            return capturedPiece;

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
            Piece capturedPiece = DoMovement(origin, destiny);

            if (isOnCheck(CurrentPlayer))
            {
                eraseMovement(origin, destiny, capturedPiece);
                throw new boardException("Você não pode se colocar em cheque!");
            }
            if (isOnCheck(enemy(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (checkTest(enemy(CurrentPlayer)))
            {
                MatchEnded = true;
            }
            else
            {
                CurrentTurn++;
                changePlayer();
            }


        }

        public void eraseMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = MyBoard.removePiece(destiny);
            p.decreaseMovement();
            if(capturedPiece != null)
            {
                MyBoard.placePiece(capturedPiece, destiny);
                CapturedPieces.Remove(capturedPiece);
            }
            MyBoard.placePiece(p, origin);
        }

        public bool checkTest(Color color)
        {
            if (!isOnCheck(color))
            {
                return false;
            }
            foreach(Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMovements();
                for (int i=0; i<MyBoard.Row;i++)
                {
                    for (int j = 0; j < MyBoard.Column; j++)
                    {
                        if(mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = DoMovement(origin, destiny);
                            bool checktested = isOnCheck(color);
                            eraseMovement(origin, destiny, capturedPiece);
                            if (!checktested)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
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
            setNewPiece('d', 1, new King(Color.Green, MyBoard));
            setNewPiece('h', 7, new Tower(Color.Green, MyBoard));
            setNewPiece('a', 8, new King(Color.Red, MyBoard));
            setNewPiece('b', 8, new Tower(Color.Red, MyBoard));

        }
    }
}
