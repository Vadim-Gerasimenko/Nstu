namespace MathematicalExpressions.operations.types.functions.power
{
    public class SquareRoot : Function
    {
        public SquareRoot(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Sqrt(Operand.Compute(variableValues));
        public override string ToString() => $"sqrt({Operand})";
    }
}