using System;
using System.Collections.Generic;
using System.Text;
using board;
using chess;

namespace xadrez_console
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i=0; i < board.Row; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j < board.Column; j++)
                {
                    if (board.piece(i, j) == null) {
                        Console.Write(" - ");
                    }
                    else
                    {
                        Console.Write("[");
                        printPiece(board.piece(i, j));
                        Console.Write("]");
                    }
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(column, row);
        }
        public static void printPiece(Piece piece)
        {
            if (piece.Color == Color.Black)
            {
                ConsoleColor actualColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(piece);
                Console.ForegroundColor = actualColor;
            }
            else
            {
                Console.Write(piece);
            }
        }
    }
}
