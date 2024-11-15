using MathematicalExpressions;
using MathematicalExpressions.operations.operands;
using MathematicalExpressionsTests.auxiliary;

namespace MathematicalExpressionsTests.operations.types.binary
{ 
    [TestClass]
    public class BinaryOperationTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly IExpr testVariableX = new Variable(testVariableXName);
        private static readonly string testVariableYName = "y";
        private static readonly IExpr testVariableY = new Variable(testVariableYName);

        private static readonly double testConstant10Value = 10;
        private static readonly IExpr testConstant10 = new Constant(testConstant10Value);
        private static readonly double testConstant20Value = 20;
        private static readonly IExpr testConstant20 = new Constant(testConstant20Value);

        private static readonly IExpr testNotPolynomialIexprImplementer = new NotPolynomialIExprImplementer();

        private static readonly BinaryOperationHeir operationWithConstants10And20 = new(testConstant10, testConstant20);
        private static readonly BinaryOperationHeir operationWithVariablesXAndY = new(testVariableX, testVariableY);
        private static readonly BinaryOperationHeir operationWithConstant10AndVariableX = new(testConstant10, testVariableX);
        private static readonly BinaryOperationHeir operationWithVariableYAndConstant20 = new(testVariableY, testConstant20);
        private static readonly BinaryOperationHeir operationWithPolynomialAndNotPolynomial = new(testConstant10, testNotPolynomialIexprImplementer);
        private static readonly BinaryOperationHeir operationWithNotPolynomialAndPolynomial = new(testNotPolynomialIexprImplementer, testConstant10);
        private static readonly BinaryOperationHeir operationWithNotPolynomials = new(testNotPolynomialIexprImplementer, testNotPolynomialIexprImplementer);

        [TestMethod]
        public void Variables_PassConstants_ReturnEmptyStringIEnumerable() 
            => Assert.IsTrue(!operationWithConstants10And20.Variables.Any());
        [TestMethod]
        public void Variables_PassVariables_ReturnStringIEnumerableWithSameVariables()
            => Assert.IsTrue(operationWithVariablesXAndY.Variables.Count() == 2
                && operationWithVariablesXAndY.Variables.Contains(testVariableXName)
                && operationWithVariablesXAndY.Variables.Contains(testVariableYName));
        [TestMethod]
        public void Variables_PassConstantAndVariable_ReturnStringIEnumerableWithVariable()
            => Assert.IsTrue(operationWithConstant10AndVariableX.Variables.Contains(testVariableXName) 
                && operationWithConstant10AndVariableX.Variables.Count() == 1);
        [TestMethod]
        public void Variables_PassVariableAndConstant_ReturnStringIEnumerableWithVariable()
            => Assert.IsTrue(operationWithVariableYAndConstant20.Variables.Contains(testVariableYName) 
                && operationWithVariableYAndConstant20.Variables.Count() == 1);
        [TestMethod]
        public void IsConstant_PassConstants_ReturnTrue()
            => Assert.IsTrue(operationWithConstants10And20.IsConstant);
        [TestMethod]
        public void IsConstant_PassNotConstants_ReturnFalse()
            => Assert.IsFalse(operationWithVariablesXAndY.IsConstant);
        [TestMethod]
        public void IsConstant_PassConstantAndNotConstant_ReturnFalse()
            => Assert.IsFalse(operationWithConstant10AndVariableX.IsConstant);
        [TestMethod]
        public void IsConstant_PassNotConstantAndConstant_ReturnFalse()
            => Assert.IsFalse(operationWithVariableYAndConstant20.IsConstant);
        [TestMethod]
        public void IsPolynomial_PassPolynomials_ReturnTrue()
            => Assert.IsTrue(operationWithConstants10And20.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassNotPolynomials_ReturnFalse()
            => Assert.IsFalse(operationWithNotPolynomials.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassPolynomialAndNotPolynomial_ReturnFalse()
            => Assert.IsFalse(operationWithPolynomialAndNotPolynomial.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassNotPolynomialAndPolynomial_ReturnFalse()
            => Assert.IsFalse(operationWithNotPolynomialAndPolynomial.IsPolynomial);
        [TestMethod]
        public void PolynomialDegree_PassPolynomialsWithDifferentDegrees_ReturnMaxDegreeAsInt()
            => Assert.AreEqual(1, operationWithConstant10AndVariableX.PolynomialDegree);
        [TestMethod]
        public void PolynomialDegree_PassPolynomialsWithSameDegrees_ReturnSameDegreeAsInt()
            => Assert.AreEqual(0, operationWithConstants10And20.PolynomialDegree);
        [TestMethod]
        public void PolynomialDegree_PassNotPolynomial_ReturnMinus1AsInt()
            => Assert.AreEqual(-1, operationWithNotPolynomials.PolynomialDegree);
    }
}