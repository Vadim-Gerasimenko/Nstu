namespace MathematicalExpressions.operations.operands
{
    public class Variable : Expression
    {
        public string Name { get; }

        public Variable(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name is null");

            if (name == string.Empty)
                throw new ArgumentException("name is empty string");
            Name = name;
        }

        public override IEnumerable<string> Variables => [Name];
        public override bool IsConstant => false;
        public override bool IsPolynomial => true;
        public override int PolynomialDegree => 1;

        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => variableValues[Name];

        public override string ToString() => Name;
    }
}