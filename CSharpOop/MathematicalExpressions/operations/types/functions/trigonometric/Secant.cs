namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Secant : Function
    {
        public Secant(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => 1 / Math.Cos(Operand.Compute(variableValues));
        public override string ToString() => $"sec({Operand})";
    }
}