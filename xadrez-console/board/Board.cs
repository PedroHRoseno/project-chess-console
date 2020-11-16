using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        private Piece[,] pieces;

        public Board(int row, int column)
        {
            this.Row = row;
            this.Column = column;
            this.pieces = new Piece[row, column];
        }
        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }
        public Piece piece(Position pos)
        {
            return pieces[pos.Row, pos.Column];
        }

        public bool therePiece(Position pos)
        {
            validatePosition(pos);
            return piece(pos) != null;
        } 
        public void placePiece(Piece p, Position pos)
        {
            if (therePiece(pos))
            {
                throw new boardException("Already exists a piece in this position");
            }
            pieces[pos.Row, pos.Column] = p;
            p.Position = pos;
        }

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece temp = piece(pos);
            temp.Position = null;
            pieces[pos.Row, pos.Column] = null;
            return temp;

        }

        public bool validPosition(Position pos)
        {
            if (pos.Row < 0 || pos.Row >= this.Row || pos.Column < 0 || pos.Column >= this.Column)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void validatePosition(Position pos)
        {
            if (!validPosition(pos))
            {
                throw new boardException("Invalid Position !");
            }

        }


        
    }

}
