namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Cotangent(IExpr operand) : Function(operand)
    {
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => 1 / Math.Tan(Operand.Compute(variableValues));
        public override string ToString() => $"cot({Operand})";
    }
}