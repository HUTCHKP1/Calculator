using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class NumberTheory
    {
        // Trial division up to sqrt(n) — efficient enough for n < 10000
        // Math.Sqrt from Lab 4
        public static bool IsPrime(int num)
        {
            if (num < 2)
            {
                return false;
            }
            else if (num == 2)
            {
                return true;
            }
            else if (num % 2 == 0)
            {
                return false;
            }
            int sqrt = (int)Math.Sqrt(num);
            for (int i = 3; i <= sqrt; i += 2)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        // Xi+1 = (a * Xi + c) mod m
        // Generate steps until we cycle back to the original seed
        // Display each step in the format from the lexicon
        public static void LCG(int a, int x, int c, int m)
        {
            int seed = x;
            int step = 0;
            do
            {
                int next = (a * x + c) % m;
                Console.WriteLine($"X{step + 1} = ( {a} * {x} + {c} ) mod {m} = {next}");
                x = next;
                step++;
            } while (x != seed && step < 100);
            // step < 100 guards against infinite loop
            // if m is large and period is long
        }

        // UPC-A: 11 digits in, produces 12th
        // Odd positions (1,3,5...) × 3, even positions (2,4,6...) × 1
        // check = (10 - (sum % 10)) % 10
        public static int UPCCheckDigit(string digits)
        {
            int sum = 0;
            for (int i = 0; i < 11; i++)
            {
                int dig = digits[i] - '0';
                if (i % 2 == 0)
                {
                    sum += dig * 3; // positions 1,3,5... (0-indexed: 0,2,4...)
                }
                else
                {
                    sum += dig * 1;
                }
            }
             return (10 - (sum % 10)) % 10;
        }

        // EAN-13 / ISBN-13: 12 digits in, produces 13th
        // Odd positions × 1, even positions × 3
        // check = (10 - (sum % 10)) % 10
        public static int EANCheckDigit(string digits)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                int dig = digits[i] - '0';
                if (i % 2 == 0)
                {
                    sum += dig * 1;
                }
                else
                {
                    sum += dig * 3;
                }
            }
            return (10 - (sum % 10)) % 10;
        }

        // ISBN-10: 9 digits in, produces 10th
        // digit[0]×10, digit[1]×9 ... digit[8]×2
        // check = (11 - (sum % 11)) % 11
        // if result is 10, display 'X'
        public static string ISBN10CheckDigit(string digits)
        {
            int sum = 0;
            for(int i = 0;i < 9; i++)
            {
                sum += (digits[i] - '0') * (10 - i);
            }
            int check = (11 - (sum % 11)) % 11;
            if (check == 10)
            {
                return "X";
            }
            else
            {
                return check.ToString();
            }
        }
        public static void CheckDigit(string digits)
        {
            switch (digits.Length)
            {
                case 9:
                    string check10 = ISBN10CheckDigit(digits);
                    Console.WriteLine($"ISBN-10: {digits}{check10}");
                    break;
                case 11:
                    int checkUPC = UPCCheckDigit(digits);
                    Console.WriteLine($"UPC-A: {digits}{checkUPC}");
                    break;
                case 12:
                    if (digits.StartsWith("978") || digits.StartsWith("979"))
                    {
                        int checkISBN13 = EANCheckDigit(digits);
                        Console.WriteLine($"ISBN-13: {digits}{checkISBN13}");
                    }
                    else
                    {
                        int checkEAN = EANCheckDigit(digits);
                        Console.WriteLine($"EAN-13: {digits}{checkEAN}");
                    }
                    break;
                default:
                    Console.WriteLine("Error: provide 9 digits (ISBN-10), 11 digits (UPC-A), or 12 digits (EAN-13/ISBN-13)");
                    break;


            }
        }
    }
}
