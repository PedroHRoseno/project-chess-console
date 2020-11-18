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
            else
            {
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

        public static void showMatch(ChessMatch match)
        {
            printBoard(match.MyBoard);
            Console.WriteLine();
            printCapturedPieces(match);
            Console.WriteLine();
            Console.WriteLine("Turno: " + match.CurrentTurn);
            Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Peças capturdas: ");
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Verdes: ");
            printSet(match.capturedPiecesMethod(Color.Green));
            Console.ForegroundColor = temp;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Vermelhas: ");
            printSet(match.capturedPiecesMethod(Color.Red));
            Console.ForegroundColor = temp;
            Console.WriteLine();

        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("{");
            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("}");
        }

        
    }
}
