using MathematicalExpressions.operations.operands;

namespace MathematicalExpressionsTests.operations.operands
{
    [TestClass]
    public class VariableTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5;
        private static readonly Variable testVariableX = new(testVariableXName);

        [TestMethod]
        public void InitialVariable_PassEmptyString_ThrowArgumentException()
        {
            try { var emptyNameVariable = new Variable(string.Empty); }
            catch (ArgumentException) { return; }
            Assert.Fail();
        }

        [TestMethod]
        public void Variables_IsIEnumerableOfOneString_ReturnIEnumerableOfThisString() 
            => Assert.IsTrue(testVariableX.Variables.Contains(testVariableXName) && testVariableX.Variables.Count() == 1);
        [TestMethod]
        public void IsConstant_IsFalseForVariable_ReturnFalse() => Assert.IsFalse(testVariableX.IsConstant);
        [TestMethod]
        public void IsPolynomial_IsTrueForVariable_ReturnTrue() => Assert.IsTrue(testVariableX.IsPolynomial);
        [TestMethod]
        public void PolynomialDegree_Is1ForVariable_Return1() => Assert.AreEqual(1, testVariableX.PolynomialDegree);
        [TestMethod]
        public void Compute_CorrectVariableAndValueDependOnOnlyOneKeyValuePair_ReturnSameValueAsDouble() 
            => Assert.AreEqual(testVariableXValue, testVariableX.Compute(new Dictionary<string, double> {["y"] = 4, [testVariableXName] = testVariableXValue}));
        [TestMethod]
        public void Compute_PassDictionaryWithoutRequiredVariable_ThrowException() {
            try { var value = testVariableX.Compute(new Dictionary<string, double> {}); }
            catch { return; }
            Assert.Fail();
        }
        [TestMethod]
        public void Name_WithCorrectName_ReturnSameNameAsString() => Assert.AreEqual(testVariableXName, testVariableX.ToString());
    }
}