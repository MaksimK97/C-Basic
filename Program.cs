using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework5
{
    struct Element
    {
        public string FI;
        public double avgNote;
    }
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
            Console.WriteLine("Welcome! This program checks the login correctness.");
            int AmountOfTries = 3;

            do
            {
                Console.Write("Enter login: ");
                string login = Console.ReadLine();

                if (CheckLogin(login) && CheckLoginReg(login))
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    AmountOfTries--;
                    Console.WriteLine("Wrong login. \nThe following requirements must be met:"
                        + "\nline length from 2 to 10 symbols;"
                        + "\nonly latin letters and numbers;"
                        + "\nnumber cannot be first."
                        + Environment.NewLine + "Amount of tries left: " + AmountOfTries + RightTryWord(AmountOfTries));
                }

            } while (AmountOfTries > 0);

            Console.WriteLine("Login correct!");

            Console.ReadKey();
        }

        private static void Task2()
        {
            Console.WriteLine("Welcome. This programm demonstrates the abilities of the static class Message.");

            Console.WriteLine("\nWe have the following text: \n" + Message.text);

            Console.WriteLine("\nWe will show the words of the text, which have no more than 5 letters:");
            Message.GetWordsByLength(5);

            Console.WriteLine();
            Console.Write("\nWill delete the words from the text, which end with 'a': ");
            Message.DeleteWordByEndChar('a');

            Console.WriteLine();
            Console.WriteLine("\nThe biggest word in the text is: " + Message.FindMaxLengthWord());


            Console.WriteLine("\nStringBuilder line is the line of the biggest words: \n" + Message.GetLongWordsString());

            Console.WriteLine("\nFrequency analysis of the text: ");
            string[] arr = { "I", "mercy", "swift", "thousands", "will" };
            Message.FrequencyAnalysis(arr, Message.text);

            Console.ReadKey();
        }

        private static void Task3()
        {
            string first = "overpowered";
            string second = "ovporederwe";

            Console.WriteLine("Welcome! This program checks if one line is randomized of the other line");

            Console.WriteLine("Checking the following lines: " + first + ", and " + second);

            if (isThisPermutation(first, second) == true && isThisPermutation2(first, second) == true)
                Console.WriteLine("Lines contain the same symbols, so one line is randomized of the other line.");
            else
                Console.WriteLine("Lines contain different symbols.");

            Console.ReadKey();
        }

        private static void Task4()
        {
            int amountOfWorstPupils = 3;
            StreamReader sr = new StreamReader("..\\..\\data.txt", encoding: System.Text.Encoding.GetEncoding(1251));
            int N = int.Parse(sr.ReadLine());
            Element[] a = new Element[N];
            for (int i = 0; i < N; i++)
            {
                string[] s = sr.ReadLine().Split(' ');
                a[i].FI = s[0] + " " + s[1];
                a[i].avgNote = (double.Parse(s[2]) + double.Parse(s[3]) + double.Parse(s[4])) / 3;
            }
            sr.Close();

            SortPupils(ref a);

            String result = String.Format("{0,-20} {1,-10}\n\n", "Student", "Average Score");

            Element prev = a[0];

            for (int i = 0; i < amountOfWorstPupils; i++)
            {
                if (i > 0)
                {
                    if (prev.avgNote == a[i].avgNote)
                        amountOfWorstPupils++;
                    prev = a[i];
                }

                result += String.Format("{0,-20} {1,-10:F2}\n",
                                       a[i].FI, a[i].avgNote);

            }

            Console.WriteLine("Welcome. This program shows 3 students with the worst average score.");

            Console.WriteLine($"\n{result}");

            Console.ReadKey();
        
        }
    

        static string RightTryWord(int x)
        {
            string s = "";
            if (x % 10 == 1 && x != 11) s += " try";
            else
                if ((x >= 2 && x <= 4) || (x >= 22 && x <= 24) || (x >= 32 && x <= 34) || (x > 41 && x < 45)) s += " tries";
            else
                    if ((x == 11) || (x >= 5 && x <= 20) || (x >= 25 && x <= 30) || (x >= 35 && x < 41) || (x > 44 && x < 51)) s += " tries";
            return s;
        }
        static bool CheckLogin(string login)
        {
            int length = login.Length;
            if (length >= 2 && length <= 10)
            {
                bool check = true;
                char letter = login[0];
                if (Char.IsDigit(letter))
                    return false;
                for (int i = 1; i < length; i++)
                {
                    letter = login[i];
                    if (!(Char.IsDigit(letter) || IsLatinLetter(letter)))
                    {
                        check = false;
                        break;
                    }
                }
                if (check)
                    return true;
            }
            return false;
        }

        static bool CheckLoginReg(string login)
        {
            char letter = login[0];
            if (Char.IsDigit(letter))
                return false;
            if (!Regex.IsMatch(login, @"^[a-zA-Z0-9]+${2,10}"))
                return false;
            return true;
        }

        private static bool IsLatinLetter(char letter)
        {
            int num = letter;
            if ((num >= 65 && num <= 90) || (num >= 97 && num <= 122))
                return true;
            else
                return false;
        }
        static bool isThisPermutation(string s1, string s2)
        {
            return s1.Select(Char.ToUpper).OrderBy(x => x).SequenceEqual(s2.Select(Char.ToUpper).OrderBy(x => x));
        }

        static bool isThisPermutation2(string s1, string s2)
        {
            s1 = s1.ToLower();
            s2 = s2.ToLower();

            string news1 = s1[0].ToString();
            string news2 = s2[0].ToString();

            for (int i = 1; i < s1.Length; i++)
                putCharIntoStr(ref news1, s1[i]);

            for (int i = 1; i < s2.Length; i++)
                putCharIntoStr(ref news2, s2[i]);

            if (news1.Equals(news2))
                return true;
            else
                return false;
        }

        static void putCharIntoStr(ref string s, char ch)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] > ch)
                {
                    s = s.Insert(i, ch.ToString());
                    break;
                }
                else
                    if (i == s.Length - 1)
                {
                    s += ch.ToString();
                    break;
                }
            }

        }

        static void SortPupils(ref Element[] pupils)
        {
            for (int i = 0; i < pupils.Length; i++)
            {
                for (int j = 0; j < pupils.Length - i - 1; j++)
                {
                    if (pupils[j].avgNote > pupils[j + 1].avgNote)
                    {
                        Element tmp = pupils[j + 1];
                        pupils[j + 1] = pupils[j];
                        pupils[j] = tmp;
                    }
                }
            }
        }
    }
}
