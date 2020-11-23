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

        public Piece dangerEnPassant { get; private set; }

        public ChessMatch()
        {
            this.MyBoard = new Board(8, 8);
            this.Check = false;
            this.CurrentTurn = 1;
            this.CurrentPlayer = Color.Green;
            this.GamePieces = new HashSet<Piece>();
            this.CapturedPieces = new HashSet<Piece>();
            dangerEnPassant = null;
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

            //Special movie castling
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinyTower = new Position(origin.Row, origin.Column + 1);
                Piece t = MyBoard.removePiece(originTower);
                t.increaseMovement();
                MyBoard.placePiece(t, destinyTower);
            }
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinyTower = new Position(origin.Row, origin.Column - 1);
                Piece t = MyBoard.removePiece(originTower);
                t.increaseMovement();
                MyBoard.placePiece(t, destinyTower);
            }

            //Special move - En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posP;
                    if (p.Color == Color.Green)
                    {
                        posP = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = MyBoard.removePiece(posP);
                    CapturedPieces.Add(capturedPiece);
                }
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
            Piece p1 = MyBoard.piece(destiny);
            //Special move - promotion
            if (p1 is Pawn)
            {
                if (p1.Color == Color.Green && destiny.Row == 0 || (p1.Color == Color.Red && destiny.Row == 7))
                {
                    p1 = MyBoard.removePiece(destiny);
                    GamePieces.Remove(p1);
                    Piece queen = new Queen(p1.Color, MyBoard);
                    MyBoard.placePiece(queen, destiny);
                    GamePieces.Add(queen);
                }
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

            Piece p = MyBoard.piece(destiny);
            // Special move - En passant

            if (p is Pawn && (destiny.Row == origin.Row -2 || destiny.Row == origin.Row + 2))
            {
                dangerEnPassant = p;
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

            //Special movie castling
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinyTower = new Position(origin.Row, origin.Column + 1);
                Piece t = MyBoard.removePiece(destinyTower);
                t.decreaseMovement();
                MyBoard.placePiece(t, originTower);
            }
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinyTower = new Position(origin.Row, origin.Column - 1);
                Piece t = MyBoard.removePiece(destinyTower);
                t.increaseMovement();
                MyBoard.placePiece(t, originTower);
            }
            //Special move - En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == dangerEnPassant)
                {
                    Piece pawn = MyBoard.removePiece(destiny);
                    Position posP;
                    if (p.Color == Color.Green)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    MyBoard.placePiece(pawn, posP);
                }
            }

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
            //Green
            setNewPiece('a', 1, new Tower(Color.Green, MyBoard));
            setNewPiece('b', 1, new Horse(Color.Green, MyBoard));
            setNewPiece('c', 1, new Bishop(Color.Green, MyBoard));
            setNewPiece('d', 1, new Queen(Color.Green, MyBoard));
            setNewPiece('e', 1, new King(Color.Green, MyBoard, this));
            setNewPiece('f', 1, new Bishop(Color.Green, MyBoard));
            setNewPiece('g', 1, new Horse(Color.Green, MyBoard));
            setNewPiece('h', 1, new Tower(Color.Green, MyBoard));
            setNewPiece('a', 2, new Pawn(Color.Green, MyBoard, this));
            setNewPiece('b', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('c', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('d', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('e', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('f', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('g', 2, new Pawn(Color.Green, MyBoard,this));
            setNewPiece('h', 2, new Pawn(Color.Green, MyBoard,this));

            //Red
            setNewPiece('a', 8, new Tower(Color.Red, MyBoard));
            setNewPiece('b', 8, new Horse(Color.Red, MyBoard));
            setNewPiece('c', 8, new Bishop(Color.Red, MyBoard));
            setNewPiece('d', 8, new Queen(Color.Red, MyBoard));
            setNewPiece('e', 8, new King(Color.Red, MyBoard, this));
            setNewPiece('f', 8, new Bishop(Color.Red, MyBoard));
            setNewPiece('g', 8, new Horse(Color.Red, MyBoard));
            setNewPiece('h', 8, new Tower(Color.Red, MyBoard));
            setNewPiece('a', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('b', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('c', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('d', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('e', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('f', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('g', 7, new Pawn(Color.Red, MyBoard, this));
            setNewPiece('h', 7, new Pawn(Color.Red, MyBoard, this));

        }
    }
}
