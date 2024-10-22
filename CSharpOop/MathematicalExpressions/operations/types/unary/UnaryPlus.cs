namespace MathematicalExpressions.operations.types.unary
{
    public class UnaryPlus : UnaryOperation
    {
        public UnaryPlus(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Operand.Compute(variableValues);
        public override string ToString() => $"{Operand}";
    }
}