using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.functions.trigonometric;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    [TestClass]
    public class SecantTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 1;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = Math.PI/2;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Secant secantX = new(testVariableX);
        private static readonly Secant secantY = new(testVariableY);

        [TestMethod]
        public void Compute_PassCorrectRadians_ReturnSecantOfThisValueAsDouble()
            => Assert.AreEqual(1 / Math.Cos(testVariableXValue), secantX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassRadiansWhereSecantNotDefined_ReturnNaN()
            => Assert.AreEqual(1 / Math.Cos(testVariableYValue), secantY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue
    }));

        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnSecAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"sec({testVariableXName})", secantX.ToString());
    }
}