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
    }
}
