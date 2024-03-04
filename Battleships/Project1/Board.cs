using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Board
    {
        public enum State
        {
            Empty,
            Ship,
            Hit,
            Miss,
            Sunk,
            Blocked
        }
        public State[,] Grid { get; }
        public State[,] Grid2 { get; }
        public Board()
        {
            Grid = new State[10, 10];
            Grid2 = new State[10, 10];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Grid[i, j] = State.Empty;
                }
            }
        }

        public void PlaceShip(int x, int y, int length, bool horizontal, Ship ship)
        {
            if (x < 0 || x >= 10 || y < 0 || y >= 10)
            {
                Console.WriteLine("Invalid coordinates! Try again.");
                return;
            }

            if (horizontal)
            {
                if (y + length > 10)
                {
                    Console.WriteLine("Ship placement out of bounds! Try again.");
                    return;
                }

                for (int i = y; i < y + length; i++)
                {
                    Grid[x, i] = State.Ship;
                    ship.shipParts[x, i] = 1;
                    BlockPos(x, i);

                }
            }
            else
            {
                if (x + length > 10)
                {
                    Console.WriteLine("Ship placement out of bounds! Try again.");
                    return;
                }

                for (int i = x; i < x + length; i++)
                {
                    Grid[i, y] = State.Ship;
                    ship.shipParts[i, y] = 1;
                    BlockPos(i, y);
                }
            }

            

            DisplayBoard();
        }
        public void BlockPos(int x, int y)
        {
            bool up = false, down = false, left = false, right = false;

            if (x + 1 < 10)
            {
                right = true;
                if (Grid[x + 1, y] != Board.State.Ship && Grid[x + 1, y] != Board.State.Hit)
                {
                    Grid[x + 1, y] = Board.State.Blocked;
                }
            }

            if (x - 1 >= 0)
            {
                left = true;
                if (Grid[x - 1, y] != Board.State.Ship && Grid[x - 1, y] != Board.State.Hit)
                {
                    Grid[x - 1, y] = Board.State.Blocked;
                }
            }

            if (y + 1 < 10)
            {
                down = true;
                if (Grid[x, y + 1] != Board.State.Ship && Grid[x, y + 1] != Board.State.Hit)
                {
                    Grid[x, y + 1] = Board.State.Blocked;
                }
            }

            if (y - 1 >= 0)
            {
                up = true;
                if (Grid[x, y - 1] != Board.State.Ship && Grid[x, y - 1] != Board.State.Hit)
                {
                    Grid[x, y - 1] = Board.State.Blocked;
                }
            }

            if (up && right)
            {
                if (Grid[x + 1, y - 1] != Board.State.Ship && Grid[x + 1, y - 1] != Board.State.Hit)
                {
                    Grid[x + 1, y - 1] = Board.State.Blocked;
                }
            }

            if (down && left)
            {
                if (Grid[x - 1, y + 1] != Board.State.Ship && Grid[x - 1, y + 1] != Board.State.Hit)
                {
                    Grid[x - 1, y + 1] = Board.State.Blocked;
                }
            }

            if (up && left)
            {
                if (Grid[x - 1, y - 1] != Board.State.Ship && Grid[x - 1, y - 1] != Board.State.Hit)
                {
                    Grid[x - 1, y - 1] = Board.State.Blocked;
                }
            }

            if (down && right)
            {
                if (Grid[x + 1, y + 1] != Board.State.Ship && Grid[x + 1, y + 1] != Board.State.Hit)
                {
                    Grid[x + 1, y + 1] = Board.State.Blocked;
                }
            }
        }
        public void DisplayYourShootBoard()
        {
            Console.WriteLine("Board to shoot:");
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < 10; j++)
                {
                    switch (Grid2[i, j])
                    {
                        case State.Empty:
                            Console.Write("- ");
                            break;
                        case State.Ship:
                            Console.Write("S ");
                            break;
                        case State.Hit:
                            Console.Write("X ");
                            break;
                        case State.Miss:
                            Console.Write("O ");
                            break;
                        case State.Sunk:
                            Console.Write("S ");
                            break;
                        case State.Blocked:
                            Console.Write("B ");
                            break;
                        default:
                            Console.Write("- ");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
        public void DisplayBoard()
        {
            Console.WriteLine("Board after placing ship:");
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((char)('A' + i) + " ");
                for (int j = 0; j < 10; j++)
                {
                    switch (Grid[i, j])
                    {
                        case State.Empty:
                            Console.Write("- ");
                            break;
                        case State.Ship:
                            Console.Write("S ");
                            break;
                        case State.Hit:
                            Console.Write("X ");
                            break;
                        case State.Miss:
                            Console.Write("O ");
                            break;
                        case State.Sunk:
                            Console.Write("S ");
                            break;
                        case State.Blocked:
                            Console.Write("B ");
                            break;
                        default:
                            Console.Write("- ");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
