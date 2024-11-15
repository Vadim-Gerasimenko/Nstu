namespace MathematicalExpressions.operations.types.binary.arithmetic
{
    public class Division(IExpr operand1, IExpr operand2) : BinaryOperation(operand1, operand2)
    {
        public override bool IsPolynomial => Operand1.IsPolynomial && Operand2.IsConstant;
        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
            => Operand1.Compute(variableValues) / Operand2.Compute(variableValues);
        public override string ToString() => $"{Operand1} / {Operand2}";
    }
}