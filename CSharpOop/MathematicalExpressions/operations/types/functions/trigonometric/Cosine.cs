namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public class Cosine : Function
    {
        public Cosine(IExpr operand) : base(operand) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Math.Cos(Operand.Compute(variableValues));
        public override string ToString() => $"cos({Operand})";
    }
}