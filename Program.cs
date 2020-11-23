using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace Homework6
{ 

    public delegate double Fun(double a, double x);
    public delegate double Fun(double x);

    class Program
    {
        static void Main(string[] args)
        {
            Task1();
            Task2();
            Task3();
        }

        private static void Task1()
        {
        Console.WriteLine("Table of the function a*x^2:");
        Table(new Fun(MyFunc), -1.5, -2, 2);

        Console.WriteLine("Table of the function a*sin(x):");
        Table(new Fun(MyFunc), 3, -2, 2);

        Console.ReadKey();
        }

        private static void Task2()
        {
            Console.WriteLine("Welcome! This programm is designed to find min of a function!");
            List<Fun> functions = new List<Fun> { new Fun(secondDegree), new Fun(thirdDegree), new Fun(mySqrt), new Fun(Sin) };
            Console.WriteLine("Enter a function for further testing.");
            Console.WriteLine("1) f(x)=y^2");
            Console.WriteLine("2) f(x)=y^3");
            Console.WriteLine("3) f(x)=y^1/2");
            Console.WriteLine("4) f(x)=Sin(y)");
            int userChoose = GetInt(functions.Count);

            Console.WriteLine("Enter a line to find a minimum in the following format: 'х1 х2':");

            double start = 0;
            double end = 0;
            GetInterval(out start, out end);

            Console.WriteLine("Set the size of the discrediting step:");
            double step = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            SaveFunc("data.bin", start, end, step, functions[userChoose - 1]);
            double min = double.MaxValue;
            Console.WriteLine("The following function values are obtained: ");
            PrintResults(start, end, step, Load("data.bin", out min));
            Console.WriteLine("Min function value equals: {0:0.00}", min);
            Console.ReadKey();
        }

        private static void Task3()
        {
            int magistr1 = 0;
            int magistr2 = 0;
            List<Student> list = new List<Student>();                           
            DateTime dt = DateTime.Now;
            Dictionary<int, int> cousreFrequency = new Dictionary<int, int>();
            StreamReader sr = new StreamReader("..\\..\\students_6.csv");
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    list.Add(new Student(s[0], s[1], s[2], s[3], s[4], int.Parse(s[5]), int.Parse(s[6]), int.Parse(s[7]), s[8]));
                    if (int.Parse(s[6]) == 5) magistr1++; else if (int.Parse(s[6]) == 6) magistr2++;
                    if (int.Parse(s[5]) > 17 && int.Parse(s[5]) < 21)
                    {
                        if (cousreFrequency.ContainsKey(int.Parse(s[6])))
                            cousreFrequency[int.Parse(s[6])] += 1;
                        else
                            cousreFrequency.Add(int.Parse(s[6]), 1);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Error! ESC - stop the programm");
                    // Выход из Main
                    if (Console.ReadKey().Key == ConsoleKey.Escape) return;
                }
            }
            sr.Close();
            Console.WriteLine("All the students:" + list.Count);
            Console.WriteLine("Magisters of the first year:{0}", magistr1);
            Console.WriteLine("Magisters of the second year:{0}", magistr2);
            Console.WriteLine("\nThe amount of students from 18 to 20 at each studying year.");
            ICollection<int> keys = cousreFrequency.Keys;
            String result = String.Format("{0,-10} {1,-10}\n", "Year", "Number of students");
            foreach (int key in keys)
                result += String.Format("{0,-10} {1,-10:N0}\n",
                                   key, cousreFrequency[key]);
            Console.WriteLine($"\n{result}");

            list.Sort(new Comparison<Student>(AgeCompare));
            Console.WriteLine("Sort by age: ");
            foreach (var v in list) Console.WriteLine($"{v.firstName} {v.age}");

            list.Sort(new Comparison<Student>(CourceAndAgeCompare));
            Console.WriteLine("\nSort by year and age: ");
            foreach (var v in list) Console.WriteLine($"{v.firstName}, курс {v.course}, возраст {v.age}");

            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }

        public static void Table(Fun F, double a, double x, double b)
        {
            Console.WriteLine("----- A ------- X -------- Y -----");
            while (x <= b)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} | {2,8:0.000} |", a, x, F(a, x));
                x += 1;
            }
            Console.WriteLine("-----------------------------------");
        }

        public static double MyFunc(double a, double x)
        {
            return a * x * x;
        }

        public static double MySin(double a, double x)
        {
            return a * Math.Sin(x);
        }

        public static void SaveFunc(string fileName, double start, double end, double step, Fun F)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            while (start <= end)
            {
                bw.Write(F(start));
                start += step;
            }
            bw.Close();
            fs.Close();
        }

        public static double[] Load(string fileName, out double min)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double[] array = new double[fs.Length / sizeof(double)];
            min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                d = bw.ReadDouble();
                array[i] = d;
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return array;
        }

        static double secondDegree(double x)
        {
            return x * x;
        }

        static double thirdDegree(double x)
        {
            return x * x * x;
        }

        static double mySqrt(double x)
        {
            return Math.Sqrt(x);
        }

        static double Sin(double x)
        {
            return Math.Sin(x);
        }

        static int GetInt(int max)
        {
            while (true)
                if (!int.TryParse(Console.ReadLine(), out int x) || x > max)
                    Console.Write("Incorrect Entry (Numeric value from 0 to {10} is required).\nPlease try again: ", max);
                else return x;
        }

        static void GetInterval(out double start, out double end)
        {
            string[] interval = Console.ReadLine().Split(' ');
            start = double.Parse(interval[0], CultureInfo.InvariantCulture);
            end = double.Parse(interval[1], CultureInfo.InvariantCulture);
        }

        static void PrintResults(double start, double end, double step, double[] values)
        {
            Console.WriteLine("------- X ------- Y -----");
            int index = 0;
            while (start <= end)
            {
                Console.WriteLine("| {0,8:0.000} | {1,8:0.000} ", start, values[index]);
                start += step;
                index++;
            }
            Console.WriteLine("--------------------------");
        }

        static int AgeCompare(Student st1, Student st2)      
        {
            return String.Compare(st1.age.ToString(), st2.age.ToString());       
        }

        static int CourceAndAgeCompare(Student st1, Student st2)
        {
            if (st1.course > st2.course)
                return 1;
            if (st1.course < st2.course)
                return -1;
            if (st1.age > st2.age)
                return 1;
            if (st1.age < st2.age)
                return -1;
            return 0;
        }
    }
}
