using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.binary.arithmetic;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.binary.arithmetic
{
    public class SubtractionTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly double testConstant10Value = 10;
        private static readonly IExpr testConstant10 = new Constant(testConstant10Value);

        private static readonly Subtraction VariableXAndConstant10Subtraction = new Subtraction(testVariableX, testConstant10);

        [TestMethod]
        public void ToString_PassCorrectIExprs_ReturnIExprsSeparatedByMinusInParenthesesAsString()
            => Assert.AreEqual($"({testVariableXName} - {testConstant10Value})", VariableXAndConstant10Subtraction.ToString());
        [TestMethod]
        public void Compute_PassCorrectIExprs_ReturnSubtractionOfFirstIExprAndSecondIExprAsDouble()
            => Assert.AreEqual(testVariableXValue - testConstant10Value, 
                VariableXAndConstant10Subtraction.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
    }
}