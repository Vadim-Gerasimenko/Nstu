using MathematicalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathematicalExpressionsTests.auxiliary
{
    internal class NotPolynomialIExprImplementer : IExpr
    {
        internal NotPolynomialIExprImplementer() { }
        public IEnumerable<string> Variables => throw new NotImplementedException();
        public bool IsConstant => throw new NotImplementedException();
        public double Compute(IReadOnlyDictionary<string, double> variableValues) => throw new NotImplementedException();
        public bool IsPolynomial => false;
        public int PolynomialDegree => -1;
    }
}
