namespace MathematicalExpressions.operations.operands
{
    public class Constant(double value) : Expression
    {
        public double Value { get; } = value;

        public override IEnumerable<string> Variables => [];
        public override bool IsConstant => true;
        public override bool IsPolynomial => true;
        public override int PolynomialDegree => 0;

        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Value;

        public override string ToString() => Value.ToString();
    }
}