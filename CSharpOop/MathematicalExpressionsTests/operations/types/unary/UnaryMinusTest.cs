using MathematicalExpressions.operations.operands;
using MathematicalExpressions;
using MathematicalExpressions.operations.types.unary;

namespace MathematicalExpressionsTests.operations.types.unary
{
    [TestClass]
    public class UnaryMinusTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);
        private static readonly UnaryMinus unaryMinusWithX = new(testVariableX);

        [TestMethod]
        public void Compute_PassCorrectIExprWithValue_ReturnMinusSameValueAsDouble()
            => Assert.AreEqual(-testVariableXValue, unaryMinusWithX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue}));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnMinusAndIExprInParenthesesAsString()
            => Assert.AreEqual($"-({testVariableXName})", unaryMinusWithX.ToString());
    }
}