namespace MathematicalExpressions.operations.types.functions.power
{
    public class Sqrt(IExpr operand) : Function(operand)
    {
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Sqrt(Operand.Compute(variableValues));
        public override string ToString() => $"sqrt({Operand})";
    }
}