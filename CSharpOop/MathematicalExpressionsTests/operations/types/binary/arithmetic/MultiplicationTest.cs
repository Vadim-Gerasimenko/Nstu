using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.binary.arithmetic;
using MathematicalExpressions;
using MathematicalExpressionsTests.auxiliary;

namespace MathematicalExpressionsTests.operations.types.binary.arithmetic
{
    public class MultiplicationTest
    {
        private static readonly string testVariableXName = "x";
        private static readonly double testVariableXValue = 5.5;
        private static readonly IExpr testVariableX = new Variable(testVariableXName);

        private static readonly string testVariableYName = "y";
        private static readonly double testVariableYValue = 10;
        private static readonly IExpr testVariableY = new Variable(testVariableXName);

        private static readonly Multiplication VariablesXAndYMultiplication = new(testVariableX, testVariableY);

        [TestMethod]
        public void PolynomialDegree_PassPolynomials_ReturnSumOfPolynomialDegreesAsInt()
            => Assert.AreEqual(2, VariablesXAndYMultiplication.PolynomialDegree);
        [TestMethod]
        public void PolynomialDegree_PassNotPolynomial_ReturnMinus1AsInt()
            => Assert.AreEqual(-1, new Multiplication(testVariableX, new NotPolynomialIExprImplementer()).PolynomialDegree);
        [TestMethod]
        public void Compute_PassCorrectIExprs_ReturnMultiplicationOfFirstIExprAndSecondIExprAsDouble()
            => Assert.AreEqual(testVariableXValue * testVariableYValue, 
                VariablesXAndYMultiplication.Compute(new Dictionary<string, double> { [testVariableXName] = testVariableXValue, [testVariableYName] = testVariableYValue }));
        [TestMethod]
        public void ToString_PassCorrectIExprs_ReturnIExprsSeparatedByMultiplicationAsString()
            => Assert.AreEqual($"{testVariableXName} * {testVariableYName}", VariablesXAndYMultiplication.ToString());
    }
}