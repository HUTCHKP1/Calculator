using Calculator;
namespace BinaryTesting
{
    [TestClass]
    public class BinaryTests
    {

        // ADDITION
       
        // VALIDATION — IsValidBinary
       

        [TestMethod]
        // Invalid: empty string — no bits to process, should be rejected
        public void IsValidBinary_EmptyString_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidBinary(""));
        }

        [TestMethod]
        // Invalid: contains letters — not binary digits
        public void IsValidBinary_Letters_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidBinary("abc"));
        }

        [TestMethod]
        // Invalid: contains '2' — common mistake assuming binary goes higher than 1
        public void IsValidBinary_ContainsTwo_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidBinary("102"));
        }

        [TestMethod]
        // Valid: "0" and "1" are the only valid single characters
        public void IsValidBinary_ValidInput_ReturnsTrue()
        {
            Assert.IsTrue(Binary.IsValidBinary("1010"));
        }

       
        // VALIDATION — IsValidDecimal
       

        [TestMethod]
        // Invalid: empty string
        public void IsValidDecimal_EmptyString_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidDecimal(""));
        }

        [TestMethod]
        // Invalid: decimal point — user passes "12.5" expecting BCD of 12.5
        // The method only handles whole numbers, so this must be rejected
        public void IsValidDecimal_ContainsDecimalPoint_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidDecimal("12.5"));
        }

        [TestMethod]
        // Invalid: letters mixed in — catches typos like "12a"
        public void IsValidDecimal_ContainsLetters_ReturnsFalse()
        {
            Assert.IsFalse(Binary.IsValidDecimal("12a"));
        }

        [TestMethod]
        // Valid: "0" through "9" should all be accepted
        public void IsValidDecimal_ValidInput_ReturnsTrue()
        {
            Assert.IsTrue(Binary.IsValidDecimal("1234"));
        }

       
        // INVALID INPUT — Addition with guards
       

        [TestMethod]
        // Invalid: non-binary characters passed to Addition
        // Before validation was added, this would silently corrupt the carry arithmetic
        // Now returns a clear error string instead of crashing or producing garbage
        public void Addition_NonBinaryInput_ReturnsErrorString()
        {
            Assert.AreEqual("Error: inputs must contain only 0s and 1s",
                Binary.Addition("abc", "1"));
        }

        [TestMethod]
        // Invalid: empty string — previously caused IndexOutOfRangeException
        public void Addition_EmptyInput_ReturnsErrorString()
        {
            Assert.AreEqual("Error: inputs must contain only 0s and 1s",
                Binary.Addition("", "1"));
        }

       
        // INVALID INPUT — DecimalToBCD with guards
       

        [TestMethod]
        // Invalid: decimal point in input — user misunderstands what "decimal" means here
        public void DecimalToBCD_ContainsDecimalPoint_ReturnsErrorString()
        {
            Assert.AreEqual("Error: input must be a positive whole number",
                Binary.DecimalToBCD("12.5"));
        }

        [TestMethod]
        // Invalid: empty string — previously returned empty string with no feedback
        public void DecimalToBCD_EmptyString_ReturnsErrorString()
        {
            Assert.AreEqual("Error: input must be a positive whole number",
                Binary.DecimalToBCD(""));
        }

        [TestMethod]
        // Invalid: letters — catches user passing a word instead of a number
        public void DecimalToBCD_Letters_ReturnsErrorString()
        {
            Assert.AreEqual("Error: input must be a positive whole number",
                Binary.DecimalToBCD("abc"));
        }

        [TestMethod]
        // Valid: simplest possible case — ensures the base condition (no bits, no carry) works
        public void Addition_ZeroPlusZero_ReturnsZero()
        {
            Assert.AreEqual("0", Binary.Addition("0", "0"));
        }

        [TestMethod]
        // Valid: 1+1 must produce a carry — tests that the carry mechanism fires at all
        public void Addition_OnePlusOne_ProducesCarry()
        {
            Assert.AreEqual("10", Binary.Addition("1", "1"));
        }

        [TestMethod]
        // Valid: 1010 (10) + 0101 (5) = 1111 (15) — complementary bits, no carry generated
        // Tests that bits add cleanly without carry at any position
        public void Addition_ComplementaryBits_NoCarry()
        {
            Assert.AreEqual("1111", Binary.Addition("1010", "0101"));
        }

        [TestMethod]
        // Valid: 1111 (15) + 0001 (1) = 10000 (16)
        // Tests that carry cascades through all bits and extends the result width
        public void Addition_CascadingCarry_ExtendsResult()
        {
            Assert.AreEqual("10000", Binary.Addition("1111", "0001"));
        }

        [TestMethod]
        // Valid: 101 (5) + 11 (3) = 1000 (8) — different length inputs
        // Tests that the index-based loop handles mismatched lengths without crashing
        public void Addition_DifferentLengths_HandlesCorrectly()
        {
            Assert.AreEqual("1000", Binary.Addition("101", "11"));
        }


        // SUBTRACTION — UNSIGNED


        [TestMethod]
        // Valid: 1010 (10) - 0101 (5) = 0101 (5)
        // Standard two's complement subtraction — tests the full SUBU pipeline
        public void Subtraction_Unsigned_BasicSubtraction()
        {
            Assert.AreEqual("0101", Binary.Subtraction("1010", "0101", false));
        }

        [TestMethod]
        // Valid: 1000 (8) - 0001 (1) = 0111 (7)
        // Tests that borrow chains correctly through a run of zeros
        public void Subtraction_Unsigned_BorrowChain()
        {
            Assert.AreEqual("0111", Binary.Subtraction("1000", "0001", false));
        }

        [TestMethod]
        // Edge: 0000 - 0000 = 0000
        // Also exercises the Negate("0000") known bug path —
        // the truncation step in Subtraction rescues the result even though Negate overflows
        public void Subtraction_Unsigned_ZeroMinusZero_ReturnsZero()
        {
            Assert.AreEqual("0000", Binary.Subtraction("0000", "0000", false));
        }

        [TestMethod]
        // Edge: subtracting a value from itself must always yield zero
        // Tests that two's complement of a number added to itself cancels out
        public void Subtraction_Unsigned_SameValue_ReturnsZero()
        {
            Assert.AreEqual("0000", Binary.Subtraction("1111", "1111", false));
        }


        // SUBTRACTION — SIGNED


        [TestMethod]
        // Valid: 0101 (5) - 0011 (3) = 0010 (2) — positive result
        // Tests that sign-padded subtraction produces the correct positive result
        public void Subtraction_Signed_PositiveResult()
        {
            Assert.AreEqual("0010", Binary.Subtraction("0101", "0011", true));
        }

        [TestMethod]
        // Valid: 0011 (3) - 0101 (5) = 1110 (-2 in 4-bit two's complement)
        // Tests that signed subtraction correctly produces a negative result
        public void Subtraction_Signed_NegativeResult_TwosComplement()
        {
            Assert.AreEqual("1110", Binary.Subtraction("0011", "0101", true));
        }


        // NEGATE


        [TestMethod]
        // Valid: negate 0001 (1) → 1111 (-1)
        // flip 0001 → 1110, add 1 → 1111
        public void Negate_One_GivesNegativeOne()
        {
            Assert.AreEqual("1111", Binary.Negate("0001"));
        }

        [TestMethod]
        // Valid: negate 1111 (-1) → 0001 (+1)
        // flip 1111 → 0000, add 1 → 0001
        public void Negate_NegativeOne_GivesPositiveOne()
        {
            Assert.AreEqual("0001", Binary.Negate("1111"));
        }

        [TestMethod]
        // Valid: negate 0101 (5) → 1011 (-5 in two's complement)
        // flip 0101 → 1010, add 1 → 1011
        public void Negate_Five_GivesNegativeFive()
        {
            Assert.AreEqual("1011", Binary.Negate("0101"));
        }

        [TestMethod]
        /* KNOWN BUG — intentional failure documentation
        // Negate("0000") returns "10000" instead of "0000"
        // flip 0000 → 1111, add 1 → 10000 (carry overflows into a 5th bit)
        // Negate does not truncate its result to the input width
        // Subtraction handles this safely via its own truncation step, but
        // calling Negate("0000") directly produces the wrong width
        / This test asserts the actual (buggy) output to document the known limitation
        */
        public void Negate_Zero_KnownOverflowBug_DocumentedLimitation()
        {
            Assert.AreEqual("10000", Binary.Negate("0000"));
        }


        // PADSIGNED


        [TestMethod]
        // Valid: "1" (negative) padded to match length 3 — sign bit '1' extended
        // Ensures negative sign extension produces 1s, not 0s
        public void PadSigned_NegativeSign_ExtendedWithOnes()
        {
            string a = "1";
            string b = "101";
            Binary.PadSigned(ref a, ref b);
            Assert.AreEqual("111", a);
            Assert.AreEqual("101", b);
        }

        [TestMethod]
        // Valid: "0" (positive) padded to match length 4 — sign bit '0' extended
        // Ensures positive sign extension produces 0s, not 1s
        public void PadSigned_PositiveSign_ExtendedWithZeros()
        {
            string a = "0";
            string b = "0101";
            Binary.PadSigned(ref a, ref b);
            Assert.AreEqual("0000", a);
            Assert.AreEqual("0101", b);
        }

        [TestMethod]
        // Edge: same-length inputs — neither string should be modified
        public void PadSigned_SameLength_BothUnchanged()
        {
            string a = "0101";
            string b = "1010";
            Binary.PadSigned(ref a, ref b);
            Assert.AreEqual("0101", a);
            Assert.AreEqual("1010", b);
        }


        // PADUNSIGNED


        [TestMethod]
        // Valid: shorter input zero-padded on the left to match longer
        public void PadUnsigned_ShorterInput_ZeroPaddedLeft()
        {
            string a = "1";
            string b = "101";
            Binary.PadUnsigned(ref a, ref b);
            Assert.AreEqual("001", a);
            Assert.AreEqual("101", b);
        }

        [TestMethod]
        // Edge: same-length inputs — neither string should be modified
        public void PadUnsigned_SameLength_BothUnchanged()
        {
            string a = "1010";
            string b = "0101";
            Binary.PadUnsigned(ref a, ref b);
            Assert.AreEqual("1010", a);
            Assert.AreEqual("0101", b);
        }

        [TestMethod]
        // Valid: much shorter second input — tests padding over a large width difference
        public void PadUnsigned_MuchShorter_PaddedToFullWidth()
        {
            string a = "11111111";
            string b = "1";
            Binary.PadUnsigned(ref a, ref b);
            Assert.AreEqual("11111111", a);
            Assert.AreEqual("00000001", b);
        }


        // DECIMALTOBCD


        [TestMethod]
        // Valid: single digit 0 → "0000" — minimum single-digit case
        public void DecimalToBCD_Zero_ReturnsFourZeroBits()
        {
            Assert.AreEqual("0000", Binary.DecimalToBCD("0"));
        }

        [TestMethod]
        // Valid: single digit 9 → "1001" — maximum valid BCD digit
        // Tests the top boundary before BCD would need two groups
        public void DecimalToBCD_Nine_Returns1001()
        {
            Assert.AreEqual("1001", Binary.DecimalToBCD("9"));
        }

        [TestMethod]
        // Valid: two-digit number — tests that groups are space-separated correctly
        // 1 → "0001", 2 → "0010"
        public void DecimalToBCD_TwoDigits_CorrectlySpaceSeparated()
        {
            Assert.AreEqual("0001 0010", Binary.DecimalToBCD("12"));
        }

        [TestMethod]
        // Valid: "99" — both digits at their BCD maximum
        // Tests that two maximum-value groups are produced correctly
        public void DecimalToBCD_MaxTwoDigits_TwoMaxGroups()
        {
            Assert.AreEqual("1001 1001", Binary.DecimalToBCD("99"));
        }

        [TestMethod]
        // Valid: three-digit number — confirms a third group is produced
        // 1 → "0001", 0 → "0000", 0 → "0000"
        public void DecimalToBCD_ThreeDigits_ThreeGroups()
        {
            Assert.AreEqual("0001 0000 0000", Binary.DecimalToBCD("100"));
        }


        // BCDADDITION


        [TestMethod]
        // Valid: 5 + 3 = 8 — result ≤ 9, no BCD correction needed
        // Tests the fast path where raw binary sum is already valid BCD
        public void BCDAddition_SumBelowNine_NoCorrectionNeeded()
        {
            Assert.AreEqual("1000", Binary.BCDAddition("0101", "0011"));
        }

        [TestMethod]
        // Valid: 5 + 5 = 10 — result > 9, correction applied, carry to next group
        // Tests that sumVal > 9 triggers the -10 correction and sets carry = 1
        public void BCDAddition_SumExceedsNine_CorrectionAndCarry()
        {
            Assert.AreEqual("0001 0000", Binary.BCDAddition("0101", "0101"));
        }

        [TestMethod]
        // Edge: 9 + 1 = 10 — boundary case, exactly one above valid BCD limit
        // Tests that sumVal == 10 (the first invalid value) triggers correction
        public void BCDAddition_BoundaryAtTen_TriggersCorrection()
        {
            Assert.AreEqual("0001 0000", Binary.BCDAddition("1001", "0001"));
        }

        [TestMethod]
        // Valid: 53 + 21 = 74 — two groups, no correction needed in either
        // Tests that right-to-left multi-group processing works correctly
        public void BCDAddition_MultiGroup_NoCorrection()
        {
            Assert.AreEqual("0111 0100", Binary.BCDAddition("0101 0011", "0010 0001"));
        }

        [TestMethod]
        // Valid: 99 + 1 = 100 — carry cascades through both groups, a third group is created
        // Tests that carry propagation across groups and overflow into a new group works
        public void BCDAddition_CarryPropagatesThroughAllGroups_CreatesNewGroup()
        {
            Assert.AreEqual("0001 0000 0000", Binary.BCDAddition("1001 1001", "0000 0001"));
        }
    }
}