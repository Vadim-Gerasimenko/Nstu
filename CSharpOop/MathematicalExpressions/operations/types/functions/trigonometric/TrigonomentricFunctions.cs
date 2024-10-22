namespace MathematicalExpressions.operations.types.functions.trigonometric
{
    public static class TrigonomentricFunctions
    {
        public static Sine Sin(Expression argument) => new(argument);
        public static Cosine Cos(Expression argument) => new(argument);
        public static Tangent Tan(Expression argument) => new(argument);
        public static Cotangent Cot(Expression argument) => new(argument);
        public static Secant Sec(Expression argument) => new(argument);
        public static Cosecant Csc(Expression argument) => new(argument);
    }
}