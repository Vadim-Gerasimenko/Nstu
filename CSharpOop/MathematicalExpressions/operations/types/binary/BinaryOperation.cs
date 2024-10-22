namespace MathematicalExpressions.operations.types.binary
{
    public abstract class BinaryOperation : Expression
    {
        public IExpr Operand1 { get; }
        public IExpr Operand2 { get; }

        public BinaryOperation(IExpr operand1, IExpr operand2)
        {
            Operand1 = operand1;
            Operand2 = operand2;
        }

        public override IEnumerable<string> Variables => Operand1.Variables.Union(Operand2.Variables);
        public override bool IsConstant => Operand1.IsConstant && Operand2.IsConstant;
        public override bool IsPolynomial => Operand1.IsPolynomial && Operand2.IsPolynomial;
        public override int PolynomialDegree => IsPolynomial ? Math.Max(Operand1.PolynomialDegree, Operand2.PolynomialDegree) : -1;
    }
}