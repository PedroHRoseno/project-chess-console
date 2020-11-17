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
                try { 
                    Console.Clear();
                    Screen.printBoard(match.MyBoard);

                    Console.WriteLine();
                    Console.WriteLine("Turno: " + match.CurrentTurn);
                    Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.ReadChessPosition().toPosition();
                    match.originPositionValidation(origin);
                    Console.WriteLine(match.MyBoard.piece(origin));
                    bool[,] possiblePositions = match.MyBoard.piece(origin).possibleMovements();

                    Console.Clear();

                    Screen.printBoard(match.MyBoard, possiblePositions);
                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destiny = Screen.ReadChessPosition().toPosition();
                    match.destinyPositionValidation(origin, destiny);
                    match.makeMove(origin, destiny);
                }
                catch (boardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadLine();
                }
            }
        }
    }
}
