using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.unary;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.unary
{
    [TestClass]
    public class UnaryPlusTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);
        private static readonly UnaryPlus unaryPlusWithX = new(testVariableX);

        [TestMethod]
        public void Compute_PassCorrectIExprWithValue_ReturnSameValueAsDouble()
            => Assert.AreEqual(testVariableXValue, unaryPlusWithX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnSameIExprAsString()
            => Assert.AreEqual($"{testVariableXName}", unaryPlusWithX.ToString());
    }
}