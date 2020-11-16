using System;
using board;
using chess;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch match = new ChessMatch();
            Screen.printBoard(match.MyBoard);
            while (!match.MatchEnded)
            {
                Console.Clear();
                Screen.printBoard(match.MyBoard);
                Console.WriteLine();
                Console.Write("Origem: ");
                Position origin = Screen.ReadChessPosition().toPosition();
                Console.Write("Destino: ");
                Position destiny = Screen.ReadChessPosition().toPosition();

                match.DoMovement(origin, destiny);
            }
        }
    }
}
