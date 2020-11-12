using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
    {
        static int GetInt()
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x))
                    Console.Write("Incorrect entry (numeric value is required).\nTry again: ");
                else return x;
        }
        
        static int CheckExeptionByInput()
        {
            int result = 0;
            bool exceptionCatched;
            do
            {
                exceptionCatched = false;
                try
                {
                    result = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    exceptionCatched = true;
                    string message = ex.Message;
                    Console.WriteLine("Exception occured: " + message);
                    Console.Write("Try again: ");
                }

            } while (exceptionCatched);
            return result;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ahoy! This program counts odd positive numbers.");
            Console.WriteLine("Enter the numbers one by one, enter 0 to finish: ");

            int AmountOfOddNumbers = 0;
            while (true)
            {
                int number = CheckExeptionByInput();
                if (number == 0)
                {
                    break;
                }
                else if (number > 0 && number % 2 == 1)
                {
                    AmountOfOddNumbers++;
                }
            }

            Console.WriteLine(Environment.NewLine + "Amount of odd positive numbers: " + AmountOfOddNumbers);

            Console.ReadKey();
        }

}