using MathematicalExpressions.operations.types.binary;
using MathematicalExpressions;
using MathematicalExpressions.operations.types.unary;
using MathematicalExpressions.operations.operands;
using MathematicalExpressionsTests.operations.types.binary;
using MathematicalExpressionsTests.auxiliary;

namespace MathematicalExpressionsTests.operations.types.unary
{
    [TestClass]
    public class UnaryOperationTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly IExpr testVariableX = new Variable(testVariableXName);
        private static readonly double testConstant10Value = 10;
        private static readonly IExpr testConstant10 = new Constant(testConstant10Value);
        private static readonly IExpr testNotPolynomialIexprImplementer = new NotPolynomialIExprImplementer();

        private static readonly UnaryOperationHeir heirUnaryOperationWithVariable = new(testVariableX);
        private static readonly UnaryOperationHeir heirUnaryOperationWithConstant = new(testConstant10);
        private static readonly UnaryOperationHeir heirUnaryOperationWithNotPolynomial = new(testNotPolynomialIexprImplementer);

        [TestMethod]
        public void Variables_PassIExprWithVariable_ReturnStringIEnumerableWithVariable()
            => Assert.IsTrue(heirUnaryOperationWithVariable.Variables.Count() == 1
                && heirUnaryOperationWithVariable.Variables.Contains(testVariableXName));
        [TestMethod]
        public void Variables_PassIExprWithoutVariables_ReturnEmptyStringIEnumerable()
            => Assert.IsTrue(!heirUnaryOperationWithConstant.Variables.Any());
        [TestMethod]
        public void IsConstant_PassConstant_ReturnTrue()
            => Assert.IsTrue(heirUnaryOperationWithConstant.IsConstant);
        [TestMethod]
        public void IsConstant_PassNotConstant_ReturnFalse()
        => Assert.IsFalse(heirUnaryOperationWithVariable.IsConstant);
        [TestMethod]
        public void IsPolynomial_PassPolynomial_ReturnTrue()
            => Assert.IsTrue(heirUnaryOperationWithConstant.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassNotPolynomial_ReturnFalse()
            => Assert.IsFalse(heirUnaryOperationWithNotPolynomial.IsPolynomial);
        [TestMethod]
        public void PolynomialDegree_PassPolynomialWithCorrectDegree_ReturnSamePolynomialDegreeAsInt()
            => Assert.AreEqual(1, heirUnaryOperationWithVariable.PolynomialDegree);
        [TestMethod]
        public void PolynomialDegree_PassNotPolynomial_ReturnMinus1AsInt()
            => Assert.AreEqual(-1, heirUnaryOperationWithNotPolynomial.PolynomialDegree);
    }
}