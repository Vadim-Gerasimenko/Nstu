using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.functions.trigonometric;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    [TestClass]
    public class CosineTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 104;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = -44;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Cosine cosineX = new(testVariableX);
        private static readonly Cosine cosineY = new(testVariableY);

        [TestMethod]
        public void Compute_PassPositiveValue_ReturnCosineOfThisValueAsDouble()
            => Assert.AreEqual(Math.Cos(testVariableXValue), cosineX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassNegativeValue_ReturnCosineOfValueAsDouble()
            => Assert.AreEqual(Math.Cos(testVariableYValue), cosineY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnCosAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"cos({testVariableXName})", cosineX.ToString());
    }
}