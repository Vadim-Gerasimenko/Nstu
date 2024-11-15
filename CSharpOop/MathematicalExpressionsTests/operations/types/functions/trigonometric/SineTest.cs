using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.functions.power;
using MathematicalExpressions;
using MathematicalExpressions.operations.types.functions.trigonometric;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    [TestClass]
    public class SineTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 104;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = -44;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Sine sineX = new(testVariableX);
        private static readonly Sine sineY = new(testVariableY);

        [TestMethod]
        public void Compute_PassPositiveValue_ReturnSineOfThisValueAsDouble()
            => Assert.AreEqual(Math.Sin(testVariableXValue), sineX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassNegativeValue_ReturnSineOfValueAsDouble()
            => Assert.AreEqual(Math.Sin(testVariableYValue), sineY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnSinAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"sin({testVariableXName})", sineX.ToString());
    }
}