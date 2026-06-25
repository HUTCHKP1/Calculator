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

        /*
         * Difficulty: naively checking all divisors up to n is too slow even for n < 10000 — and handling edge cases (0, 1, 2, even numbers) correctly before the loop matters
         * Solution: eliminate 0, 1, and even numbers as special cases first, then trial divide only by odd numbers up to sqrt(n)
         * Why: if n has a factor larger than sqrt(n) it must also have one smaller — so stopping at sqrt is mathematically complete. Skipping even divisors halves the loop iterations
         */
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

        /*
         * Difficulty: the sequence can be long or potentially non-terminating if m is large and the period doesn't return to seed quickly
         * Solution: do-while loop that stops when x returns to seed, with a hard cap of 100 steps as a safety guard
         * Why: do-while is the right loop here (Lab 9) because the first iteration must always execute. The 100-step cap prevents infinite loops for degenerate inputs
         */
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

        /*
         * Difficulty: the alternating ×3/×1 weighting depends on barcode position (1-indexed odd/even), but the array is 0-indexed — easy to get the mapping backwards
         * Solution: i % 2 == 0 maps to odd barcode positions (1, 3, 5...) since 0-indexed position 0 is barcode position 1
         * Why: verified by tracing: position 0 → barcode pos 1 (odd → ×3), position 1 → barcode pos 2 (even → ×1). The modulo check on the array index correctly tracks barcode position parity
         */
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

        /*
         * Difficulty: EAN-13 uses the same alternating formula as UPC but with the weights swapped — odd positions ×1, even ×3 — easy to accidentally copy the UPC weights
         * Solution: same structure as UPC but i % 2 == 0 → ×1, i % 2 != 0 → ×3
         * Why: EAN and UPC share the formula structure but differ in which position gets ×3 — keeping them as separate methods makes each independently testable and the difference explicit
         */
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

        /*
         * Difficulty: ISBN-10 uses a completely different formula to EAN/UPC — descending weights from 10 down to 2, modulo 11 not 10, and a check digit of 10 is represented as 'X'
         * Solution: (10 - i) as the weight for position i, modulo 11, with an explicit check for result == 10\
         * Why: the modulo 11 system is used because 11 is prime, which guarantees the check digit catches all single-digit errors and all adjacent transpositions — a stronger guarantee than modulo 10
         */
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
        /*
         * Difficulty: the user provides a raw digit string and the program must determine which barcode type it is without being told
         * Solution: route by string length — 9 digits → ISBN-10, 11 → UPC-A, 12 → EAN-13 or ISBN-13 (distinguished by whether it starts with 978 or 979)
         * Why: these barcode standards have fixed lengths by definition, making length a reliable discriminator. The 978/979 prefix check for ISBN-13 is the actual ISBN standard — those prefixes are reserved for book barcodes
         */
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
