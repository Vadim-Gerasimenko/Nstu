using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.functions.trigonometric;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    public class CotangentTest
    {
        [TestClass]
        public class TangentTest
        {
            private static readonly string testVariableXName = "x";
            private static readonly double testVariableXValue = Math.PI;
            private static readonly IExpr testVariableX = new Variable(testVariableXName);

            private static readonly string testVariableYName = "y";
            private static readonly double testVariableYValue = 0;
            private static readonly IExpr testVariableY = new Variable(testVariableYName);
            private static readonly Cotangent cotangentX = new(testVariableX);
            private static readonly Cotangent cotangentY = new(testVariableY);

            [TestMethod]
            public void Compute_PassCorrectRadians_ReturnCotangentOfThisValueAsDouble()
                => Assert.AreEqual(1 / Math.Tan(testVariableXValue), cotangentX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
            [TestMethod]
            public void Compute_PassRadiansWhereCotangentNotDefined_ReturnNaN()
                => Assert.AreEqual(1 / Math.Tan(testVariableYValue), cotangentY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
            [TestMethod]
            public void ToString_PassCorrectIExpr_ReturnCotAndSameIExprInParenthesesAsString()
                => Assert.AreEqual($"cot({testVariableXName})", cotangentX.ToString());
        }
    }
}