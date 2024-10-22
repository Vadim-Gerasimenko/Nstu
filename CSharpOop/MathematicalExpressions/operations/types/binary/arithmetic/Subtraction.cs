namespace MathematicalExpressions.operations.types.binary.arithmetic
{
    public class Subtraction : BinaryOperation
    {
        public Subtraction(IExpr operand1, IExpr operand2) : base(operand1, operand2) { }
        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
            => Operand1.Compute(variableValues) - Operand2.Compute(variableValues);
        public override string ToString() => $"({Operand1} - {Operand2})";
    }
}