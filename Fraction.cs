using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    class Fraction
    {
        int numerator;
        int denominator;

        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0) throw new ArgumentException("Denominator cannot be 0");
                else
                    denominator = value;
            }
        }

        public Fraction()
        {
            numerator = 1;
            denominator = 1;
        }

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.Denominator = denominator;
        }

        public Fraction Plus(Fraction x)
        {
            Fraction y = new Fraction();
            y.denominator = Fraction.NOZ(x.denominator, this.denominator);
            y.numerator = (x.numerator * (y.denominator / x.denominator)) + (this.numerator * (y.denominator / this.denominator));
            return y;
        }

        public Fraction Minus(Fraction x)
        {
            Fraction y = new Fraction();
            y.denominator = Fraction.NOZ(x.denominator, this.denominator);
            y.numerator = (this.numerator * (y.denominator / this.denominator)) - (x.numerator * (y.denominator / x.denominator));
            return y;
        }

        public Fraction Multi(Fraction x)
        {
            Fraction y = new Fraction();
            y.numerator = x.numerator * numerator;
            y.denominator = x.denominator * denominator;
            return y;
        }

        public Fraction Division(Fraction x)
        {
            Fraction y = new Fraction();
            y.numerator = x.denominator * numerator;
            y.denominator = x.numerator * denominator;
            return y;
        }

        public string ToString()
        {
            return "(" + numerator + "/" + denominator + ")";
        }

        static int NOD(int a, int b)
        {
            while (a != b)
            {
                if (a == 0)
                    break;
                if (a > b) a = a - b; else b = b - a;
            }
            return a;
        }

        static int NOZ(int a, int b)
        {
            int noz;
            int newnoz;
            int multiplier = 2;
            if (a > b)
            {
                noz = a;
                if (noz % b == 0)
                    return noz;
            }
            else
            {
                noz = b;
                if (noz % a == 0)
                    return noz;
            }
            while (true)
            {
                newnoz = noz * multiplier;
                if (newnoz % a == 0 && newnoz % b == 0)
                    return newnoz;
                multiplier++;
            }
        }

        public void Simplification()
        {
            int num = this.numerator;
            int denom = this.denominator;
            int nod = Fraction.NOD(Math.Abs(num), Math.Abs(denom));
            while (nod != 1 && nod != 0)
            {
                this.numerator = num / nod;
                this.denominator = denom / nod;
                num = this.numerator;
                denom = this.denominator;
                nod = Fraction.NOD(Math.Abs(num), Math.Abs(denom));
            }
        }
    }
}