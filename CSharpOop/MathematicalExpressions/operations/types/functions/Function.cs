using MathematicalExpressions.operations.types.unary;

namespace MathematicalExpressions.operations.types.functions
{
    public abstract class Function(IExpr operand) : UnaryOperation(operand) { }
}