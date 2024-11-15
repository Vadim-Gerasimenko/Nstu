using MathematicalExpressions.operations.operands;
using MathematicalExpressions;
using MathematicalExpressions.operations.types.functions.trigonometric;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    [TestClass]
    public class TangentTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 104;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = Math.PI / 2;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Tangent tangentX = new(testVariableX);
        private static readonly Tangent tangentY = new(testVariableY);

        [TestMethod]
        public void Compute_PassCorrectRadians_ReturnTangentOfThisValueAsDouble()
            => Assert.AreEqual(Math.Tan(testVariableXValue), tangentX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassRadiansWhereTangentNotDefined_ReturnNaN()
            => Assert.AreEqual(Math.Tan(testVariableYValue), tangentY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnTanAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"tan({testVariableXName})", tangentX.ToString());
    }
}