using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.functions.trigonometric;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.operations.types.functions.trigonometric
{
    [TestClass]
    public class CosecantTest
    {
        /*
        public CosecantTest(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => 1 / Math.Sin(Operand.Compute(variableValues));
        public override string ToString() => $"csc({Operand})";
        */
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = Math.PI / 2;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = 0;
        private static readonly IExpr testVariableY = new Variable(testVariableYName);
        private static readonly Cosecant cosecantX = new(testVariableX);
        private static readonly Cosecant cosecantY = new(testVariableY);

        [TestMethod]
        public void Compute_PassCorrectRadians_ReturnCosecantOfThisValueAsDouble()
            => Assert.AreEqual(1 / Math.Sin(testVariableXValue), cosecantX.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue }));
        [TestMethod]
        public void Compute_PassRadiansWhereCosecantNotDefined_ReturnNaN()
         => Assert.AreEqual(1 / Math.Sin(testVariableYValue), cosecantY.Compute(new Dictionary<string, double> { [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExpr_ReturnCscAndSameIExprInParenthesesAsString()
            => Assert.AreEqual($"csc({testVariableXName})", cosecantX.ToString());
    }
}