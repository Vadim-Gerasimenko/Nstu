using MathematicalExpressions.operations.operands;
using MathematicalExpressions;
using MathematicalExpressions.operations.types.functions.power;

namespace MathematicalExpressionsTests.operations.types.functions.power
{
    [TestClass]
    public class SqrtTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 16;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = -4;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Sqrt sqrtX = new(testVariableX);
        private static readonly Sqrt sqrtY = new(testVariableY);

        [TestMethod]
        public void Compute_PassPositiveValue_ReturnSquareRootOfThisValueAsDouble()
            => Assert.AreEqual(Math.Sqrt(testVariableXValue), sqrtX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassNegativeValue_ReturnNaN()
            => Assert.AreEqual(double.NaN, sqrtY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnSqrtAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"sqrt({testVariableXName})", sqrtX.ToString());
    }
}