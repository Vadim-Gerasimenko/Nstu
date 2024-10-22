using MathematicalExpressions.operations.types.unary;

namespace MathematicalExpressions.operations.types.functions
{
    public abstract class Function : UnaryOperation 
    { 
        public Function(IExpr operand) : base(operand) { }

        public override bool IsConstant => Operand.IsConstant;
        public override bool IsPolynomial => false; 
        public override int PolynomialDegree => -1; 
    }
}