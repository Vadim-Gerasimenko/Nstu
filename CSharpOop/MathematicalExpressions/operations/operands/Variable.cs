namespace MathematicalExpressions.operations.operands
{
    public class Variable : Expression
    {
        public string Name { get; }

        public Variable(string name) => Name = name;

        public override IEnumerable<string> Variables => [Name];
        public override bool IsConstant => false;
        public override bool IsPolynomial => true;
        public override int PolynomialDegree => 1;

        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => variableValues[Name];

        public override string ToString() => Name;
    }
}