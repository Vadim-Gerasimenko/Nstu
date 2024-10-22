namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Cosecant : Function
    {
        public Cosecant(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => 1 / Math.Sin(Operand.Compute(variableValues));
        public override string ToString() => $"csc({Operand})";
    }
}