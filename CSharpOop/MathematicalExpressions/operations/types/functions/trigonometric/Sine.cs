namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Sine(IExpr operand) : Function(operand)
    {
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Sin(Operand.Compute(variableValues));
        public override string ToString() => $"sin({Operand})";
    }
}