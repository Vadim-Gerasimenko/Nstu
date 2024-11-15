using MathematicalExpressions.operations.types.binary;
using MathematicalExpressions;

namespace MathematicalExpressionsTests.auxiliary
{
    internal class BinaryOperationHeir : BinaryOperation
    {
        internal BinaryOperationHeir(IExpr operand1, IExpr operand2) : base(operand1, operand2) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => throw new NotImplementedException();
    }
}