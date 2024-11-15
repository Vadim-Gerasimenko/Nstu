using MathematicalExpressions.operations.operands;
using MathematicalExpressions.operations.types.unary;
using MathematicalExpressions.operations.types.binary.arithmetic;

namespace MathematicalExpressions
{
    public abstract class Expression : IExpr
    {
        public abstract IEnumerable<string> Variables { get; }
        public abstract bool IsConstant { get; }
        public abstract bool IsPolynomial { get; }
        public abstract int PolynomialDegree { get; }
        public abstract double Compute(IReadOnlyDictionary<string, double> variableValues);
        public static implicit operator Expression(double operand) => new Constant(operand);

        public static Expression operator +(Expression operand) => new UnaryPlus(operand);
        public static Expression operator -(Expression operand) => new UnaryMinus(operand);

        public static Expression operator +(Expression operand1, Expression operand2) => new Addition(operand1, operand2);
        public static Expression operator -(Expression operand1, Expression operand2) => new Subtraction(operand1, operand2);
        public static Expression operator *(Expression operand1, Expression operand2) => new Multiplication(operand1, operand2);
        public static Expression operator /(Expression operand1, Expression operand2) => new Division(operand1, operand2);
        public override bool Equals(object? obj)
        {
            if (obj == this) return true;
            if (obj == null || obj.GetType() != GetType()) return false;

            Expression expression = (Expression)obj;
            return string.Equals(ToString(), expression.ToString());
        }
    }
}