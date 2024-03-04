using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Ship
    {
        public int[,] shipParts = new int[10,10];
        public int length;

        public bool Sunk()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (shipParts[i,j] == 1)
                    {
                        Console.WriteLine("ZNALAZLEM: "+i + " " + j);
                        Console.ReadLine();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
