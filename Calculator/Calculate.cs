using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculate
    {
        public static double ans = 0;
        public static void Binary(ref string inputA, ref string inputB)
        {
          
            //char indexA =  inputA[inputA.Length - 1];
            //char indexB = inputB[inputB.Length - 1];

        }
        
        public static void Geometry()
        {

        }
        public static void Vectors()
        {

        }

        public static double Addition(ref double num1, ref double num2)
        {
            ans  = num1 + num2;
            return ans;
        }
        public static double Subtraction(ref double num1, ref double num2)
        {
            ans = (num1 - num2);
            return ans;
        }
        public static double Multiplication(ref double num1, ref double num2)
        {
            ans = (num1 * num2);
            return ans;
        }
        public static double Division(ref double num1, ref double num2)
        {
            ans = (num1 / num2);
            return ans;
        }
        public static double Modulus(ref double num1, ref double num2)
        {
            ans = (num1 % num2);
            return ans;
        }
        public static long Factorial(int num1)
        {
            long result = 1;

            for (int i = 2; i <= num1; i++)
            {
                result *= i;
            }

            return result;
        }


    }
}
