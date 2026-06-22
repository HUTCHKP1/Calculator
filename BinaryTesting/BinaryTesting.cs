using Calculator;
namespace BinaryTesting
{
    [TestClass]
    public sealed class BinaryTests
    {
        // The 'Negate_SimpleCase_ReturnsCorrectMagnitude` method
        // is a unit test that verifies the correctness of the
        // 'Binary.Negate' function by checking that negating
        // the binary string "1011" results in the expected "0101".
        [TestMethod]
        public void Negate_SimpleCase_ReturnsCorrectMagnitude()
        {
            string result = Binary.Negate("1011");
            Assert.AreEqual("0101", result);
        }
        
        // This method currently fails (which is intentional and expected).
        // I chose to test this method because I was curious what would happen
        // if user input was binary 0, and as expected, the output was wrong
        // because the method kept pushing the final bit to the edge,
        // resulting in a binary 10000 instead.
        [TestMethod]
        public void Negate_ZeroIssue_ReturnsCorrectMagnitude()
        {
            string result = Binary.Negate("0000");
            Assert.AreEqual("0000", result);

        }
    }
}