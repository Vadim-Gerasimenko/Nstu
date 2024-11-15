using MathematicalExpressions.operations.types.unary;
using MathematicalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalExpressionsTests.auxiliary
{
    internal class UnaryOperationHeir : UnaryOperation
    {
        public UnaryOperationHeir(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => throw new NotImplementedException();
    }
}
