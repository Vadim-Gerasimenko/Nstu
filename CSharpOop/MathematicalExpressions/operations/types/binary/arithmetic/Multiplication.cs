namespace MathematicalExpressions.operations.types.binary.arithmetic
{
    public class Multiplication(IExpr operand1, IExpr operand2) : BinaryOperation(operand1, operand2)
    {
        public override int PolynomialDegree => IsPolynomial ? Operand1.PolynomialDegree + Operand2.PolynomialDegree : -1;
        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
            => Operand1.Compute(variableValues) * Operand2.Compute(variableValues);
        public override string ToString() => $"{Operand1} * {Operand2}";
    }
}