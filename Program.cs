using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
            Task4();
        }

        private static void Task1()
        {
            const int arrayLength = 20;
            int[] myArray = new int[arrayLength];
            Random random = new Random();
            int result;

            Console.WriteLine("Welcome, this program counts pairs of the elements, which has at least one number which divides by 3");
            Console.Write("\nin the following array [ ");
            for (int i = 0; i < arrayLength; i++)
            {
                myArray[i] = random.Next(-10000, 10001);
                Console.Write(myArray[i] + ", ");
            }
            Console.WriteLine("\b\b ]\n");

            result = GetNumberOfPairs(myArray, arrayLength);

            Console.WriteLine($"Number of pairs: {result}");

            Console.ReadKey();
        }

        private static void Task2()
        {
            Console.WriteLine("Welcome. This program demonstrates the abilities of a class to work with an array.");
            Console.Write("Enter array length: ");
            int size = GetInt();
            Console.Write("Enter the first element of an array: ");
            int firstElement = GetInt();
            Console.Write("Enter the sequance of adding the next elements: ");
            int step = GetInt();

            MyArray a = new MyArray(size, firstElement, step);

            Console.WriteLine("\nArray created: [ " + a.ToString() + " ]\n");

            Console.WriteLine("Sum of the elements of the array: " + a.Sum);

            a.Inverse = -1;

            Console.WriteLine("Array with changed signs: [ " + a.ToString() + " ]\n");

            Console.Write("Enter a number by which the elements of the array will be multiplied: ");

            a.Multi = GetInt();

            Console.WriteLine("Array multiplied by a number: [ " + a.ToString() + " ]\n");

            Console.WriteLine("Max element of an array: " + a.Max);

            Console.WriteLine("Amount of max elements of an array: " + a.MaxCount);

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\nNext the array generates.");

            string fileName = "..\\..\\array.txt";
            MyArray myArray = new MyArray(fileName);

            Console.WriteLine("The following array was created: [ " + myArray.ToString() + " ]\n");

            string fileName2 = "..\\..\\anotherArray.txt";

            Console.WriteLine("\nDownload an array from the file with the use of method.");

            myArray.loadFromFile(fileName2);

            Console.WriteLine("Downloaded the following array: [ " + myArray.ToString() + " ]\n");

            Console.WriteLine("\nSave an array in file " + fileName + " with the use of method.");

            myArray.saveIntoFile(fileName);

            MyArray myArray2 = new MyArray(fileName);

            Console.WriteLine("Will check the file content: [ " + myArray2.ToString() + " ]\n");

            Console.ReadKey();
        }

        private static void Task3()
        {
            Console.WriteLine("Welcome, this program checks login and password which are read from the file.");
            int AmountOfTries = 3;

            string[] fileName = { "data.txt", "tryData.txt", "reallyTryData.txt" };

            Account account;
            account.Login = "";
            account.Password = "";

            int i = 0;

            do
            {
                Console.WriteLine("\nLoading file: " + fileName[i]);
                account.loadFromFile(fileName[i]);

                Console.Write("Loging in: ");

                if (CheckLogAndPass(account))
                {

                    break;
                }
                else
                {
                    AmountOfTries--;
                    Console.WriteLine("Wrong login or password." +
                        Environment.NewLine + "Number of tries left: " + AmountOfTries + RightTryWord(AmountOfTries));
                }
                i++;
            } while (AmountOfTries > 0);

            Console.Write("Succes!");

            Console.ReadKey();
        }

        private static void Task4()
        {
            Console.WriteLine("Welcome. This programm demonstrates class abilities to work with the dimensional array.");

            Console.Write("Enter the number of rows of the array: ");
            int size1 = GetIntTask4();
            Console.Write("Enter the number of coloms of the array: ");
            int size2 = GetIntTask4();


            DimensionalArray a = new DimensionalArray(size1, size2);

            Console.WriteLine("\nArray created: ");

            a.PrintDinArr(a.toString());

            long sum = 0;
            a.Sum(out sum);

            Console.WriteLine("Sum of the elements of the array: " + sum);

            a.SumMoreThan(out sum, a.a[0, 0]);
            Console.WriteLine("Sum of the elements of the array, which are > than the first element: " + sum);

            Console.WriteLine("Max element of the array: " + a.Max);

            Console.WriteLine("Min element of the array: " + a.Min);

            string numOfMax = "";
            a.IndexOfMax(out numOfMax);
            Console.WriteLine("Number of the max element: " + numOfMax);

            Console.WriteLine("--------------------------------------------------");

            DimensionalArray myDimArr = new DimensionalArray();

            string fileName = "loadArray.txt";
            string fileName2 = "saveArray.txt";

            myDimArr.loadFromFile(fileName);

            Console.WriteLine("\nDownloading the array from file  " + fileName + " using method.");
            Console.WriteLine("Downloaded the following array: ");

            myDimArr.PrintDinArr(myDimArr.toString());

            Console.WriteLine("\nSave the array to file " + fileName2 + " using method.");

            myDimArr.saveIntoFile(fileName2);

            DimensionalArray anotherDimArr = new DimensionalArray(fileName2);

            Console.WriteLine("Checking file content by downloading a new array from it: ");

            anotherDimArr.PrintDinArr(anotherDimArr.toString());

            Console.ReadKey();
        }

        static int GetNumberOfPairs(int[] array, int length)
        {
            int amountOfPairs = 0;
            for (int i = 0; i < length - 1; i++)
            {
                if (array[i] % 3 == 0 || array[i + 1] % 3 == 0)
                {
                    amountOfPairs++;
                }
            }
            return amountOfPairs;
        }

        static int GetInt()
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x))
                    Console.Write("Incorrect entry (numeric value required).\nTry again: ");
                else return x;
        }

        struct Account
        {
            public string Login;
            public string Password;

            public void loadFromFile(string data)
            {
                data = "..\\..\\" + data;
                StreamReader sr = new StreamReader(data);

                Login = sr.ReadLine();

                Password = sr.ReadLine();

                sr.Close();
            }

        }

        static bool CheckLogAndPass(Account toCheck)
        {
            if (toCheck.Login == "root" && toCheck.Password == "GeekBrains")
                return true;
            else
                return false;
        }

        static string RightTryWord(int x)
        {
            string s = "";
            if (x % 10 == 1 && x != 11) s += " try";
            else
                if ((x >= 2 && x <= 4) || (x >= 22 && x <= 24) || (x >= 32 && x <= 34) || (x > 41 && x < 45)) s += " tryies";
            else
                if ((x == 11) || (x >= 5 && x <= 20) || (x >= 25 && x <= 30) || (x >= 35 && x < 41) || (x > 44 && x < 51)) s += " tryies";
            return s;
        }

        static int GetIntTask4()
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x))
                    Console.Write("Incorrect entry (numeric value required).\nTry again: ");
                else return x;
        }

    }
}
