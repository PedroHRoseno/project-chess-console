using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace xadrez_console
{
    class Screen
    {
        public static void printBoard(Board board)
        {
            for (int i=0; i < board.Row; i++)
            {
                for (int j=0; j < board.Column; j++)
                {
                    if (board.piece(i, j) == null) {
                        Console.Write("- ");
                    }
                    else
                    {
                        
                        Console.Write(board.piece(i, j) + " ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}
