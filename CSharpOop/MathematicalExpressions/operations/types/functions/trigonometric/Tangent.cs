namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Tangent : Function
    {
        public Tangent(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Tan(Operand.Compute(variableValues));
        public override string ToString() => $"tan({Operand})";
    }
}