using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Binary
    {
        public static string ans = "0";
        public static string Addition(string inputA, string inputB)
        {
            // add binary numbers
            int indexA =  inputA.Length - 1;
            int indexB = inputB.Length - 1;
            int carry = 0;
            string result = "";
            while (indexA >= 0 || indexB >= 0 || carry > 0) // https://www.geeksforgeeks.org/add-two-binary-strings/
            {
                if (indexA >= 0) // if indexA is valid, add the corresponding digit to carry and move to the next digit
                {
                    carry += inputA[indexA] - '0'; // subtract '0' to convert char to int, ASCII value of '0' is 48, so '1' - '0' = 49 - 48 = 1, '0' - '0' = 48 - 48 = 0
                    indexA--;
                }
                if (indexB >= 0)
                {
                    carry += inputB[indexB] - '0';  // same as above, add the corresponding digit to carry and move to the next digit
                    indexB--;
                }
                result = (carry % 2) + result;  // add the least significant bit of carry to the result, carry % 2 gives the remainder when carry is divided by 2, which is the least significant bit of the sum of the two digits and the previous carry
                carry /= 2;
            }
            ans = result;
            return ans;
        }
        public static string Subtraction(string inputA, string inputB)
        {
            // subtract binary numbers
            // same as addition, but we need to borrow instead of carry when the digit in inputA is smaller than the corresponding digit in inputB
            // using Two's complement method for subtraction, we can convert the subtraction operation into an addition operation by taking the Two's complement of the second number and adding it to the first number
            // use pad left by length, and use math.max to ensure both binary numbers have the same length, and add 1 to the Two's complement of the second number to get the correct result
            int maxLength = Math.Max(inputA.Length, inputB.Length);
            inputA = inputA.PadLeft(maxLength, '0');
            inputB = inputB.PadLeft(maxLength, '0');
            int indexA = inputA.Length - 1;
            int indexB = inputB.Length - 1;
            int borrow = 0;
            string result = "";
            for (int i = 0; i < maxLength; i++)
            {
                // flip the bits of inputB to get the One's complement, and add 1 to get the Two's complement
                int bitA = inputA[indexA - i] - '0';
                int bitB = (inputB[indexB - i] - '0') ^ 1; // flip the bits of inputB using XOR with 1
                int sum = bitA + bitB + borrow; // add the bits of inputA, the flipped bits of inputB, and the previous borrow
                result = (sum % 2) + result; // add the least significant bit of the sum to the result
                borrow = sum / 2; // calculate the new borrow, which is the most significant bit of the sum

            }
            ans = result;
            return ans;
        }
        }
        public static string Multiplication(string inputA, string inputB)
        {
            // multiply binary numbers
            return ans;
        }
        public static string Division(string inputA, string inputB)
        {
            // divide binary numbers
            return ans;
        }
        public static string Modulus(string inputA, string inputB)
        {
            // modulus of binary numbers
            return ans;
        }

            `
    }
}
