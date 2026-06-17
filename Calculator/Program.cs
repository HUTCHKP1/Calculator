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
                
                double num1 = 0, num2 = 0;
                char[] operators = { '+', '-', '*', '/', '%', '!' };
                input = Console.ReadLine();
                
                choice = input.Split(' ');
                if (choice[0] == "HELP" || choice[1] == "HELP" || choice[2] == "HELP" || choice[3] == "HELP")
                {
                    Console.WriteLine("Binary operations: ADD, SUB, MUL, DIV, MOD. Usage: BIN [OPERATION] [BINARY NUMBER 1] [BINARY NUMBER 2]");
                }
                else if (complex.Contains(choice[0])) // https://www.geeksforgeeks.org/c-sharp/c-sharp-string-contains-method/, https://www.delftstack.com/howto/csharp/check-for-an-element-inside-an-array-in-csharp/
                {
                    switch (choice[0])
                    {
                        case "BIN":
                            string[] binaryMethods = { "ADD", "SUB", "MUL", "DIV", "MOD" };
                            char[] isBinary = { '0', '1' };
                            if (choice.Length != 4)
                            {
                                Console.WriteLine("Invalid input for binary operation. Please provide an operation and two binary numbers.");
                                break;
                            } 
                            if (!binaryMethods.Contains(choice[1]))
                            {
                                Console.WriteLine("Invalid binary operation. Supported operations are: ADD, SUB, MUL, DIV, MOD.");
                                break;
                            }
                            //{
                            //    Console.WriteLine("Invalid binary numbers. Please provide valid binary numbers consisting of only 0s and 1s.");
                            //    break;
                            //}

                            switch (choice[1])
                            {
                                case "ADD":
                                    Binary.Addition(choice[2], choice[3]);
                                    Console.WriteLine(Binary.ans);
                                break;
                        
                                case "SUB":
                                    Binary.Subtraction(choice[2], choice[3]);
                                    Console.WriteLine(Binary.ans);
                                    break;
                                case "MUL":
                                    break;
                                case "DIV":
                                    break;
                                case "MOD":
                                    break;
                            } break;
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
