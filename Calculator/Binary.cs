using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Binary
    {
        public static string ans = "0";
        public static string Addition(string inputA, string inputB)
        {
            // add binary numbers
            int indexA = inputA.Length - 1;
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
        public static string Subtraction(string inputA, string inputB, bool signed)
        {
            string temp, truncatedAns, negatedB;
            if (signed)
            {
                PadSigned(ref inputA, ref inputB);
            }
            else
            {
                PadUnsigned(ref inputA, ref inputB);
            }
            negatedB = Negate(inputB);
            temp = Addition(inputA, negatedB);
            // Truncate ans to match inputA's length
            truncatedAns = temp.Substring(temp.Length - inputA.Length, inputA.Length);
            ans = truncatedAns;
            return ans;
        }
        /*
         * Concise error Negating "0000" returns "10000" 
         * because Negate flips bits to "1111", 
         * calls Addition("1111","1") 
         * which produces "10000" (an extra carry bit).
         * 
         * Need to implement a trim to remove the excess "1", 
         * and ensure the output is the same character len 
         * as the max input length.
         */
        public static string Negate(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '1')
                {
                    result = result + '0';
                }
                else if (c == '0')
                {
                    result = result + '1';
                }
            }
            string flip = Addition(result, "1");
            return flip;
        }
        public static void PadSigned(ref string a, ref string b)
        {
            int maxLength = Math.Max(a.Length ,b.Length);
            char signA = a[0];
            char signB = b[0];
            a = a.PadLeft(maxLength, signA);
            b = b.PadLeft(maxLength, signB);
        }
        public static void PadUnsigned(ref string a, ref string b)
        {
            int maxLength = Math.Max(a.Length, b.Length);
            a = a.PadLeft(maxLength, '0');
            b = b.PadLeft(maxLength, '0');
        }
        public static string DecimalToBCD(string decimalInput)
        {
            string result = "", fourBits;
            foreach (char c in decimalInput)
            {
                int digit = c - '0';
                fourBits = Convert.ToString(digit, 2);
                fourBits = fourBits.PadLeft(4, '0');
                result = result + fourBits + " ";
            }
            return result.Trim();
        }


        // https://www.scaler.com/topics/bcd-addition/
        public static string BCDAddition(string bcdA, string bcdB)
        {
            // Lab 14
            string[] groupsA = bcdA.Split(' ');
            string[] groupsB = bcdB.Split(' ');

            int lenA = groupsA.Length;
            int lenB = groupsB.Length;
            int maxLen = Math.Max(lenA, lenB); // Lab 4
            
            // Build right-aligned padded arrays of equal length
            string[] paddedA = new string[maxLen];
            string[] paddedB = new string[maxLen];

            for (int i = 0; i < maxLen; i++)
            {
                int srcA = i - (maxLen - lenA); // negative = before the start of the array
                int srcB = i - (maxLen - lenB);

                if (srcA >= 0)
                {
                    paddedA[i] = groupsA[srcA];
                }
                else
                {
                    paddedA[i] = "0000";
                }
                if (srcB >= 0)
                {
                    paddedB[i] = groupsB[srcB];
                }
                else
                {
                    paddedB[i] = "0000";
                }
            }
            // Result array — one slot bigger in case there's a final carry
            string[] resultArr = new string[maxLen + 1];

                // C# default for string[] elements is null, so initialise them (microsoft Learn)
                for (int i = 0; i <= maxLen; i++)
                {
                    resultArr[i] = "";
                }
                int carry = 0;
                // Work RIGHT TO LEFT through the groups
                for (int i = maxLen - 1; i >= 0; i--)
                {
                int sumVal = Convert.ToInt32(paddedA[i], 2)
                           + Convert.ToInt32(paddedB[i], 2)
                           + carry;
                    carry = 0;
                    if (sumVal > 9)
                    {
                        sumVal -= 10;
                        carry = 1;
                    }
                // Store in slot i+1 so slot 0 stays free for a final carry
                    resultArr[i + 1] = Convert.ToString(sumVal, 2).PadLeft(4, '0');
                }

                // If there's still a carry after all groups, put "0001" in slot 0
                if (carry == 1)
                {
                    resultArr[0] = "0001";
                }
                // Build output string with + concatenation — no String.Join needed
                string result = "";
                for (int i = 0; i <= maxLen; i++)
                {
                    if (resultArr[i] != "")
                    {
                        if (result != "")
                        {
                            result += " ";
                        }
                        result += resultArr[i];
                    }
                }
            return result;
        }

    }
}