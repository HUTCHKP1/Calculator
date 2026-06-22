namespace CalcTest1
{
    [TestClass]
    public sealed class BasicTest
    {
        [TestMethod]
        public void TestMethod1() // Addition test 1
        {
            double expectedResult = 2;
            double actualResult = (1+1);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestMethod2() // Subtraction test 1
        {
            double expectedResult = 4;
            double actualResult = (10 - 6);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestMethod3() // Division test 1
        {
            double expectedResult = 5;
            double actualResult = (10 / 2);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestMethod4() // Multiplication test 1
        {
            double expectedResult = 30;
            double actualResult = (10 * 3);
            Assert.AreEqual(expectedResult, actualResult);
        }
        [TestMethod]
        public void TestMethod5() // Modulus test 1
        {
            double expectedResult = 5;
            double actualResult = (17 % 6);
            Assert.AreEqual(expectedResult, actualResult);
        }


    }
}
