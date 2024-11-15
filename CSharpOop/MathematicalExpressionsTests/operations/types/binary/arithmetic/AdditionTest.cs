using MathematicalExpressions.operations.types.binary.arithmetic;
using MathematicalExpressions.operations.operands;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.binary.arithmetic
{
    [TestClass]
    public class AdditionTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static  readonly double testConstant10Value = 10;
        private static readonly IExpr testConstant10 = new Constant(testConstant10Value);

        private static readonly Addition VariableXAndConstant10Addition = new(testVariableX, testConstant10);

        [TestMethod]
        public void ToString_PassCorrectIExprs_ReturnIExprsSeparatedByPlusInParenthesesAsString() 
            => Assert.AreEqual($"({testVariableXName} + {testConstant10Value})", VariableXAndConstant10Addition.ToString());
        [TestMethod]
        public void Compute_PassCorrectIExprs_ReturnAdditionOfFirstIExprAndSecondIExprAsDouble() 
            => Assert.AreEqual(testConstant10Value + testVariableXValue, 
                VariableXAndConstant10Addition.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
    }
}