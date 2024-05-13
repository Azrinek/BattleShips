using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class BattleshipGame
    {
        private Player player1;
        private Player player2;
        private Player currentPlayer;

        public BattleshipGame(string player1Name, string player2Name)
        {
            player1 = new Player(player1Name);
            player2 = new Player(player2Name);
            currentPlayer = player1;
        }

        public void StartGame()
        {
            player1.PlaceShips();
            player1.OwnBoard.DisplayBoard();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Press any key to change player...");
            Console.ReadLine();
            Console.Clear();
            player2.PlaceShips();
            player2.OwnBoard.DisplayBoard();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Press any key to change player...");
            Console.ReadLine();
            Console.Clear();
            while (true)
            {
                Console.Clear();

                Player player = currentPlayer == player1 ? player2 : player1;

                Console.WriteLine($"{currentPlayer.Name}'s turn:");
                currentPlayer.TakeTurn(player);
                bool gameOver = player.CheckIfShipsSunk();
                if (gameOver)
                {
                    Console.WriteLine($"{currentPlayer.Name} wins!");
                    currentPlayer.Wins++;
                    Console.WriteLine($"Total wins - {player1.Name}: {player1.Wins}, {player2.Name}: {player2.Wins}");
                    Console.WriteLine("Do you want to play again? ('yes' - yes, anything else - no)");
                    string playAgain = Console.ReadLine().ToLower();
                    if (playAgain != "yes")
                    {
                        break;
                    }
                    // Reset the game for a new round
                    player1.OwnBoard.InitializeBoard();
                    player2.OwnBoard.InitializeBoard();
                    player1.PlaceShips();
                    player1.OwnBoard.DisplayBoard();
                    player2.PlaceShips();
                    player2.OwnBoard.DisplayBoard();
                    currentPlayer = player1;
                }
                currentPlayer = currentPlayer == player1 ? player2 : player1; // Update current player for the next turn
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Press any key to change player...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        
    }
}
