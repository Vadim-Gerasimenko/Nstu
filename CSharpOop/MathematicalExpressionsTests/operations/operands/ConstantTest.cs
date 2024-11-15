using MathematicalExpressions.operations.operands;

namespace MathematicalExpressionsTests.operations.operands
{
    [TestClass]
    public class ConstantTest
    {
        private static readonly double testConstantValue = 10;
        private static readonly string testConstantAsString = "10";
        private static readonly Constant testConstant = new(testConstantValue);

        [TestMethod]
        public void Value_WithCorrectValue_ReturnSameValue() => Assert.AreEqual(testConstantValue, testConstant.Value);
        [TestMethod]
        public void Variables_IsEmptyForConstant_ReturnEmptyStringIEnumerable() => Assert.AreEqual(0, testConstant.Variables.Count());
        [TestMethod]
        public void IsConstant_IsTrueForConstant_ReturnTrue() => Assert.IsTrue(testConstant.IsConstant);
        [TestMethod]
        public void IsPolynomial_IsTrueForConstant_ReturnTrue() => Assert.IsTrue(testConstant.IsPolynomial);
        [TestMethod]
        public void PolynomialDegree_Is0ForConstant_Return0AsInt() => Assert.AreEqual(0, testConstant.PolynomialDegree);
        [TestMethod]
        public void Compute_WithCorrectValueNotDependOnVariables_ReturnSameValueAsDouble() 
            => Assert.AreEqual(testConstantValue, testConstant.Compute(new Dictionary<string, double> { ["x"] = 7}));
        [TestMethod]
        public void ToString_WithCorrectValue_ReturnSameValueAsString() => Assert.AreEqual(testConstantAsString, testConstant.ToString());
    }
}