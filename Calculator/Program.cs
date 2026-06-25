using System.ComponentModel.Design;
using System.Linq;

namespace Calculator
{
    public class Program
    {
        public static string DivideByZero = "Undefined: Cannot divide by zero";
        public static string FactorOfZero = "Undefined: Cannot Factorise by zero";
        static void Main(string[] args)
        {
            bool running;
            running = true;
            while (running)
            {
                string input;
                string[] choice;
                string[] complex = 
                { "BIN", "GEO", "VECT", "ENCRYPT",
                  "mat", "addMat", "dotMat", "scalMat", 
                  "detMat", "invMat", "numPrime", "numCheckDigit", "numRand" 
                };

                double num1 = 0, num2 = 0;
                char[] operators = { '+', '-', '*', '/', '%', '!' };
                input = Console.ReadLine() ?? "";

                choice = input.Split(' ');

                // Exit
                if (choice.Length > 0 && choice[0] == "ESC")
                {
                    Console.WriteLine("Exiting...");
                    running = false;
                    continue;
                }
                //Help
                if (choice.Contains("HELP"))
                {
                    Console.WriteLine("=== CALCULATOR HELP ============================");
                    Console.WriteLine("BASIC:        [num][op][num]  e.g. 5+3, 10/2, 5!");
                    Console.WriteLine("BINARY:       BIN ADDS/ADDU/SUBS/SUBU [a] [b]");
                    Console.WriteLine("              BIN CONVERT BIN/HEX/DEC [value]");
                    Console.WriteLine("              BIN BCD [decimal]");
                    Console.WriteLine("              BIN BCDA [decimal] [decimal]");
                    Console.WriteLine("MATRICES:     mat [name] [a] [b] [c] [d]");
                    Console.WriteLine("              addMat/dotMat [a] [b]");
                    Console.WriteLine("              scalMat [scalar] [matrix]");
                    Console.WriteLine("              detMat/invMat [matrix]");
                    Console.WriteLine("NUMBER THEORY:numPrime [n]");
                    Console.WriteLine("              numCheckDigit [digits]");
                    Console.WriteLine("              numRand [a] [X] [c] [m]");
                    Console.WriteLine("OTHER:        HELP, CLR, ESC");
                    Console.WriteLine("================================================");
                }
                //Clear
                else if (choice[0] == "CLR")
                {
                    Console.Clear();
                }
                else if (complex.Contains(choice[0])) // https://www.geeksforgeeks.org/c-sharp/c-sharp-string-contains-method/, https://www.delftstack.com/howto/csharp/check-for-an-element-inside-an-array-in-csharp/
                {
                    switch (choice[0])
                    {
                        case "BIN":
                            string[] binaryMethods = { "ADDS", "ADDU", "SUBS", "SUBU", "CONVERT", "BCD", "BCDA" };
                            //if (choice.Length != 4)
                            //{
                            //    Console.WriteLine("Invalid input for binary operation. Please provide an operation and binary number(s).");
                            //    break;
                            //}
                            if (choice.Length < 2)
                            {
                                Console.WriteLine("Usage: BIN [OPERATION] ...");
                                break;
                            }

                            if (!binaryMethods.Contains(choice[1]))
                            {
                                Console.WriteLine("Invalid binary operation. Supported operations are: ADDS, ADDU, SUBU, BCD, BCDA and CONVERT");
                                break;
                            }
                            //{
                            //    Console.WriteLine("Invalid binary numbers. Please provide valid binary numbers consisting of only 0s and 1s.");
                            //    break;
                            //}

                            switch (choice[1])
                            {
                                case "ADDS":
                                    string a = choice[2];
                                    string b = choice[3];
                                    Binary.PadSigned(ref a, ref b);
                                    Binary.Addition(a, b);
                                    Console.WriteLine(Binary.ans);
                                    break;

                                case "ADDU":
                                    Binary.Addition(choice[2], choice[3]);
                                    Console.WriteLine(Binary.ans);
                                    break;
                                case "SUBS":
                                    Binary.Subtraction(choice[2], choice[3], true);
                                    Console.WriteLine(Binary.ans);
                                    break;
                                case "SUBU":
                                    Binary.Subtraction(choice[2], choice[3], false);
                                    Console.WriteLine(Binary.ans);
                                    break;
                                case "CONVERT":
                                    int decOutput;
                                    string hexOutput, binOutput;
                                    switch (choice[2])
                                    {
                                        case "BIN":
                                            decOutput = Convert.ToInt32(choice[3], 2);
                                            hexOutput = Convert.ToString(decOutput, 16).ToUpper();
                                            Console.WriteLine($"DECIMAL = {decOutput}\nHEX = {hexOutput}");
                                            break;
                                        case "HEX":
                                            decOutput = Convert.ToInt32(choice[3], 16); // Base 16 (Decimal)
                                            binOutput = Convert.ToString(decOutput, 2); // Base 2 (Hexidecimal)
                                            Console.WriteLine($"DECIMAL = {decOutput}\nBINARY = {binOutput}");
                                            break;
                                        case "DEC":
                                            decOutput = int.Parse(choice[3]); // Just parse, already decimal
                                            binOutput = Convert.ToString(decOutput, 2); // Base 2 (Hexidecimal)
                                            hexOutput = Convert.ToString(decOutput, 16).ToUpper();
                                            Console.WriteLine($"HEX = {hexOutput}\nBINARY = {binOutput}");
                                            break;
                                    }
                                    break;
                                case "BCD":
                                    if (choice.Length != 3)
                                    {
                                        Console.WriteLine("Usage: BIN BCD [decimal number]");
                                        break;
                                    }
                                    Console.WriteLine(Binary.DecimalToBCD(choice[2]));
                                    break;
                                case "BCDA":
                                    if (choice.Length != 4)
                                    {
                                        Console.WriteLine("Usage: BIN BCDA [decimal number] [decimal number]");
                                        break;
                                    }
                                    string bcdA = Binary.DecimalToBCD(choice[2]);
                                    string bcdB = Binary.DecimalToBCD(choice[3]);
                                    Console.WriteLine($"BCD A:  {bcdA}");
                                    Console.WriteLine($"BCD B:  {bcdB}");
                                    Console.WriteLine($"Result: {Binary.BCDAddition(bcdA, bcdB)}");
                                    break;
                            }
                            break;
                        case "GEO":
                            Calculate.Geometry();
                            break;
                        case "VECT":
                            Calculate.Vectors();
                            break;
                        case "ENCRYPT":
                            break;
                        case "mat":
                            // Usage: mat a 1 2 3 4
                            if (choice.Length != 6)
                            {
                                Console.WriteLine("Usage: mat [name] [a] [b] [c] [d]");
                                break;
                            }
                            Matrix.StoreMatrix(
                            choice[1],
                            Convert.ToInt32(choice[2]),
                            Convert.ToInt32(choice[3]),
                            Convert.ToInt32(choice[4]),
                            Convert.ToInt32(choice[5])
                            );
                            Console.WriteLine($"Matrix {choice[1]} stored:");
                            Matrix.DisplayMatrix(Matrix.GetMatrix(choice[1]));  // see note below
                            break;
                        case "addMat":
                            // Usage: addMat a b
                            if(choice.Length != 3)
                            {
                                Console.WriteLine("Usage: addMat [matrix1] [matrix2]");
                                break;
                            }
                            int addMXA = Matrix.FindMatrix(choice[1]);
                            int addMXB = Matrix.FindMatrix(choice[2]);
                            if (addMXA == -1 || addMXB == -1)
                            {
                                Console.WriteLine("Error: one or both matrices not found");
                                break;
                            }
                            Matrix.DisplayMatrix(Matrix.AddMatrix(
                            Matrix.GetMatrix(choice[1]),
                            Matrix.GetMatrix(choice[2])
                            ));
                            break;
                        case "scalMat":
                            // Usage: scalMat 2 a
                            if (choice.Length != 3)
                            {
                                Console.WriteLine("Usage: scalMat [scalar] [matrix]");
                                break;
                            }
                            int scalMXS = Matrix.FindMatrix(choice[2]);
                            if (scalMXS == -1)
                            {
                                Console.WriteLine("Error: matrix not found");
                                break;
                            }
                            double scalar = Convert.ToDouble(choice[1]);
                            Matrix.DisplayMatrix(Matrix.ScalarMultiply(scalar, Matrix.GetMatrix(choice[2])));
                            break;
                        case "dotMat":
                            // Usage: dotMat a b
                            if (choice.Length != 3)
                            {
                                Console.WriteLine("Usage: dotMat [matrix1] [matrix2]");
                                break;
                            }
                            int dotMXD1 = Matrix.FindMatrix(choice[1]);
                            int dotMXD2 = Matrix.FindMatrix(choice[2]);
                            if (dotMXD1 == -1 || dotMXD2 == -1)
                            {
                                Console.WriteLine("Error: one or both matrices not found");
                                break;
                            }
                            Matrix.DisplayMatrix(Matrix.DotProduct(
                                Matrix.GetMatrix(choice[1]),
                                Matrix.GetMatrix(choice[2])
                            ));
                            break;
                        case "detMat":
                            // Usage: detMat a
                            if (choice.Length != 2)
                            {
                                Console.WriteLine("Usage: detMat [matrix]");
                                break;
                            }
                            int MXDet = Matrix.FindMatrix(choice[1]);
                            if (MXDet == -1)
                            {
                                Console.WriteLine("Error: matrix not found");
                                break;
                            }
                            Console.WriteLine($"Determinant: {Matrix.Determinant(Matrix.GetMatrix(choice[1]))}");
                            break;
                        case "invMat":
                            // Usage: invMat a
                            if (choice.Length != 2)
                            {
                                Console.WriteLine("Usage: invMat [matrix]");
                                break;
                            }
                            int MATInv = Matrix.FindMatrix(choice[1]);
                            if (MATInv == -1)
                            {
                                Console.WriteLine("Error: matrix not found");
                                break;
                            }
                            MatrixData inv = Matrix.Inverse(Matrix.GetMatrix(choice[1]));
                            if (inv.name == "ERROR")
                            {
                                Console.WriteLine("Error: matrix is singular (determinant = 0), cannot invert");
                                break;
                            }
                            Matrix.DisplayMatrix(inv);
                            break;
                        case "numPrime":
                            if (choice.Length != 2)
                            {
                                Console.WriteLine("Usage: numPrime [number]");
                                break;
                            }
                            int n = Convert.ToInt32(choice[1]);
                            if (n > 10000)
                            {
                                Console.WriteLine("Error: number must be less than 10000");
                                break;
                            }
                            if (NumberTheory.IsPrime(n))
                            {
                                Console.WriteLine($"{n} is prime");
                            }
                            else
                            {
                                Console.WriteLine($"{n} is not prime");
                            }
                            break;
                        case "numCheckDigit":
                            if (choice.Length != 2)
                            {
                                Console.WriteLine("Usage: numCheckDigit [digits]");
                                break;
                            }
                            if (!Binary.IsValidDecimal(choice[1]))
                            {
                                Console.WriteLine("Error: digits must be numeric");
                                break;
                            }
                            NumberTheory.CheckDigit(choice[1]);
                            break;
                        case "numRand":
                            if (choice.Length != 5)
                            {
                                Console.WriteLine("Usage: numRand [a] [X] [c] [m]");
                                break;
                            }
                            NumberTheory.LCG(
                                Convert.ToInt32(choice[1]),   // a
                                Convert.ToInt32(choice[2]),   // X (seed)
                                Convert.ToInt32(choice[3]),   // c
                                Convert.ToInt32(choice[4])    // m
                            );
                            break;
                    }
                }
                else
                {
                    int opIndex = input.IndexOfAny(operators);
                    if (opIndex != -1)
                    {
                        string Snum1 = input.Substring(0, opIndex);
                        char op = input[opIndex];
                        string Snum2 = input.Substring(opIndex + 1);
                        num1 = Convert.ToDouble(Snum1);
                        if (op != '!')

                        {
                            num2 = Convert.ToDouble(Snum2);
                        }


                        switch (op)
                        {
                            case '+':
                                Calculate.Addition(ref num1, ref num2);
                                Console.WriteLine(Calculate.ans);
                                break;
                            case '-':
                                Calculate.Subtraction(ref num1, ref num2);
                                Console.WriteLine(Calculate.ans);
                                break;
                            case '*':
                                Calculate.Multiplication(ref num1, ref num2);
                                Console.WriteLine(Calculate.ans);
                                break;
                            case '/':
                                if (num2 == 0)
                                {
                                    Console.WriteLine(DivideByZero);
                                    break;
                                }
                                Calculate.Division(ref num1, ref num2);
                                Console.WriteLine(Calculate.ans);
                                break;
                            case '%':
                                Calculate.Modulus(ref num1, ref num2);
                                Console.WriteLine(Calculate.ans);
                                break;
                            case '!':
                                //if (num1 == 0)
                                //{
                                //    Console.WriteLine(FactorOfZero);
                                //}
                                if (num1 < 0 || num1 % 1 != 0)
                                {
                                    Console.WriteLine("Factorial requires a non-negative integer.");
                                    break;
                                }
                                Console.WriteLine(Calculate.Factorial((int)num1));
                                break;
                        }

                    }
                }
            }
        }
    }
}