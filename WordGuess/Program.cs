using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGuess
{
    class program
    {
        static void Main(string[] args)
        {
            Random R = new Random();
            int randoNum = R.Next(1, 100);

            bool win = false;

            do
            {
                Console.WriteLine("guess a number between 1 and 100");
                string a = Console.ReadLine();
                int i = int.Parse(a);

                if (i > randoNum)
                {
                    Console.WriteLine("the number u enter is to high, guess lower! ");
                }
                else if (i < randoNum)
                {
                        Console.WriteLine("the number u enter is to low, guess higher! ");
                }
                else if (i == randoNum)
                {
                   Console.WriteLine("u win! ");
                }

            } while (win == false);
        }
    }
}
