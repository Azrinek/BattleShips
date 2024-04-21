using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Player
    {
        public string Name;
        public Board OwnBoard;
        public Board TargetBoard;
        public int Wins;
        public int hits = 0;
        Ship[] ships;
        public Player(string name)
        {
            Name = name;
            OwnBoard = new Board();
            TargetBoard = new Board();
            Wins = 0;
        }
        int GetXCoordinate(bool forAttack)
        {
            Console.Write("Enter x coordinate (A-J): ");
            string xChar = Console.ReadLine().ToUpper();
            if (xChar.Length != 1 || xChar[0] < 'A' || xChar[0] > 'J')
            {
                Console.Clear();
                Console.WriteLine("Invalid input! Please enter a letter from A to J.");
                OwnBoard.DisplayBoard();
                if (forAttack)
                {
                    OwnBoard.DisplayYourShootBoard();
                }
                return GetXCoordinate(forAttack);
            }
            return xChar[0] - 'A';
        }
        public int GetYCoordinate(bool forAttack)
        {
            Console.Write("Enter y coordinate (1-10): ");
            int yCoordinate;
            bool validY = int.TryParse(Console.ReadLine(), out yCoordinate);

            if (!validY || yCoordinate < 1 || yCoordinate > 10)
            {
                Console.Clear();
                Console.WriteLine("Invalid input ! Please try again");
                OwnBoard.DisplayBoard();
                if (forAttack)
                {
                    OwnBoard.DisplayYourShootBoard();
                }
                return GetYCoordinate(forAttack);
            }
            return yCoordinate - 1;
        }
        public void PlaceShips()
        {
            ships = new Ship[10];
            OwnBoard = new Board();
            Console.WriteLine($"{Name}, place your ships on the board.");

            int[] shipLengths = {4,3,3,2,2,2,1,1,1,1};
            
            for (int i = 0; i < shipLengths.Length; i++)
            {
                
                ships[i] = new Ship();
                ships[i].length = shipLengths[i];
                ships[i].shipParts = new int[10, 10];
                OwnBoard.DisplayBoard();
                Console.WriteLine($"Placing ship of length {shipLengths[i]}");

                int x = GetXCoordinate(false);
                int y = GetYCoordinate(false);

                if (OwnBoard.Grid[x, y] == Board.State.Ship || OwnBoard.Grid[x, y] == Board.State.Blocked)
                {
                    Console.Clear();
                    Console.WriteLine("You cant place ship here");
                    i--;
                    continue;
                }

                if (shipLengths[i] == 1)
                {
                    if(!OwnBoard.CanPlaceShip(x, y, 1, true, ships[i]))
                    {
                        Console.WriteLine("You cant place ship here");
                        i--;
                        continue;
                    }
                    OwnBoard.PlaceShip(x, y, 1, true, ships[i]);
                }
                else
                {
                    Console.Write("Horizontal (true/false): ");
                    bool horizontal;
                    bool validHorizontal = bool.TryParse(Console.ReadLine(), out horizontal);

                    if (!validHorizontal)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid input for horizontal! Please enter 'true' or 'false'.");
                        i--;
                        continue;
                    }

                    if (!OwnBoard.CanPlaceShip(x, y, shipLengths[i], horizontal, ships[i]))
                    {
                        Console.WriteLine("You cant place ship here");
                        i--;
                        continue;
                    }

                    OwnBoard.PlaceShip(x, y, shipLengths[i], horizontal, ships[i]);
                    continue;
                }
                OwnBoard.BlockPos(x, y);

            }
            Console.Clear();
        }
        public bool CheckIfShipsSunk()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (OwnBoard.Grid[i, j] == Board.State.Ship)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void CheckIfShipSunk(int x, int y)
        {
            foreach (Ship ship in ships)
            {
                if(ship == null)
                {
                    continue;
                }
                if (ship.shipParts[x,y] == 1)
                {
                    ship.shipParts[x, y] = 2;
                    if(ship.Sunk())
                    {
                        Console.WriteLine("You sunk a ship!");
                    }
                }
            }
        }
        public bool TakeTurn(Player opponent)
        {
            OwnBoard.DisplayBoard();
            OwnBoard.DisplayYourShootBoard();
            Console.WriteLine($"{Name}, enter coordinates to fire:");

            int x = GetXCoordinate(true);
            int y = GetYCoordinate(true);

            Board.State targetState = opponent.OwnBoard.Grid[x, y];

            if (targetState == Board.State.Ship)
            {
                Console.WriteLine("Hit!");

                opponent.OwnBoard.Grid[x, y] = Board.State.Hit;
                OwnBoard.Grid2[x, y] = Board.State.Hit;
                CheckIfShipSunk(x,y);

                if(opponent.CheckIfShipsSunk())
                {
                    return true;
                }
                TakeTurn(opponent);

                

                return false; // Miss
            }
            else
            {
                opponent.OwnBoard.Grid[x, y] = Board.State.Miss;
                OwnBoard.Grid2[x, y] = Board.State.Miss;
                Console.WriteLine("Miss!");
                return false; // Hit
            }
        }
    }
}
