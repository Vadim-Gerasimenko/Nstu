using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.binary.arithmetic;
using MathematicalExpressions;
using MathematicalExpressionsTests.auxiliary;

namespace MathematicalExpressionsTests.operations.types.binary.arithmetic
{
    public class DivisionTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = 3;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);

        private static readonly double testConstant10Value = 10;
        private static readonly IExpr testConstant10 = new Constant(testConstant10Value);
        private static readonly IExpr testNotPolynomialIexprImplementer = new NotPolynomialIExprImplementer();

        private static readonly Division variableXAndConstant10Division = new(testVariableX, testConstant10);
        private static readonly Division constant10AndVariableXDivision = new(testConstant10, testVariableX);
        private static readonly Division variablesXAndYDivision = new(testVariableX, testConstant10);
        private static readonly Division constants10And10Division = new(testConstant10, testConstant10);
        private static readonly Division notPolynomialsDivision = new(testNotPolynomialIexprImplementer, testNotPolynomialIexprImplementer);

        [TestMethod]
        public void IsPolynomial_PassPolynomialsButNotConstants_False()
            => Assert.IsFalse(variablesXAndYDivision.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassPolynomialsConstants_ReturnTrue()
            => Assert.IsTrue(constants10And10Division.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassPolynomialConstantAndNotConstant_ReturnFalse()
            => Assert.IsFalse(constant10AndVariableXDivision.IsPolynomial);
        [TestMethod]
        public void IsPolynomial_PassNotPolynomials_ReturnFalse()
            => Assert.IsFalse(notPolynomialsDivision.IsPolynomial);
        [TestMethod]
        public void Compute_PassCorrectIExprs_ReturnDivisionfFirstIExprAndSecondIExprAsDouble()
           => Assert.AreEqual(testVariableXValue / testVariableXValue,
               variableXAndConstant10Division.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue, [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExprs_ReturnIExprsSeparatedByDivisionAsString()
    => Assert.AreEqual($"{testVariableXName} * {testConstant10Value}", variableXAndConstant10Division.ToString());
    }
}