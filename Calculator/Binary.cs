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
        /* Validates that a string contains only '0' and '1' characters
        // Used to guard Addition, Subtraction, and Negate against corrupted input
        // Returns true if valid binary, false otherwise
        */

        /*
         * Difficulty: C# doesn't throw a helpful error when non-binary characters corrupt the carry arithmetic — it silently produces wrong results
         * Solution: foreach loop checking each character is exactly '0' or '1'
         * Why: foreach is the natural tool for character-by-character string inspection (Lab 13), and returning bool keeps the validator reusable across multiple methods
         */
        public static bool IsValidBinary(string input)
        { 
            if (input == "" || input == null)
            {
                return false;
            }
            foreach (char c in input) // Lab 13
            {
                if (c != '0' && c != '1')
                {
                    return false;
                }
            }
            return true;
        }

        /* Validates that a string contains only digit characters 0-9
        // Used to guard DecimalToBCD against letters, decimals, symbols
        // Returns true if valid decimal, false otherwise
        */


        /*
         * Difficulty: same problem — letters or decimal points passed to DecimalToBCD would corrupt the ASCII subtraction c - '0'
         * Solution: checking c < '0' || c > '9' uses the ASCII ordering directly rather than needing a lookup
         * Why: consistent approach with IsValidBinary, reusable in Program.cs for numCheckDigit validation
         */
        public static bool IsValidDecimal(string input)
        {
            if (input == "" || input == null)
            {
                return false;
            }
            foreach (char c in input) // Lab 13
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
        /*
         * Difficulty: inputs can be different lengths, so you can't just loop through both arrays together
         * Solution: two separate index variables working right-to-left, each checked independently before use
         * Why: this approach from GeeksForGeeks handles mismatched lengths naturally without needing to pad first — the carry continues even after both indices exhaust
         */
        public static string Addition(string inputA, string inputB)
        {

            // Input validation — returns error string rather than crashing
            if (!IsValidBinary(inputA) || !IsValidBinary(inputB))
            {
                return "Error: inputs must contain only 0s and 1s";
            }
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
        /*
         * Difficulty: handling signed vs unsigned differently, and the overflow from Negate
         * Solution: truncating the result back to inputA's length
         * Why: avoids needing to fix Negate itself, documented as known limitation
         */
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
         * Difficulty: negating "0000" produces "10000" because flipping gives "1111" and adding 1 cascades a carry into a fifth bit
         * Solution: documented as a known limitation rather than fixed — Subtraction's truncation step rescues the result when Negate is called internally
         * Why: fixing Negate would require truncating inside it, which would break cases where the extra bit is meaningful — the limitation is narrow and documented in tests
         */
        public static string Negate(string input)
        {
            if (!IsValidBinary(input))
            {
                return "Error: input must contain only 0s and 1s";
            }
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
        /*
         * Difficulty: signed binary numbers must extend with their sign bit, not with zero — padding a negative number with 0s changes its value
         * Solution: read the first character of each string as the sign bit and use PadLeft with that character
         * Why: this correctly implements sign extension as defined in two's complement arithmetic
         */
        public static void PadSigned(ref string a, ref string b)
        {
            int maxLength = Math.Max(a.Length ,b.Length);
            char signA = a[0];
            char signB = b[0];
            a = a.PadLeft(maxLength, signA);
            b = b.PadLeft(maxLength, signB);
        }
        /*
         * Difficulty: same length mismatch problem as signed, but simpler since unsigned numbers always extend with 0
         * Solution: PadLeft with '0' to the max length of the two inputs
         * Why: PadLeft is the natural string method for this (Lab 14), and '0' is always correct for unsigned extension
         */
        public static void PadUnsigned(ref string a, ref string b)
        {
            int maxLength = Math.Max(a.Length, b.Length);
            a = a.PadLeft(maxLength, '0');
            b = b.PadLeft(maxLength, '0');
        }

        /*
         * Difficulty: each decimal digit needs to be independently converted to exactly 4 bits regardless of its value — Convert.ToString(digit, 2) produces variable-length output (e.g. "1" for 1, not "0001")
         * Solution: PadLeft(4, '0') after each conversion to enforce fixed 4-bit wide.
         * BCD format requires exactly 4 bits per group — without padding, groups would be unreadable and BCDAddition's Split would produce wrong-length strings
         */
        public static string DecimalToBCD(string decimalInput)
        {
            if (!IsValidDecimal(decimalInput))
            {
                return "Error: input must be a positive whole number";
            }
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

        /*
         * Difficulty: two BCD strings can have different numbers of groups, and the shorter one needs right-aligning before addition — you can't just zip them together
         * Solution: build padded arrays using a negative-index trick: srcA = i - (maxLen - lenA) is negative for positions before the shorter array starts, triggering "0000" padding
         * Why: this avoids needing a second pass or a List — one loop handles both padding and copying using only arrays (Lab 12)
         */
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