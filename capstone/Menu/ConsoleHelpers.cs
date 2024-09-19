using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNomApplication.Menu
{
    internal static class ConsoleHelpers
    {
        public static int GetIntegerInRange(int pMin, int pMax, string pMessage)
        {
            if (pMin > pMax)
            {
                throw new Exception($"Minimum value {pMin} cannot be greater than maximum value {pMax}");
            }

            int result;

            do
            {
                Console.WriteLine(pMessage);
                Console.WriteLine($"Please enter a number between {pMin} and {pMax} inclusive.");

                string userInput = Console.ReadLine();

                try
                {
                    result = int.Parse(userInput);
                }
                catch
                {
                    Console.WriteLine($"{userInput} is not a number");
                    continue;
                }

                if (result >= pMin && result <= pMax)
                {
                    return result;
                }
                Console.WriteLine($"{result} is not between {pMin} and {pMax} inclusive.");
            } while (true);

        }
        public static bool GetYesNoInput(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "Y" || input == "YES")
                {
                    return true;
                }
                else if (input == "N" || input == "NO")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y/YES or N/NO.");
                }
            }

        }
    }
}
