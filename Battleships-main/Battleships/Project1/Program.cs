using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Battleship Game!");
            Console.Write("Enter Player 1 name: ");
            string player1Name = Console.ReadLine();
            Console.Write("Enter Player 2 name: ");
            string player2Name = Console.ReadLine();
            if(player1Name == "" || player2Name == "")
            {
                Console.WriteLine("Player 1 and Player 2 cannot have empty names. Please try again.");
                Main(args);
                return;
            }
            if(player1Name == player2Name)
            {
                Console.WriteLine("Player 1 and Player 2 cannot have the same name. Please try again.");
                Main(args);
                return;
            }
            BattleshipGame game = new BattleshipGame(player1Name, player2Name);
            game.StartGame();
        }
    }
}
