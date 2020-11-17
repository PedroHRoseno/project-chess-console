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
                    printPiece(board.piece(i, j));                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }
        public static void printBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor changedBackground = ConsoleColor.DarkYellow;

            for (int i = 0; i < board.Row; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    if (possibleMovements[i, j])
                    {
                        Console.BackgroundColor = changedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.BackgroundColor = originalBackground;
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
            if (piece == null)
            {
                Console.Write(" - ");
            }
            else { 
                if (piece.Color == Color.Green)
                {
                    ConsoleColor actualColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[");
                    Console.Write(piece);
                    Console.Write("]");
                    Console.ForegroundColor = actualColor;
                }
                else
                {
                    ConsoleColor actualColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[");
                    Console.Write(piece);
                    Console.Write("]");
                    Console.ForegroundColor = actualColor;
                }
            }
        }
    }
}
