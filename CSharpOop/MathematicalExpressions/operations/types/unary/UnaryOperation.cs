namespace MathematicalExpressions.operations.types.unary
{
    public abstract class UnaryOperation : Expression
    {
        public IExpr Operand { get; }

        public UnaryOperation(IExpr operand) => Operand = operand;

        public override IEnumerable<string> Variables => Operand.Variables;
        public override bool IsConstant => Operand.IsConstant;
        public override bool IsPolynomial => Operand.IsPolynomial;
        public override int PolynomialDegree => Operand.PolynomialDegree;
    }
}