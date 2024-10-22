namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Sine : Function
    {
        public Sine(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Sin(Operand.Compute(variableValues));
        public override string ToString() => $"sin({Operand})";
    }
}