namespace MathematicalExpressions
{
    public interface IExpr
    {
        IEnumerable<string> Variables { get; }
        bool IsConstant { get; }
        bool IsPolynomial { get; }
        int PolynomialDegree { get; }
        double Compute(IReadOnlyDictionary<string, double> variableValues);
    }
}