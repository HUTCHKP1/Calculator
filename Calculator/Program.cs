using System.ComponentModel.Design;
using System.Linq;

namespace Calculator
{
    internal class Program
    {
        public static string DivideByZero = "Undefined: Cannot divide by zero";
        public static string FactorOfZero = "Undefined: Cannot Factorise by zero";
        static void Main(string[] args)
        {

            while (true)
            {
                string input;
                string[] choice;
                string[] complex = { "BIN", "GEO", "VECT", "ENCRYPT" };
                string[] conversion = { "BIN", "HEX", "DEC" };
                
                double num1 = 0, num2 = 0;
                char[] operators = { '+', '-', '*', '/', '%', '!' };
                input = Console.ReadLine()?? "";

                choice = input.Split(' ');

                if (choice.Contains("HELP"))
                {
                    Console.WriteLine("Binary operations: ADDS, ADDU, SUBS, SUBU, BCD, BCDA and CONVERT \nUsage: BIN [OPERATION] [BINARY NUMBER 1] [BINARY NUMBER 2]");
                }
                else if (complex.Contains(choice[0])) // https://www.geeksforgeeks.org/c-sharp/c-sharp-string-contains-method/, https://www.delftstack.com/howto/csharp/check-for-an-element-inside-an-array-in-csharp/
                {
                    switch (choice[0])
                    {
                        case "BIN":
                            string[] binaryMethods = { "ADDS", "ADDU", "SUBS", "SUBU", "CONVERT", "BCD", "BCDA" };
                            char[] isBinary = { '0', '1' };
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
//                        else
//                        {
//                            string type = input, temp;

//                            input = Console.ReadLine();

//                            switch (type)
//                            {
//                                case "BIN":
//                                    Calculate.Binary(ref input);
//                                    Console.WriteLine(Calculate.ans);
//                                    break;
//                            }
//                        }
//                    }
//                }    
//            }  
//        }
//    }
//}
