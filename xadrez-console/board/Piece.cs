using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QtMovements { get; set; }
        public Board Chessboard { get; set; }

        public Piece (Color color, Board chessboard)
        {
            this.Position = null;
            this.Color = color;
            this.Chessboard = chessboard;
            this.QtMovements = 0;
        }
        public void increaseMovement()
        {
            QtMovements++;
        }
        public void decreaseMovement()
        {
            this.QtMovements--;
        } 

        public bool therePossibleMovements()
        {
            bool[,] matrix = possibleMovements();
            foreach (bool i in matrix) 
            {
                if (i)
                {
                    return true;
                }

            }
            return false;

        }

        public bool canMoveTo(Position pos)
        {
            return possibleMovements()[pos.Row, pos.Column];
        }

        public abstract bool[,] possibleMovements();

    }
}
